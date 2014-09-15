using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	float armor = 100.0f;
	float health = 100.0f;
	float armorRegen = 3.0f;
	float fuel = 50.0f;

	public float moveSpeed = 200.0f;
	public float jumpForce = 400.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (armor < 100.0f) {
			armor += armorRegen * Time.deltaTime;
		}

		if(fuel>0.0f){

			float hor = Input.GetAxis ("Horizontal");
			float ver = Input.GetAxis ("Vertical");

			rigidbody.AddForce(new Vector3(hor, ver, 0.0f));

		}


		if (Input.GetButtonDown ("Jump")) {
			rigidbody.AddForce (new Vector3(0.0f, jumpForce, 0.0f));	
		}

	}
}
