using UnityEngine;
using System.Collections;
using Stuff;

public class MoveShootAI : AI {


	enum State
	{
		ST_Move,
		ST_Brake,
		ST_Turn
	}

	State currentState;

	public override void StepLogic() {

		//MoveForward (2.0f);
		//GameObject.fin
		//TurnCannonTo (new Vector3(12.31893f,1.400977f,24.94851f));

		/*Transform p = GetNearestVisibleThing();
		if (p != null) {
			StopMoving();
			Debug.Log ("VISIBLE PLAYER!! at "+p.position);
			TurnCannonTo (p.position);
			player.FireCannon();
		}
		else
		{
			MoveForward (2.0f);
		}*/

		/*Transform nearestPlayer = GetNearestVisibleThing (playersLayer);

		if(nearestPlayer != null)
			HandlePlayerSeen()

		//rigidbody.inertiaTensor = new Vector3(1, 1, 1);
		
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
		}*/
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
