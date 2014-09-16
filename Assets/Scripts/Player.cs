using UnityEngine;
using System.Collections;

internal class Player : MonoBehaviour {

	internal float armor = 100.0f;
	internal float health = 100.0f;
	internal float armorRegen = 3.0f;
	internal float armorRegenBonus = 0.0f;
	internal float regenTime = 0.0f;
	internal float fuel = 50.0f;

	public float moveSpeed = 200.0f;
	public float jumpForce = 400.0f;
	public float turnSpeed = 10.0f;

	public GameObject bullet;
	public float bulletSpeed = 10.0f;

	bool onGround = true;
	Transform cannonHolder = null;
	Transform cannon = null;
	Transform cannonBarrel = null;

	// Use this for initialization
	void Start () {
		cannonHolder = transform.FindChild ("CannonHolder");
		cannon = cannonHolder.FindChild("Cannon");
		if (cannon) {
			cannonBarrel = cannon.FindChild ("Barrel");
		}
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
		
		float hor;
		float ver;
		if(fuel > 0.0f) {
			ver = Input.GetAxis ("Vertical");
			hor = Input.GetAxis ("Horizontal");
			rigidbody.AddForce(transform.forward * (ver*moveSpeed));
			transform.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
		}


		if (Input.GetButtonDown ("Jump") && onGround) {
			rigidbody.AddForce (new Vector3(0.0f, jumpForce, 0.0f));
			onGround = false;
		}

		if (cannon) {
			hor = Input.GetAxis ("CannonHorizontal");
			ver = Input.GetAxis ("CannonVertical");
			cannon.Rotate (new Vector3(0.0f, 0.0f, ver * turnSpeed * Time.deltaTime));
			cannonHolder.Rotate (new Vector3(0.0f, hor * turnSpeed * Time.deltaTime, 0.0f));
		//	cannonBarrel.RotateAround (cannon.position, new Vector3(ver, hor, 0.0f), turnSpeed * Time.deltaTime);
			cannon.eulerAngles = new Vector3(
				cannon.eulerAngles.x,
			//	Mathf.Clamp(cannon.rotation.eulerAngles.y, 1.0f, 90.0f),
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
}
