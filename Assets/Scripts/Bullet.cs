using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float damage = 5.0f;
	internal Player player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		// cause damage
		Player other = col.gameObject.GetComponent<Player>();
		if (other) {
			if (other.health <= damage){
				other.health -=damage;
				player.kills++;
			}
			else{
			other.health -= damage;
			}
		}
		// destroy self
		GameObject.Destroy (gameObject);
	}
}
