using UnityEngine;
using System.Collections;

namespace Stuff {

public class AmmoP : Pickup {

	internal int amount = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p) {
		p.ammo += amount;
		return true;
	}
}

}