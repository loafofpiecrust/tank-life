using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float damage = 5.0f;

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
			other.health -= damage;
		}

		// destroy self
		GameObject.Destroy (gameObject);
	}
}
