using UnityEngine;
using System.Collections;

public class SimpleAI : AI {



	public override void StepLogic() {
		if (IsBlocked (transform.up)) {
			float turnAngle = 20;
			player.MoveBackwards (0.3f);
			if(IsBlocked (transform.right)) {
				player.Turn(turnAngle);
			}
			else {
				player.Turn(-turnAngle);
			}
		} else if(player.IsMoved()) {
			player.MoveForward (stepLength);
		}

		Player p = GetNearestVisiblePlayer ();
		if (p) {
			Debug.Log ("VISIBLE PLAYER!! at "+p.transform.position);
			player.TurnCannonTo (p.transform.position);
		}
	}

}
