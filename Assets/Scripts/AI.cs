using UnityEngine;
using System.Collections;


namespace Stuff {
	[RequireComponent (typeof (Player))]
	[RequireComponent (typeof (SphereCollider))]
	public abstract class AI : MonoBehaviour {

		public float stepLength = 0.1f;
		private float currStep = 0.0f;
		
		public float visibleRadius = 10.0f;
		public float dangerRadius = 1.0f;
		
		protected Player player;

		
		private float currSpeed = 0.0f;
		private float moveTime = 0.0f;
		private float currAngle = 0.0f;
		private float turnTime = 0.0f;
		private float currCannonAngle = 0.0f;
		private float cannonTurnTime = 0.0f;

		protected static readonly int playersLayer = LayerMask.GetMask ("Player");
		protected static readonly int wallsLayer = LayerMask.GetMask ("Wall");
		protected static readonly int pickupsLayer = LayerMask.GetMask ("Pickup");
		protected static readonly int goalsLayer = LayerMask.GetMask ("Win");
		protected static readonly int allLayers = 0xFF;

		public abstract void StepLogic();
		public virtual void SeeObject(Transform other) {}

		// Use this for initialization
		void Start () {
			player = GetComponent<Player> ();
			
			SphereCollider sphere = GetComponent<SphereCollider> ();
			sphere.isTrigger = true;
			sphere.radius = visibleRadius;
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

			
			if (currSpeed != 0.0f && player.fuel > 0.0f && moveTime > 0.0f) {
				rigidbody.AddForce (transform.up * currSpeed);
				moveTime -= Time.deltaTime;
				player.BurnFuel();
			}
			else {
				moveTime = 0.0f;
				currSpeed = 0.0f;
			}

			
			if (player.cannon && (currCannonAngle > 0.5f || currCannonAngle < -0.5f)) {
				float dir = Mathf.Sign (currCannonAngle);
				float amt = dir * player.turnSpeed * Time.deltaTime;
				player.cannon.Rotate (new Vector3 (0.0f, 0.0f, amt));
				currCannonAngle -= amt;
			}
			
			if (!(currAngle < 0.5f && currAngle > -0.5f)) {
				float dir = Mathf.Sign(currAngle);
				float amt = dir * player.turnSpeed * Time.deltaTime;
				transform.Rotate (new Vector3(0.0f, 0.0f, amt));
				currAngle -= amt;
			}
		}

		private void OnTriggerStay(Collider other) {
			RaycastHit hit;
			Vector3 dir = other.transform.position - transform.position;
			Color c = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
			Physics.Raycast (transform.position, dir, out hit);
			Debug.DrawLine (transform.position, hit.collider.transform.position, c);
			if(hit.collider == other) {
				SeeObject (other.transform);
			}
		}

		private void OnTriggerExit(Collider other) {

		}
		
		public bool IsBlocked(int layerMask, Vector3 inDir, float clearance = 0.51f) {
			Vector3 startBase = transform.position + (inDir * clearance);
			Vector3 across = Vector3.Cross (inDir, transform.forward) * clearance;
			Vector3 start1 = startBase + across;
			Vector3 start2 = startBase - across;
			Debug.DrawLine(startBase, startBase+(inDir*dangerRadius), Color.green);
			return Physics.Raycast (start1, inDir, dangerRadius, layerMask) || Physics.Raycast (start2, inDir, dangerRadius, layerMask);
		}

		
		public void MoveForward(float time = float.NaN) {
			if (float.IsNaN(time)) {
				time = stepLength;
			}
			currSpeed = player.moveSpeed;
			moveTime = time;
		}
		
		public void MoveBackwards(float time = float.NaN) {
			if (float.IsNaN(time)) {
				time = stepLength;
			}
			currSpeed = -player.moveSpeed;
			moveTime = time;
		}
		
		public void StopAccelerating(float time = 0.0f) {
			moveTime = time;
			currSpeed = 0.0f;
		}

		public void StopMoving(float time = 0.0f) {
			StopAccelerating (time);
			rigidbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
		}
		

		public void TurnTo(float deg) {
			Turn(deg - transform.eulerAngles.z);
		}
		public void Turn(float deg) {
			currAngle = -deg;
		}
		
		public void TurnCannonTo(Vector3 to) {
			player.cannon.LookAt (new Vector3(to.x, to.y, player.cannon.position.z), player.cannon.up);

		//	player.cannon.LookAt (to);
		}
		public void TurnCannonTo(float deg) {
			TurnCannon(deg - player.cannon.eulerAngles.z);
		}
		public void TurnCannon(float deg) {
			currCannonAngle = deg;
		}
		
		public bool IsTurned() {
			return Mathf.Abs (currAngle) < 2.0f;
		}
		
		public bool IsMoved() {
			return moveTime <= 0.0f;
		}

		public static bool LayersContain(int mask, GameObject obj) {
			return (mask & obj.layer) > 0;
		}
	}
}
