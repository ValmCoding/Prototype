using UnityEngine;
using System.Collections;
//This is now a comment here
[System.Serializable] //lets you manage database through inspector in Item database
public class Item {

	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public ItemType itemType;

	//optional
	public int itemAtkPow;
	public int itemAtkSpd;
	public int itemStack;


	public enum ItemType {Weapon, Consumable, Quest}


	public Item(string name, int id, string desc, int pow, int spd, int stk, ItemType type){
	
		//a folder required for resources
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D> ("Icons/" + name);
		itemAtkPow = pow;
		itemAtkSpd = spd;
		itemStack = stk;
		itemType = type;
	}

	public Item(){

	}

}
