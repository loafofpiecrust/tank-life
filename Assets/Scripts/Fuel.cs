using UnityEngine;
using System.Collections;

public class Fuel : Pickup{

	public float amount;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override void DoEffect(Player p){
		p.fuel += amount;
	}

}
