using UnityEngine;
using System.Collections;


public abstract class Pickup : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		//Debug.Log("collided");
		Player p = col.GetComponent<Player>();
		if (p){
			bool keep = DoEffect (p);
			if(!keep){
				this.transform.parent = p.inv.transform;
				this.collider.enabled = false;
				this.renderer.enabled = false;
			}
			else {
				//Debug.Log("Go away now");
				Destroy(this.gameObject);
			}
		}
	}

	internal abstract bool DoEffect(Player p);

}
