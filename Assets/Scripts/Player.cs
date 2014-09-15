using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float armor = 100.0f;
	float health = 100.0f;
	float armorRegen = 3.0f;
	float fuel = 50.0f;

	float moveSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (armor < 100.0f) {
			armor += armorRegen * Time.deltaTime;
		}

		if(fuel>0){

		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		rigidbody.AddForce(new Vector3(hor, ver, 0.0f));

		}


	}
}
