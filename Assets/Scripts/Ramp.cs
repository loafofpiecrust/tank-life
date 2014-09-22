//Jared

using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		Player p = c.gameObject.GetComponent<Player> ();
		if (p) {
			p.moveSpeed*=.5f;
			}
	}

	void OnCollisionExit(Collision c){
		Player p = c.gameObject.GetComponent<Player> ();
		if (p) {
			p.moveSpeed*=2f;
		}
	}
}
