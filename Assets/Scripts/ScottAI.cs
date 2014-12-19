using UnityEngine;
using System.Collections;
using Stuff;

public class ScottAI : AI {

	private bool wallOnRight = false;
	private float nearestPlayerDist = 100.0f;
	private Player nearestPlayer = null;
	
	public override void StepLogic() {
	
		
		if (nearestPlayer) {
			StopMoving();
			Debug.Log ("VISIBLE PLAYER!! at "+nearestPlayer.transform.position);
			TurnCannonTo (nearestPlayer.transform.position);
			player.FireCannon ();
			nearestPlayer = null;
		}
		else
		{
			MoveForward (10.0f);
		}
	}
	
	public override void SeeObject(Transform other) {
		Player pl = other.GetComponent<Player> ();
		if (pl) {
			Debug.Log ("we see a player");
			if(Vector3.Distance (transform.position, other.position) < nearestPlayerDist) {
				Debug.Log ("NEAREST VISIBLE PLAYER IS AT "+other.position);
				nearestPlayer = other.GetComponent<Player>();
			}
		}
	}
	
	void OnCollisionEnter(Collision col) {
		if (IsBlocked (wallsLayer, transform.up)) {
			Debug.Log ("WE HIT A WALL AHEAD");
			float turnAngle = 20;
			MoveBackwards (0.25f);
			if(!IsBlocked (wallsLayer, -transform.right)) {
				Turn(-turnAngle);
			}
			else {
				Turn(turnAngle);
			}
		} else if (IsBlocked (wallsLayer, transform.right)) {
			Turn (-5);
		} else if (IsBlocked (wallsLayer, -transform.right)) {
			Turn (5);
		}
	}
}
