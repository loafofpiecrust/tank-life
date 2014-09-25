using UnityEngine;
using System.Collections;

public class Flag : Pickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect (Player p){
		return false;
	}
}
