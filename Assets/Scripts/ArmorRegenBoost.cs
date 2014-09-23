using UnityEngine;
using System.Collections;

public class ArmorRegenBoost : Pickup {

	public int time;
	public float amount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p){
		p.armorRegenBonus = amount;
		p.regenTime = time;
		return false;
	}
}
