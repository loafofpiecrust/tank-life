
using System;
using UnityEngine;

namespace Stuff {
	public class SneadAI {
		public override void StepLogic() {
			
		}

		public override void SeeObject(Transform other) {
			Player p = other.GetComponent<Player> ();
			if (p) {
				
			}
		}
	}
}

