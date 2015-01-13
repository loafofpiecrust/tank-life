using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Stuff;

namespace ScottAI
{
	public class ScottAI : AI 
	{					
		
		private float nearestPlayerDist = 100.0f;
		private Vector3 nearestPlayer = Vector3.zero;
		
		public override void StepLogic() {
			
			if (nearestPlayer != Vector3.zero) {
				TurnCannonTo (nearestPlayer);
				player.FireCannon ();
				nearestPlayer = Vector3.zero;
			}
			else
			{
				Vector3 forward = transform.localToWorldMatrix.MultiplyVector(Vector3.up);
				Vector3 left = transform.localToWorldMatrix.MultiplyVector(Vector3.left);
				Vector3 right = transform.localToWorldMatrix.MultiplyVector(Vector3.right);
				if(IsBlocked(wallsLayer,forward,3.0f)
				   || IsBlocked (wallsLayer,left,1.0f)
				   || IsBlocked (wallsLayer,right,1.0f))
				{
					StopMoving();
					if(rigidbody.velocity.magnitude <= 0.01f)
					{
						if(!IsBlocked (wallsLayer,left,5.0f))
							Turn(-15);
						else
						   Turn(15);
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
				
			//nearestPlayer = player.transform.position;
			Vector3 direction =  player.transform.position - transform.position;
			if(!IsBlocked(wallsLayer, direction, direction.magnitude))
			{
				nearestPlayer = player.transform.position;
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
