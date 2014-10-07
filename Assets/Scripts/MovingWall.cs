using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	private Vector3 movement = Vector3.left * 0.01f;
	
	public void Update() {
		if (transform.position.x > 10)
			movement = Vector3.left * 0.01f;
		else if (transform.position.x < -10)
			movement = Vector3.right * 0.01f;
		
		transform.Translate(movement);
	}
}
