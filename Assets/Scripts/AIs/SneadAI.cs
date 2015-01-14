
using System;
using UnityEngine;
using Stuff;

public class SneadAI : AI {

	public enum Priority {
		Kill,
		Health,
		Fuel,
		Win
	}

	Priority[] priority = new Priority[] {Priority.Win, Priority.Kill, Priority.Health, Priority.Fuel};
	Vector3 nearestPlayer = new Vector3(0.0f, 0.0f, 0.0f);
	float nearestPlayerDist = 1000.0f;

	Transform destination = null;

	public override void StepLogic() {
		// things ais need to do
		/*
		 * look for goals and prioritize between them
		 */
		
	}

	public override void SeeObject(Transform other) {
		Player pl = other.GetComponent<Player> ();
		if (pl) {
			float dist = Vector3.Distance (other.position, transform.position);
			if(dist <= nearestPlayerDist && (priority[0] == Priority.Kill || priority[0] == Priority.Health || dist <= dangerRadius)) {
				nearestPlayerDist = dist;
				nearestPlayer = other.position;
				destination = other;
			}
		}

		Pickup pu = other.GetComponent<Pickup> ();
		if (pu) {
			float dist = Vector3.Distance (other.position, transform.position);
			if(dist <= dangerRadius) {
				destination = other;
			}
		}
	}
}