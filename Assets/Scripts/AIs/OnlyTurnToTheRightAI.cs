using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stuff;

namespace ScottAI
{
	public class OnlyTurnToTheRightAI : AI 
	{		
		private Vector3 nearestPlayer = Vector3.zero;
		
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
				if(IsBlocked(wallsLayer,transform.up, 3.0f))
				{
					StopMoving();
					if(rigidbody.velocity.magnitude <= 0.01f)
					{
						Turn(Random.Range(5,30));
					}
				}
				else
				{
					MoveForward(300);
				}
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
