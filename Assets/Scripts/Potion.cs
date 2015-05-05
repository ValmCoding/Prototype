using UnityEngine;
using System.Collections;
//There once was a man from nantucket
public class Potion : MonoBehaviour {

	void OnTriggerEnter (){

		Inventory.Instance.AddItem (2);
		print ("Hey Mang");
		Destroy (gameObject);
	}
}