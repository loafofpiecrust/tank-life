using UnityEngine;
using System.Collections;


public abstract class Pickup : MonoBehaviour {



	void OnCollisionEnter(Collision col) {
		Player p = col.gameObject.GetComponent<Player>();
		if (p){
			DoEffect (p);
		}
		Destroy(this.gameObject);
	}

	internal abstract void DoEffect(Player p);

}
