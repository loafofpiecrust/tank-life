using UnityEngine;
using System.Collections;
namespace Stuff {
	public class Bullet : MonoBehaviour {

		public float lifeTime = 10.0f;
		public float damage = 5.0f;
		internal Player player;

		// Use this for initialization
		void Start () {
			GameObject.Destroy (gameObject, lifeTime);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionEnter(Collision col) {
			// Do Damage
			Player other = col.gameObject.GetComponent<Player>();
			if (other) {
				particleSystem.Play();

				other.rigidbody.AddForceAtPosition (Vector3.Normalize (rigidbody.velocity) * player.bulletForce * 0.6f, col.contacts[0].point);
				if (other.health <= damage) {
					other.health -= damage;
					player.kills++;
				}
				else {
					other.health -= damage;
				}
				// Self-destruct
				renderer.enabled = false;
				collider.enabled = false;
				GameObject.Destroy (gameObject, 1.0f);
			}
		}
	}
}