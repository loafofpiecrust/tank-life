using UnityEngine;
using System.Collections;
using Stuff;

public class ScottAI : AI {


	enum State
	{
		ST_FirstFire,
		ST_Turn,
		ST_Move,
		ST_Brake,
		ST_Stop
	}

	State currentState;
	float currentStateTime = 0;
	float lastTime = 0;

	int shotsFired = 0;
	
	float stepTime = 1;

	public override void StepLogic() {

		currentStateTime+= Time.timeSinceLevelLoad - lastTime;
		lastTime = Time.timeSinceLevelLoad;
		
		/*Transform enemy = GetNearestVisibleThing(playersLayer);
		if (enemy != null) {
			Debug.Log ("VISIBLE PLAYER!! at "+enemy.position);
			TurnCannonTo (enemy.position);
			player.FireCannon();
		}*/

		if(currentState == State.ST_FirstFire)
		{
			if(transform.position.x < 0)
				TurnCannonTo (new Vector3(14.35922f, -13.00012f, 24.71606f));
			else
				TurnCannonTo (new Vector3(-14.44455f, 15.80382f, 24.81667f));
			shotsFired++;
			player.FireCannon();
			if(shotsFired >5)
			{
				currentState = State.ST_Turn;
				currentStateTime=0;
			}
		}
		if(currentState == State.ST_Turn)
		{
			Turn(45.0f);
			stepTime = Random.Range(2,5);
			MoveForward(stepTime);
			currentState = State.ST_Move;
			currentStateTime=0;
		}
		if(currentState == State.ST_Move)
		{
			if(currentStateTime >= stepTime)
			{
				MoveForward(-1*stepTime);
				currentState = State.ST_Brake;
				currentStateTime = 0;
			}
		}
		if(currentState == State.ST_Brake)
		{
			/*if(currentStateTime >= stepTime
			   && enemy == null)
			{
				currentState = State.ST_Turn;
				currentStateTime = 0;
			}*/
		}
		//MoveForward (2.0f);
		//GameObject.fin
		//TurnCannonTo (new Vector3(12.31893f,1.400977f,24.94851f));

		

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
