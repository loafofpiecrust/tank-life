using UnityEngine;
using System.Collections;
namespace Stuff{
	public class Flag : Stuff.Pickup {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		internal override bool DoEffect (Player p){
			p.flags++;
			return false;
		}
	}

}