using UnityEngine;
using System.Collections;

public class FlagWin : Pickup {

	public int minimumFlagsRequired;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect (Player p){
		
		stayingAlive=true;
		if((p.GetComponentInChildren<Flag>() is Flag
		   && p.flags >= minimumFlagsRequired) 
		   && p.neededWins == 1){
			Application.LoadLevel("Lv" + (1 + levelCount));
		}
		else if (p.GetComponentInChildren<Flag>() is Flag
		         && p.flags >= minimumFlagsRequired){
			stayingOut=true;
			p.neededWins -= 1;
		}
		else{
			stayingOut = true;
		}
		return false;
	}
}
