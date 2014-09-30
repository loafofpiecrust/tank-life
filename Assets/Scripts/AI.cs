using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public abstract class AI : MonoBehaviour {
	
	public float stepLength = 0.01f;
	private float currStep = 0.0f;
	
	public float visibleRadius = 5.0f;
	public float dangerRadius = 1.0f;
	
	protected Player player;



	public abstract void StepLogic();

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
	
	public Player GetNearestVisiblePlayer() {
		Transform obj = GetNearestVisiblePriority ();
		if (obj) {
			return obj.GetComponent<Player>();
		}
		return null;
	}
	
	public Transform GetNearestVisiblePriority() {
		RaycastHit hit;
		bool cast = Physics.SphereCast (transform.position, visibleRadius, transform.forward, out hit, visibleRadius);
		if (cast) {
			return hit.transform;
		}
		return null;
	}
	
	public bool IsBlocked(Vector3 inDir) {
		Vector3 startBase = transform.position + (inDir * 0.5f);
		Vector3 start1 = startBase + (Vector3.Cross (inDir, transform.forward) * 0.5f);
		Vector3 start2 = startBase - (Vector3.Cross (inDir, transform.forward) * 0.5f);
		return Physics.Raycast (start1, inDir, dangerRadius) || Physics.Raycast (start2, inDir, dangerRadius);
	}
}
