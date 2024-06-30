using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //Player state data
    private bool isRunning;
    private bool isJumping;
    private bool isFalling;
    //Attacking1 defines whether or not you are doing the first or second attack animation
    //attacking defines whether you are in any attack animation
    private bool isAttacking1;
    private bool attacking;
    private bool isDashing;
	private bool hasBeenHit;
    private float attackTimer = 0.0f;
    private float attackCd = 0.24f; //attack cooldown
    private float dashTimer = 0.0f;
    private float dashCd = 0.16f;
    private Rigidbody2D rb2d;
    private BoxCollider2D playerCollider;
    public float playerRunSpeed;
    public float playerJumpHeight;
    public float playerDashForce;
    public float playerHealth;
    public bool facingRight;
    private Animator myAnim;
	public GameObject attackHitbox;

	public GameObject fishy;
	
    // Start is called before the first frame update
    void Start() {
        rb2d = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        facingRight = true;
        myAnim = this.GetComponent<Animator>();

    }

    void processInput() {
        //horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        if (moveX != 0.0f) {
            isRunning = true;
            myAnim.SetBool("isRunning", true);
        } else {
            myAnim.SetBool("isRunning", false);
            isRunning = false;
        }

		//jumping, falling animation
        if (rb2d.velocity.y > 0 && !hasBeenHit) {
            myAnim.SetBool("isJumping", true);
            isJumping = true;
        } else if (rb2d.velocity.y < 0 && !hasBeenHit) {
            myAnim.SetBool("isFalling", true);
            isJumping = false;
            isFalling = true;
        } else if (rb2d.velocity.y == 0) {
            myAnim.SetBool("isFalling", false);
            myAnim.SetBool("isJumping", false);
            isJumping = false;
            isFalling = false;
			hasBeenHit = false;
        }
    
        //Horizontal movement
        if (!attacking && !isDashing && !hasBeenHit) {
            rb2d.velocity = new Vector2(moveX * playerRunSpeed, rb2d.velocity.y);
        } else if (isDashing && !hasBeenHit) {
            if (facingRight) {
                rb2d.velocity = new Vector2(playerDashForce * 1, rb2d.velocity.y);
            } else {
                rb2d.velocity = new Vector2(playerDashForce * -1, rb2d.velocity.y);
            }
		} else if (hasBeenHit) {
			rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
			
        } else {
            rb2d.velocity = new Vector2(0, 0);
        }
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.001f) {
            jump();
        }

        //Assert facing direction
        if (moveX < 0.0f && facingRight == true) {
            flipPlayer();
        } else if (moveX > 0.0f && facingRight == false) {
            flipPlayer();
        }

        //Attacking, dashing
        if (Input.GetButtonDown("Fire1")) {
            attack();
        } else if (Input.GetButton("Fire2")) {
			fishy.SetActive(true);
        } else if (Input.GetButtonDown("Fire3")) {
            if (!isJumping && !isFalling) {
                dash();
            }
        } else {
			fishy.SetActive(false);
		}

    }

	void groundPlayer() {
		myAnim.SetBool("isFalling", false);
        myAnim.SetBool("isJumping", false);
		hasBeenHit = false;
        isJumping = false;
        isFalling = false;
		rb2d.velocity = new Vector2(0, 0);
		Debug.Log("player grounded");
	}

    void jump() {
        Debug.Log("Jump requrested");
        rb2d.velocity = Vector2 .up * playerJumpHeight;
        myAnim.SetBool("isJumping", true);
		isJumping = true;
    }

    void flipPlayer() {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

	void takeDamage() {
		if (!hasBeenHit) {
			float dir = 1;
			if (facingRight) { 
				dir = -1f; 
			} else { 
				dir = 1f; 
			}
			hasBeenHit = true;
			myAnim.SetTrigger("Hit");
			rb2d.velocity = new Vector2(dir * (playerRunSpeed / 2), playerJumpHeight / 2);
		}
	}

    void attack() {
        if (!attacking) {
            attacking = true;
            if (!isAttacking1) {
                myAnim.SetTrigger("Attack");
                isAttacking1 = true;
            } else {
                isAttacking1 = false;
                myAnim.SetTrigger("Attack2");
            }
            attackTimer = attackCd;
        }
    }

    void attackThree() {
    }

    void dash() {
        if (!isDashing) {
            isDashing = true;
            myAnim.SetTrigger("Dash");
            dashTimer = dashCd;
        }
    }

	//Collision detection and damage
	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "ground") {
			groundPlayer();
		}
		
		if (collider.gameObject.tag == "enemy") {
			takeDamage();
		}
	}

    // Update is called once per frame
    void Update() {
        processInput();

        //Attack timer countdown
        if (attacking) {
            if (attackTimer > 0) {
                attackTimer -= Time.deltaTime;
				attackHitbox.gameObject.SetActive(true);
            } else {
                attacking = false;
				attackHitbox.gameObject.SetActive(false);
            }
        }

        //Dash timer countdown
        if (isDashing) {
            if (dashTimer > 0) {
                dashTimer -= Time.deltaTime;
            } else {
                isDashing = false;
            }
        }
		
    }
}
