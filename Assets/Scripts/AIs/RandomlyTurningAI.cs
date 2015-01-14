using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stuff;

namespace ScottAI
{
	public class RandomlyTurningAI : AI 
	{		
		private Vector3 nearestPlayer = Vector3.zero;
		private float turnTime = 0.0f;
		
		public override void StepLogic() {
			
			if (nearestPlayer != Vector3.zero) {
				StopMoving();
				TurnCannonTo (nearestPlayer);
				player.FireCannon ();
				nearestPlayer = Vector3.zero;
			}
			else
			{ 
				Vector3 left = transform.localToWorldMatrix.MultiplyVector(Vector3.left);
				Vector3 right = transform.localToWorldMatrix.MultiplyVector(Vector3.right);
				if(IsBlocked(wallsLayer,transform.up, 2.0f, .5f))
				{
					StopMoving();
					if(rigidbody.velocity.magnitude <= 0.01f
					   && turnTime >= 2.0f)
					{
						int turnLeft = Random.Range(0,2);
						if(turnLeft == 1)
							Turn(Random.Range(45,135));
						else
							Turn(Random.Range(-135,-45));
						turnTime=0.0f;
					}
					
				}
				else
				{
					MoveForward(300);
				}
			}
			turnTime+=stepLength;
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
