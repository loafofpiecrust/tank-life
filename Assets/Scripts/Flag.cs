using UnityEngine;
using System.Collections;
namespace Stuff{
public class Flag : Stuff.Pickup {

	//public int removeHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal override bool DoEffect (Player p){
		Debug.Log("Flag Here!");
		//p.health -= removeHealth;
		p.flags++;
		return false;
	}
}

}