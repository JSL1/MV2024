using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBullet : MonoBehaviour
{   
    public float xSpeed;
    public float ySpeed;
    public float shotSpeed;
	public float damage = 7;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        fire();
    }

    void fire() {
        rb2d.velocity = new Vector2(xSpeed * shotSpeed, ySpeed * shotSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Renderer>().isVisible) {
			Destroy(this.gameObject, 0.5f);
		}
    }
}
