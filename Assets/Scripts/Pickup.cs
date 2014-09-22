using UnityEngine;
using System.Collections;


public abstract class Pickup : MonoBehaviour {

	internal bool doNotDestroy = true;

	void OnTriggerEnter(Collider col) {
		Player p = col.gameObject.GetComponent<Player>();
		if (p && !doNotDestroy){
			bool keep = DoEffect (p);
			if(!keep){
				this.transform.parent = p.inv.transform;
				this.collider.enabled = false;
				this.renderer.enabled = false;
			}
			else {
				Destroy (this);
			}
		}
	}

	internal abstract bool DoEffect(Player p);

}
