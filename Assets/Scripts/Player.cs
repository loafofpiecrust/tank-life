using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

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

	internal float reloadTime = 0.2f;
	private float currReload = 0.0f;

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
	public float bulletForce = 100.0f;
	bool onGround = true;
	public Transform cannon = null;
	public Transform cannonBarrel = null;

	public ParticleSystem cannonEffect = null;
	public int neededWins;


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
			die();
			Destroy(gameObject);
		}

		if (currReload > 0.0f) {
			currReload -= Time.deltaTime;
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


		if (Input.GetButtonDown ("Fire")) {
			FireCannon();
		}

	}

	void OnCollisionEnter(Collision collision) {
		onGround = true;
	}

	public void FireCannon() {
		if(!cannonBarrel || !bullet || ammo <= 0 || currReload > 0.0f) return;

		Vector3 pos = cannonBarrel.position + (cannonBarrel.up * 1.0f);
		pos.z = -.3f;
		GameObject obj = Object.Instantiate (bullet, pos, Quaternion.identity) as GameObject;
		Bullet b = obj.GetComponent<Bullet>();
		b.player = this;
		obj.rigidbody.AddForce (cannonBarrel.up * bulletSpeed);
		this.rigidbody.AddExplosionForce (bulletForce, pos, 10.0f);
		ammo--;
		currReload = reloadTime;

		if (cannonEffect) {
			cannonEffect.Emit (50);
		}
	}

	public void Implode() {
		for(int i = 0; i < transform.childCount; ++i) {
			GameObject child = transform.GetChild (i).gameObject;
			child.transform.parent = null;
			if(!child.rigidbody) {
				child.AddComponent<Rigidbody>();
			}
			child.rigidbody.useGravity = false;
			child.rigidbody.AddExplosionForce (100.0f, transform.position, 3.0f);
		}
	}

	public void die(){
		Component flag = inv.GetComponentInChildren<Flag>();
		if ( flag is Flag){
			flag.transform.parent = null;
			flag.collider.enabled = true;
			flag.renderer.enabled = true;
		}
		Destroy(this.gameObject);
	}

	internal void BurnFuel() {
		fuel -= rigidbody.velocity.magnitude * Time.deltaTime;
	}


}
