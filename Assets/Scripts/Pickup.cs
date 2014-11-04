using UnityEngine;
using System.Collections;


public abstract class Pickup : MonoBehaviour {

	internal bool stayingAlive;
	internal bool stayingOut;
	internal static int levelCount=1;

	void OnTriggerEnter(Collider col) {
		Debug.Log("collided");
		Player p = col.GetComponent<Player>();
		Debug.Log("Player received.");

			Debug.Log("Is player?");
			bool keep = DoEffect (p);
			if(!stayingOut && !keep){
				Debug.Log("Do you want to keep");
				this.transform.parent = p.inv.transform;
				this.collider.enabled = false;
				this.renderer.enabled = false;
			}
			else if(!stayingAlive){
				Debug.Log("Go away now");
				Destroy(this.gameObject);
			}
		}

	internal abstract bool DoEffect(Player p);
	internal void Drop() {}

}
