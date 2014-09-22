
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	ArrayList inv = new ArrayList();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void addItem(Object item){
		inv.Add(item);
	}

	internal void removeItemFromSpot(int spot){
		inv.RemoveAt(spot);
	}

	internal void removeItem(Object item){
		inv.Remove(item);
	}

	internal void place(Object trap){
		GameObject obj = trap as GameObject;
		obj.transform.position = transform.position;
	}
}