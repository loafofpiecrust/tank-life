using UnityEngine;
using System.Collections;

public class Mine : Pickup{

	public int dmg;
	public int colCount;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p){
		if (colCount == 0){
			this.collider.enabled = false;
			this.renderer.enabled = false;
			colCount++;
			return true;
		}
		else {
			p.health -=dmg;
			return false;
		}
	}
}
