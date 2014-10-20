using UnityEngine;
using System.Collections;

public class SimpleAI : AI {

	private bool wallOnRight = false;

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

		Transform p = GetNearestVisibleThing(playersLayer);
		if (p) {
			Debug.Log ("VISIBLE PLAYER!! at "+p.position);
			TurnCannonTo (p.position);
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
