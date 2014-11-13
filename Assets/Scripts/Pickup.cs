using UnityEngine;
using System.Collections;

namespace Stuff{
	public abstract class Pickup : MonoBehaviour {

		internal bool stayingAlive;
		internal bool stayingOut;
		internal static int levelCount=1;

		//On hit event

		void OnTriggerEnter(Collider col) {

			Player p = col.GetComponent<Player>();
				bool keep = DoEffect (p);

			// If you want to store it in the player inv.

				if(!stayingOut && !keep){
					this.transform.parent = p.inv.transform;
					this.collider.enabled = false;
					this.renderer.enabled = false;
				}

			// If you don't want it in the player inv & you want it off the map.

				else if(!stayingAlive){
					Destroy(this.gameObject);
				}
			}

		internal abstract bool DoEffect(Player p);

	}
}
