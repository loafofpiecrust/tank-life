using UnityEngine;
using System.Collections;

public class Mine : Pickup{

	public int dmg;
	internal int colCount = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect(Player p){
		if (colCount == 0){
			colCount++;
			//Debug.Log("Mine collision : " + colCount);
			return false;
		}
		else {
			p.health -=dmg;
			//Debug.Log("Die you fuck");
			return true;
		}
	}
}
