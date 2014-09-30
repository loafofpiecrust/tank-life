using UnityEngine;
using System.Collections;

public class KillWin : Pickup {

	public int minimumRequiredKills;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	internal override bool DoEffect(Player p){
		if(p.kills >= minimumRequiredKills){
			Application.LoadLevel(++levelCount);
			stayingAlive = true;
			stayingOut = true;
			return false;
		}
		else{
			return true;
		}
	}
}
