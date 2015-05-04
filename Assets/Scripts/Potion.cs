using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

	void OnTriggerEnter (){

		Inventory.Instance.AddItem (2);
		print ("Hey Mang");
		Destroy (gameObject);
	}
}