using UnityEngine;
using System.Collections;
namespace Stuff{
public class KillWin : Pickup {

	public int minimumRequiredKills;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	internal override bool DoEffect(Player p){
		
		stayingAlive=true;
		if((p.kills >= minimumRequiredKills)
		   && p.neededWins == 1){
			Application.LoadLevel(++levelCount);
		}
		else if(p.kills >= minimumRequiredKills){
			p.neededWins -= 1;
			stayingOut = true;
		}
		else{
			stayingOut = true;
		}
		return true;
	}
}
}