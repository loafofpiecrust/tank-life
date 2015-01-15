
using System;
using UnityEngine;
using Stuff;
using System.Collections.Generic;

public class SneadAI : AI {

	public enum Priority {
		Kill,
		Health,
		Fuel,
		Win
	}

	List<Priority> priority = new List<Priority> {Priority.Win, Priority.Kill, Priority.Health, Priority.Fuel};
	Transform nearestPlayer = null;
	float nearestPlayerDist = 1000.0f;

	Transform destination = null;

	Vector3 lastGoodDir = Vector3.up;

	private void SetPriority(Priority target, int idx) {
		priority.Remove (target);
		priority.Insert (idx, target);
	}

	private bool IsTopPriority(Priority p) {
		return priority [0] == p;
	}

	private bool IsHighPriority(Priority p) {
		return priority [0] == p || priority [1] == p;
	}

	public override void StepLogic() {
		// things ais need to do
		/*
		 * look for goals and prioritize between them
		 */

		bool front = IsBlocked (wallsLayer, transform.up);
		bool right = IsBlocked (wallsLayer, transform.right);
		bool left = IsBlocked (wallsLayer, -transform.right);

		if (front) {
			StopMoving ();
			if(right && left) {
				MoveBackwards ();
			} else if(right) {
				Turn (-25);
			} else {
				Turn (25);
			}
		} else {
			MoveForward ();
		}

		float closeDist = 0.6f;
		bool closeRight = IsBlocked (wallsLayer, transform.right, closeDist);
		bool closeLeft = IsBlocked (wallsLayer, -transform.right, closeDist);

		if (closeRight && !closeLeft) {
			Turn (-5);
		} else if (closeLeft && !closeRight) {
			Turn (5);
		}

		if (destination) {
			TurnTo (destination.position);
		}
		if (nearestPlayer) {
			TurnCannonTo (nearestPlayer.position);
			player.FireCannon ();
		}
	}

	public override void SeeObject(Transform other) {
		Player pl = other.GetComponent<Player> ();
		if (pl) {
			float dist = Vector3.Distance (other.position, transform.position);
			if(dist <= nearestPlayerDist && (IsHighPriority(Priority.Kill) || IsHighPriority(Priority.Health) || dist <= dangerRadius)) {
				nearestPlayerDist = dist;
				nearestPlayer = other;
				destination = other;
			}
		}

		Pickup pu = other.GetComponent<Pickup> ();
		if (pu) {
			float dist = Vector3.Distance (other.position, transform.position);
			FuelP fuel = other.GetComponent<FuelP>();
			Flag flag = other.GetComponent<Flag>();
			FlagWin win = other.GetComponent<FlagWin>();
			Mine mine = other.GetComponent<Mine>();
			if(dist <= dangerRadius
			   || (fuel && IsTopPriority (Priority.Fuel))
			   || ((flag || win) && IsTopPriority (Priority.Win))
			   || (mine && IsTopPriority (Priority.Kill) && destination != nearestPlayer)
			   ) {
				destination = other;
			}
		}
	}

	private void AvoidWalls() {

	}
}