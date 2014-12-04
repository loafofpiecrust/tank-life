using UnityEngine;
using System.Collections;


namespace Stuff {

	public abstract class Pickup : MonoBehaviour {

		//****************
		//For determining storage vs. destruction
		internal bool stayingAlive;
		internal bool stayingOut;
		//****************
		internal static int levelCount=1;
		
		internal abstract bool DoEffect(Player p);
		
		internal virtual void Drop() {
			transform.parent = null;
			transform.Translate (-transform.parent.forward * 3.0f);
			renderer.enabled = true;
			collider.enabled = true;
		}

		void OnTriggerEnter(Collider col) {
			Debug.Log("collided");

			Player p = col.GetComponent<Player>();
			if (!p){
				return;
			}
			Debug.Log("Player received.");
			bool keep = DoEffect (p);

			if(!stayingOut && keep){
				Debug.Log("Do you want to keep");
				this.transform.parent = p.inv.transform;
				this.collider.enabled = false;
				this.renderer.enabled = false;
			}

			else if(!stayingAlive) {
				Debug.Log("Go away now");
				Destroy(this.gameObject);
			}
		}
	}
}
