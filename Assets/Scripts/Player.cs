using UnityEngine;
using System.Collections;

internal class Player : MonoBehaviour {
	// Non-User Editable
	internal float armor = 100.0f;
	internal float health = 100.0f;
	internal float armorRegen = 3.0f;
	internal float armorRegenBonus = 0.0f;
	internal float regenTime = 0.0f;
	internal float fuel = 50.0f;
	internal float maxFuel = 100.0f;


	//User Editable
	public float moveSpeed = 100.0f;
	public float maxMoveSpeed = 4.0f;
	public float jumpForce = 400.0f;
	public float turnSpeed = 10.0f;
	public GameObject inv;
	public GameObject bullet;
	public float bulletSpeed = 10.0f;
	bool onGround = true;
	public Transform cannonHolder = null;
	public Transform cannon = null;
	public Transform cannonBarrel = null;
	public Transform frontAxis;
	public WheelCollider wheelTL;
	public WheelCollider wheelTR;
	public WheelCollider wheelBL;
	public WheelCollider wheelBR;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (armor < 100.0f) {
			armor += (armorRegen + armorRegenBonus) * Time.deltaTime;
		}

		if (regenTime > 0.0f) {
			regenTime -= Time.deltaTime;
		} else if (regenTime == 0.0f) {
			regenTime = -1.0f;
			armorRegenBonus = 0.0f;
		}

		if(health<=0.0f){
			Destroy(this);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			Mine mine = inv.GetComponentInChildren<Mine>();
			if(mine){
				mine.transform.parent = null;
				mine.transform.Translate (-transform.forward * 3.0f);
				mine.renderer.enabled = true;
				mine.collider.enabled = true;
			}
		}

		
		float hor;
		float ver;
		if(fuel > 0.0f) {
			ver = Input.GetAxis ("Vertical");
			hor = Input.GetAxis ("Horizontal");

<<<<<<< HEAD
	
			if (ver != 0.0f) {
				wheelTR.motorTorque = ver * moveSpeed;
				wheelTL.motorTorque = ver * moveSpeed;
				fuel -= Time.deltaTime;
=======
		/*	if (rigidbody.velocity.x < maxMoveSpeed) {
				wheelTR.AddForce(new Vector3(transform.forward.x * (ver*moveSpeed), 0.0f, 0.0f));
				wheelTL.AddForce(new Vector3(transform.forward.x * (ver*moveSpeed), 0.0f, 0.0f));
			}*/
			if (Input.GetButtonDown ("Vertical") && ver != 0.0f) {
				ResetWheels ();
				StartAllWheels(ver);
				fuel -= maxMoveSpeed * 0.1f * Time.deltaTime;
>>>>>>> 3a51010c42e363ece09c997ad24e2daf5100c23b
			}
			else if(Input.GetButtonUp ("Vertical")) {
			//	StopAllWheels();
			//	ResetWheels ();
			}

<<<<<<< HEAD
			if(hor != 0.0f) {
				frontAxis.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
=======
		//	if(hor != 0.0f) transform.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
			if(Input.GetButtonDown ("Horizontal") && hor > 0.0f) {
			//	frontAxis.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
				ResetWheels ();
				StartWheel (wheelTL, 0.5f);
				StartWheel (wheelBL, 0.5f);
				StartWheel (wheelTR, -0.5f);
				StartWheel (wheelBR, -0.5f);
			}
			else if(Input.GetButtonDown ("Horizontal") && hor < 0.0f) {
				ResetWheels ();
				StartWheel (wheelTL, -0.5f);
				StartWheel (wheelBL, -0.5f);
				StartWheel (wheelTR, 0.5f);
				StartWheel (wheelBR, 0.5f);
			}
			else if(Input.GetButtonUp ("Horizontal")) {
			//	StopAllWheels();
>>>>>>> 3a51010c42e363ece09c997ad24e2daf5100c23b
			}
		}


		if (Input.GetButtonDown ("Jump") && onGround) {
			rigidbody.AddForce (new Vector3(0.0f, jumpForce, 0.0f));
			onGround = false;
		}

		if (cannon) {
			hor = Input.GetAxis ("CannonHorizontal");
			ver = Input.GetAxis ("CannonVertical");
			cannon.Rotate (new Vector3(ver * turnSpeed * Time.deltaTime, 0.0f, 0.0f));
			cannonHolder.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
			cannon.eulerAngles = new Vector3(
				cannon.eulerAngles.x,
				cannon.eulerAngles.y,
				Mathf.Clamp(cannon.eulerAngles.z, 1.0f, 45.0f)
			);

		}

		if (Input.GetButtonDown ("Fire")) {
			FireCannon ();
		}
	}
	void OnCollisionEnter(Collision collision) {
		onGround = true;
	}

	void FireCannon() {
		if(!cannonBarrel || !bullet) return;
 		GameObject obj = Object.Instantiate (bullet, cannonBarrel.position, Quaternion.identity) as GameObject;
		Rigidbody rb = obj.AddComponent<Rigidbody>();
		rb.AddForce (cannonBarrel.up * bulletSpeed);
	}

	void StartWheel(WheelCollider w, float mult = 1.0f) {
		w.motorTorque = mult * moveSpeed;
		w.brakeTorque = 0.0f;
	}
	void StopWheel(WheelCollider w, float mult = 1.0f) {
		w.motorTorque = 0.0f;
		w.brakeTorque = mult * moveSpeed;
	}
	void StartAllWheels(float mult = 1.0f) {
		StartWheel (wheelTR, mult);
		StartWheel (wheelTL, mult);
		StartWheel (wheelBR, mult);
		StartWheel (wheelBL, mult);
	}
	void StopAllWheels(float mult = 1.0f) {
		StopWheel (wheelTR, mult);
		StopWheel (wheelTL, mult);
		StopWheel (wheelBR, mult);
		StopWheel (wheelBL, mult);
	}
	void ResetWheels() {
		StopAllWheels (0.0f);
	}
}
