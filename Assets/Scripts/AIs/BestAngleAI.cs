using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stuff;

namespace ScottAI
{
	public class BestAngleAI : AI 
	{		
		public enum AIState
		{
			Walking,
			Navigating,
			Turning
		}
		private Vector3 nearestPlayer = Vector3.zero;
		private float turnTime = 0.0f;
		private AIState currentState = AIState.Walking;
		
		public override void StepLogic() {
			
			if (nearestPlayer != Vector3.zero) {
				StopMoving();
				TurnCannonTo (nearestPlayer);
				player.FireCannon ();
				nearestPlayer = Vector3.zero;
			}
			else
			{ 
				if(currentState == AIState.Walking)
				{
					if(IsBlocked(wallsLayer,transform.up, 2.0f, .5f))
					{
						StopMoving();
						if(rigidbody.velocity.magnitude <= 0.01f)
						{
							currentState = AIState.Navigating;
							TurnToBestRotation();
						}
					}
					else
					{
						MoveForward(300.0f);
					}
				}
				if(currentState == AIState.Navigating)
				{
					if(turnTime >= 1.0f)
					{
						currentState = AIState.Walking;
					}
				}
			}
			turnTime+=stepLength;
		}
		private void TurnToBestRotation()
		{
			bool found = false;
			int distance = 50;
			float angle = -90;	
			while(distance >3)
			{		
				angle = -90;	
				while(angle <=90)
				{
					Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.up;
					if(!IsBlocked(wallsLayer,direction, distance))
					{
						found = true;
						break;
					}
					angle+= 10;
				}
				if(found)
					break;
				distance -= 5;
			}
			turnTime = 0;
			if(found)
			{
				Turn(angle*-1);
			}
			else
			{
				Turn (145);
			}
		}
		private void HandleSeeingAPlayer(Transform player)
		{
			//We only see ourselves
			if(player == transform)
				return;
				
			nearestPlayer = player.transform.position;
			if (nearestPlayer != Vector3.zero) {
				StopMoving();
				TurnCannonTo (nearestPlayer);
			}
		}
		public override void SeeObject(Transform other) {				
			if (other.GetComponent<Player> ())
				HandleSeeingAPlayer(other);
		}
		void OnCollisionEnter(Collision col) {
		}
	}
}
