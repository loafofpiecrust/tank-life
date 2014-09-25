using UnityEngine;
using System.Collections;

public class FlagWin : Pickup {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect (Player p){
		if(p.GetComponentInChildren<Flag>() is Flag){
			//Debug.Log("You Win!");
			Application.LoadLevel(1);
		}
		return true;
	}
}
