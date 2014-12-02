using UnityEngine;
using System.Collections;
using Stuff;

public class SimpleAI : AI {

	private bool wallOnRight = false;
	private float nearestPlayerDist = 100.0f;
	private Player nearestPlayer = null;

	public override void StepLogic() {
		rigidbody.inertiaTensor = new Vector3(1, 1, 1);
	
		bool rightBlocked = IsBlocked (wallsLayer, transform.right);
		
		if (!rightBlocked && IsMoved ()) {
			Turn (40);
			MoveForward (2.0f);
		}

		if (IsBlocked (wallsLayer, transform.up)) {
			Debug.Log ("forward is blocked");
			float turnAngle = 20;
			StopMoving (0.25f);
			if(!IsBlocked (wallsLayer, -transform.right)) {
				Turn(-turnAngle);
			}
			else {
				Turn(turnAngle);
			}
		} else if(IsMoved()) {
			MoveForward (stepLength*2);
		}

		if (!wallOnRight && !rightBlocked && IsMoved ()) {
			StopMoving (0.2f);
			Turn (15);
		}
		if (!wallOnRight && rightBlocked) {
			wallOnRight = true;
		} else if (wallOnRight && !rightBlocked) {
			StopMoving (0.2f);
			Turn (70);
			wallOnRight = false;
		}

		if (nearestPlayer) {
			Debug.Log ("VISIBLE PLAYER!! at "+nearestPlayer.transform.position);
			TurnCannonTo (nearestPlayer.transform.position);
			nearestPlayer = null;
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
