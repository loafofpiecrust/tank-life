using UnityEngine;
using System.Collections;
namespace Stuff{
public class ArmorP : Stuff.Pickup {

	public float amount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p){
		p.armor += amount;
		return false;
	}
}
}