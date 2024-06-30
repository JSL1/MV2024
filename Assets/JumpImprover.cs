using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpImprover : MonoBehaviour
{
	private float fallMultiplier = 2.5f;
	private float lowJumpMultiplier = 1.1f;
	private Rigidbody2D rb2d;
	
	void Awake() {
		rb2d = this.GetComponent<Rigidbody2D>();
	}
    // Start is called before the first frame update
    void Start()
    {
			
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.y < 0) {
			rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;	
		}		
    }
}
