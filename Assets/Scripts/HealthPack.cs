using UnityEngine;
using System.Collections;

public class HealthPackP : Pickup {

	public float amount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p){
		p.health += amount;
		return false;
	}

}
