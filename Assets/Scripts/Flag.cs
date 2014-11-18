using UnityEngine;
using System.Collections;
namespace Stuff{
	public class Flag : Pickup {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		// Registers that a flag has been picked up.
		internal override bool DoEffect (Player p){
			p.flags++;
			return false;
		}
	}

}