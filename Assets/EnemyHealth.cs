using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public float health;
	public float defense;
	private Animator myAnim;
	private Rigidbody2D rb2d;
	public GameObject deathEffect;
	public GameObject damageNumber;
	
    // Start is called before the first frame update
    void Start()
    {
			myAnim = this.GetComponent<Animator>();
			rb2d = this.GetComponent<Rigidbody2D>();
    }
	

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "playerAttack") {
			float dmg = collider.gameObject.GetComponent<MeleeHitbox>().damage;
			float dmgVariance = Random.Range((dmg - defense) * 1.1f, (dmg - defense) * 0.9f); 
			health = health - dmgVariance;
			Debug.Log("enemy damaged for. " + dmgVariance + ". Enemy health: " + health);
			StartCoroutine(Flash());
			if (health < 001f) {
				die();
			}
		} else if (collider.tag == "playerBullet") {
			float dmg = collider.gameObject.GetComponent<FishBullet>().damage;
			float dmgVariance = Random.Range((dmg - (1f / defense)) * 1.1f, (dmg - (1f / defense)) * 0.9f);
			health = health - dmgVariance;
			showDamage(dmgVariance);
			StartCoroutine(Flash());
			if (health < 001f) {
				die();
			}
		}
	}
	
	void showDamage(float damage) {
		var dn = Instantiate(damageNumber, transform.position, Quaternion.identity);
		dn.GetComponent<TextMesh>().text = Mathf.Floor(damage).ToString();
		dn.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 6f));
	}
	
	void die() {
		myAnim.SetTrigger("dead");
		Instantiate(deathEffect, this.gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject, 0.5f);
	}
	
	IEnumerator Flash() {
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds(0.5f);
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	}
		
    // Update is called once per frame
    void Update()
    {

    }
}
