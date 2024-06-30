using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
	public float damage;
	
    // Start is called before the first frame update
    void Start()
    {
		
    }
	
	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "enemy") {
			Debug.Log("enemy hit");
		}
	}


    // Update is called once per frame
    void Update()
    {
        
    }
}
