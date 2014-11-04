using UnityEngine;
using System.Collections;

public class FlagWin : Pickup
{

		public int minimumFlagsRequired;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		internal override bool DoEffect (Player p)
		{
				Debug.Log ("DOING THINGS!");
				bool jobsDone = false;
				stayingAlive = true;

				foreach (GameObject o in p.otherPlayers) {
						Debug.Log ("FOREACHING!");
						if (o.GetComponent<Player> ().neededWins <= 0) {
								jobsDone = true;
								Debug.Log ("Job's Done");				
								break;
						} else {
								jobsDone = false;
								Debug.Log ("Job's not Done");
						}
				}
				if ((p.GetComponentInChildren<Flag> () is Flag
						&& p.flags >= minimumFlagsRequired)
						&& p.neededWins <= 1
						&& jobsDone) {
						Debug.Log ("You Win");
						Application.LoadLevel ("Lv" + (1 + levelCount));
				} else if (p.GetComponentInChildren<Flag> () is Flag
						&& p.flags >= minimumFlagsRequired) {
						stayingOut = true;
						p.neededWins -= 1;
						Debug.Log ("You need more wins");
				} else {
						stayingOut = true;
						Debug.Log ("else?");
				}
		
				return false;
		}
}

