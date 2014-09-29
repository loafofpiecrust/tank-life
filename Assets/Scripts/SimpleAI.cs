using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class SimpleAI : MonoBehaviour {

	public float stepLength = 0.01f;
	private float currStep = 0.0f;

	public float visibleRadius = 5.0f;

	private Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currStep <= 0.0f) {
			currStep = stepLength;
			StepLogic();
		}
		else {
			currStep -= Time.deltaTime;
		}
	}
	
	void StepLogic() {
		if (IsBlockedAhead ()) {
			player.MoveBackwards (0.1f);
			player.Turn (20, 0.1f);
		} else {
			player.MoveForward (1.0f);
		}
	}

	public Player GetNearestVisiblePlayer() {
		RaycastHit hit;
		bool cast = Physics.SphereCast (transform.position, visibleRadius, transform.forward, out hit, visibleRadius);
		if (cast) {
			return hit.transform.GetComponent<Player>();
		}
		return null;
	}

	public bool IsBlockedAhead() {
	//	return Physics.Raycast (transform.position, transform.forward, visibleRadius);
		return false;
	}

}
