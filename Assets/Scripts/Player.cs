using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private float currSpeed = 0.0f;
	private float moveTime = 0.0f;
	private float currAngle = 0.0f;
	private float turnTime = 0.0f;
	private float currCannonAngle = 0.0f;
	private float cannonTurnTime = 0.0f;

	// Non-User Editable
	internal float armor = 100.0f;
	internal float health = 100.0f;
	internal float armorRegen = 3.0f;
	internal float armorRegenBonus = 0.0f;
	internal float regenTime = 0.0f;
	internal float fuel = 50.0f;
	internal float maxFuel = 100.0f;
	internal int kills;
	internal int flags;

	internal int maxAmmo = 100;
	internal int ammo = 0;


	//User Editable
	public float moveSpeed = 100.0f;
	public float maxMoveSpeed = 4.0f;
	public float jumpForce = 400.0f;
	public float turnSpeed = 10.0f;
	public GameObject inv;
	public GameObject bullet;
	public float bulletSpeed = 10.0f;
	bool onGround = true;
	public Transform cannon = null;

	// Use this for initialization
	void Start () {
		ammo = maxAmmo;
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

		if (currSpeed > 0.0f && fuel > 0.0f && moveTime > 0.0f) {
			Debug.Log ("speed: "+currSpeed);
			rigidbody.AddForce (transform.up * currSpeed);
			moveTime -= Time.deltaTime;
		}
		else {
			moveTime = 0.0f;
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


		if (Input.GetButtonDown ("Jump") && onGround) {
			rigidbody.AddForce (new Vector3(0.0f, jumpForce, 0.0f));
			onGround = false;
		}

		if (cannon && currCannonAngle != cannon.eulerAngles.z && cannonTurnTime > 0.0f) {
			float dir = Mathf.Sign (cannon.eulerAngles.z - currCannonAngle);
			cannon.Rotate (new Vector3 (0.0f, 0.0f, dir * turnSpeed * Time.deltaTime));
			cannonTurnTime -= Time.deltaTime;
		}

		if (currAngle != transform.eulerAngles.z && turnTime > 0.0f) {
			float dir = Mathf.Sign(transform.eulerAngles.z - currAngle);
			transform.Rotate (new Vector3(0.0f, 0.0f, dir * turnSpeed * Time.deltaTime));
			turnTime -= Time.deltaTime;
		}

		if (Input.GetButtonDown ("Fire")) {
			FireCannon();
		}
	}

	void OnCollisionEnter(Collision collision) {
		onGround = true;
	}

	public void FireCannon() {
		if(!cannon || !bullet || ammo <= 0) return;
		Vector3 pos = cannon.position + (cannon.up * 1.0f);
		pos.z = -1.0f;
		GameObject obj = Object.Instantiate (bullet, pos, Quaternion.identity) as GameObject;
		Bullet b = obj.GetComponent<Bullet>();
		b.player = this;
		obj.rigidbody.AddForce (cannon.up * bulletSpeed*100.0f);
		this.rigidbody.AddExplosionForce (bulletSpeed*1500.0f, pos, 10.0f);
		ammo--;
	}

	public void MoveForward(float time) {
		currSpeed = moveSpeed;
		moveTime = time;
	}

	public void MoveBackwards(float time) {
		currSpeed = -moveSpeed;
		moveTime = time;
	}

	public void StopMoving() {
		moveTime = 0.0f;
		currSpeed = 0.0f;
	}

	public void Turn(float deg, float time) {
		currAngle = deg;
		turnTime = time;
	}

	public void TurnCannon(float deg, float time) {
		currCannonAngle = deg;
		cannonTurnTime = time;
	}

	public bool IsTurned() {
		return cannon.eulerAngles.z == currCannonAngle;
	}

	public bool IsMoved() {
		return moveTime <= 0.0f;
	}
}
