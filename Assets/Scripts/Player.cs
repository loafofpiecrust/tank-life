using UnityEngine;
using System.Collections;

namespace Stuff {
	
	[RequireComponent (typeof (BoxCollider))]
	public class Player : MonoBehaviour {

		//Stats of the Player
		internal float armor = 100.0f;
		internal float health = 100.0f;
		internal float armorRegen = 3.0f;
		internal float armorRegenBonus = 0.0f;
		internal float regenTime = 0.0f;
		internal float fuel = 50.0f;
		internal float maxFuel = 100.0f;
		
		//Stuff for Winning	
		public GameObject inv;
		internal int kills;
		internal int flags;
		public int neededWins;
		
		//Stuff for Shooting
		internal float reloadTime = 0.4f;
		private float currReload = 0.0f;
		internal int maxAmmo = 20;
		internal int ammo = 0;

		public GameObject bullet;

		private float bulletSpeed = 800.0f;
		internal float bulletForce = 100.0f;
		private float bulletDamage = 15.0f;

			
		public Transform cannon = null;
		public Transform cannonBarrel = null;
		public ParticleSystem cannonEffect = null;
		
		//Stuff for Moving
		internal float moveSpeed = 100.0f;
		internal float maxMoveSpeed = 4.0f;
		internal float turnSpeed = 150.0f;


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
				Die();
			}

			if (currReload > 0.0f) {
				currReload -= Time.deltaTime;
			}
		}

		internal void FireCannon() {
			if(!cannonBarrel || !bullet || ammo <= 0 || currReload > 0.0f) return;

			Vector3 pos = cannonBarrel.position + (cannonBarrel.up * 1.0f);
			//pos.z = -.3f;
			GameObject obj = Object.Instantiate (bullet, pos, cannonBarrel.rotation) as GameObject;
			Bullet b = obj.GetComponent<Bullet>();
			b.damage = bulletDamage;
			b.player = this;
			obj.rigidbody.AddForce (cannonBarrel.up * bulletSpeed);
			this.rigidbody.AddExplosionForce (bulletForce, pos, 10.0f);
			ammo--;
			currReload = reloadTime;

			if (cannonEffect) {
				cannonEffect.Emit (50);
			}
		}

		private void Implode() {
			for(int i = 0; i < transform.childCount; ++i) {
				GameObject child = transform.GetChild (i).gameObject;
				if(!child.rigidbody) {
					child.AddComponent<Rigidbody>();
				}
				if(!child.collider) {
					BoxCollider bc = child.AddComponent<BoxCollider>();
					bc.size = new Vector3(bc.size.x, bc.size.y, 0.25f);
				}
				child.rigidbody.AddExplosionForce (1000.0f, transform.position - transform.forward*2.0f, 3.0f);
				Destroy (child, 5.0f);
			}
		}

		internal void Die(){
			Flag f = inv.GetComponentInChildren<Flag> ();
			if(f) f.Drop ();
			Implode ();
			Destroy(this.gameObject, 3.0f);
		}

		internal void BurnFuel() {
			fuel -= rigidbody.velocity.magnitude * Time.deltaTime;
			if (fuel <= 0.0f) {

			}
		}
	}
}
