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
		stayingOut=true;
		stayingAlive=true;
		if(p.GetComponentInChildren<Flag>() is Flag
		   && p.flags >= minimumFlagsRequired){
			Debug.Log("You Win!");
			Application.LoadLevel(++levelCount);
		}
		return false;

	}
}
