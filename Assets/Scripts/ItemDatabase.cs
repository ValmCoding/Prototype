using UnityEngine;
using System.Collections;
using System.Collections.Generic; //gives us access to List type

public class ItemDatabase : MonoBehaviour {
	
	public List<Item> allOfTheItems = new List<Item> (); 

	void Start(){

		//can add here as well as in inspector
		//must set script execution order: 1) Item Database, 2) Inventory
		allOfTheItems.Add (new Item ("Demon Cutter", 1,"An absurdly sharp blade. An evil aura exudes from it",1000, 500, 1, Item.ItemType.Weapon));
		allOfTheItems.Add (new Item ("Potion", 2,"Heal 100 HP",0, 0, 1, Item.ItemType.Consumable));
	}
	
}
