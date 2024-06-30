using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
	public GameObject explosion;
	public float NumberOfExplosions;
	
    // Start is called before the first frame update
    void Start()
    {
		for (float i = 0; i < NumberOfExplosions; i++) {
			StartCoroutine(Wait(i));
		}
    }
	
	void spawnExplosion() {
		Instantiate(
			explosion,
			new Vector2(
				this.gameObject.transform.position.x + Random.Range(-0.6f, 0.6f), 
				this.gameObject.transform.position.y + Random.Range(-0.6f, 0.6f)
			),
			Quaternion.identity
		);
	}

	IEnumerator Wait(float t) {
		yield return new WaitForSeconds(t * 0.2f);
		spawnExplosion();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
