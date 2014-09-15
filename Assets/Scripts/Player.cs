using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	float armor = 100.0f;
	float health = 100.0f;
	float armorRegen = 3.0f;
	float fuel = 50.0f;

	public float moveSpeed = 200.0f;
	public float jumpForce = 400.0f;
	public float turnSpeed = 10.0f;

	bool onGround = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (armor < 100.0f) {
			armor += armorRegen * Time.deltaTime;
		}

		if(fuel > 0.0f) {
			float hor = Input.GetAxis ("Horizontal");
			float ver = Input.GetAxis ("Vertical");

			rigidbody.AddForce(transform.forward * (ver*moveSpeed));
			transform.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
		}


		if (Input.GetButtonDown ("Jump") && onGround) {
			rigidbody.AddForce (new Vector3(0.0f, jumpForce, 0.0f));
			onGround = false;
		}

	}

	void OnCollisionEnter(Collision collision) {
		onGround = true;
	}
}
