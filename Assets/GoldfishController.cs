using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldfishController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool facingRight;
    private float moveX;
    private float shootDirection = 1;
	private Animator myAnim;
	private bool shooting;
	private float shootTimer = 0f;
	private float shootCd = 0.07f;
	
    // Start is called before the first frame update
    void Start()
    {
		myAnim = this.GetComponent<Animator>();
    }

    void flipFishy() {
        facingRight = !facingRight;
    }

    void Shoot() {
		myAnim.SetBool("active", true);
		if (!shooting) {
			float offSetY = Random.Range(-0.5f, 0.5f);
			float offSet = Random.Range(-0.24f, 0.24f);
			GameObject bullet = Instantiate(bulletPrefab, new Vector2(firePoint.position.x, firePoint.position.y + offSet), firePoint.rotation);
			bullet.GetComponent<FishBullet>().xSpeed = shootDirection;
			bullet.GetComponent<FishBullet>().ySpeed = 0;
			bullet.GetComponent<FishBullet>().shotSpeed = 10;
			shooting = true;
			shootTimer = shootCd;
		}
    }
    // Update is called once per frame
    void Update() {
        moveX = Input.GetAxis("Horizontal");

        if (Input.GetButton("Fire2")) {
            Shoot();
        }

        if (facingRight) {
            shootDirection = 1.0f;
        } else {
            shootDirection = -1.0f;
        }

        if (moveX < 0.0f && facingRight == true) {
            flipFishy();
        } else if (moveX > 0.0f && facingRight == false) {
            flipFishy();
        }
		
		if (shooting) {
			if (shootTimer > 0) {
				shootTimer -= Time.deltaTime;
			} else {
				shooting = false;
			}
		}
    }
}
