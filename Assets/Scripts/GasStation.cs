//Jared

using UnityEngine;
using System.Collections;

public class GasStation : MonoBehaviour {

	float waitTime = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float GetWaitTime(){
			return waitTime;
		}

	void SetWaitTime(float xtime){
			waitTime = xtime;
		}

	IEnumerator OnCollisionEnter(Collision c){
		Player p = c.gameObject.GetComponent<Player> ();
		if (p) {
			//increase fuel
			p.fuel*=2f;
			//set velocity to 0
			p.rigidbody.velocity.Set(0f,0f,0f);
			//save old acceleration and turn values and set to 0 so player can't start moving
			float oldMoveSpeed = p.moveSpeed;
			p.moveSpeed = 0f;
			float oldTurnSpeed = p.moveSpeed;
			p.turnSpeed = 0f;
			//wait for waitTime
			yield return new WaitForSeconds(waitTime);
			//return acceleration values to original
			p.moveSpeed = oldMoveSpeed;
			p.turnSpeed = oldTurnSpeed;
		}
	}
}
