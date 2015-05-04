using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*****
 * Make sure to create an Item Database tag
 * There needs to be an Item Database object and an Inventory object
 * Don't use an Item ID of 0
 * You need to set the Script Execution order so that Item Database loads first, then Inventory
 * Edit -> Project Settings -> Script Execution order
 * For the skins you need to setup a new custom skin to load up the icon images, then we can use
 * our own designs. 
 * Also need to set up an inventory button Edit -> Project Settings -> Input 
 * 
 * 
*****/

public class Inventory : MonoBehaviour {

	public static Inventory Instance;

	public int slotsX, slotsY;
	public GUISkin skin; //Use this to create a skin for the inv boxes. Add a new skin
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> (); //only sets up the spaces, not items

	private ItemDatabase database; 
	private bool showInventory;
	private int iconSize = 50;
	private bool showToolTip; //optional
	private string toolTip; //optional

	private bool draggingItem; //optional
	private Item curDraggedItem; //optional
	private int prevItemIndex; //optional


	// Use this for initialization
	void Start () {

		Instance = this;

		for (int i = 0; i < (slotsX * slotsY); i++){

			slots.Add(new Item());
			inventory.Add(new Item());

		}

		/********Make sure to create an Item Database tag*************/
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase>();

		AddItem (1);
		//RemoveItem (1);
	}

	void Update(){
		if (Input.GetButtonDown ("Inventory")) {//add that in project settings Input

			showInventory = !showInventory;
		}

	}

	void DrawInventory(){

		Event e = Event.current;
		int i = 0; //needed for outside indexing. Does not need to reset unitl for loop returns
		for (int y = 0; y < slotsY; y++){
			for (int x = 0; x < slotsX; x++){

				Rect slotBox = new Rect(x * 60, y * 60, iconSize, iconSize);
				GUI.Box(slotBox,"", skin.GetStyle("Slot"));//add skin.GetStyle("Whatever you called it)
				slots[i] = inventory[i];
				//checks to see if there is an item in a slot
				if(slots[i].itemName != null){

					GUI.DrawTexture(slotBox, slots[i].itemIcon);

					/***Totally Optional, displays tooltip / item info****/
					/**I think it might be a good addon if we make it more detailed instead of just a few lines**/
					/***Example, Have it display a new window instead of a small box*/
					//use Event in GUI
					//checks if mouse is over tooltip
					if(slotBox.Contains(e.mousePosition)){
						toolTip = CreateToolTip(slots[i]);
						if (!draggingItem){
							showToolTip = true;
						}

						/***optional Drag and drop******/
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem){

							draggingItem = true;
							prevItemIndex = i;
							curDraggedItem = slots[i]; //sets the item being dragged
							inventory[i] = new Item();

						}
						if(e.type == EventType.mouseUp && draggingItem){

							inventory[prevItemIndex] = inventory[i];
							inventory[i] = curDraggedItem;
							draggingItem = false;
							curDraggedItem = null;
						}
						//using comsumable, or use/equip item by clicking right mouse. change maybe?
						if (e.isMouse && e.type == EventType.mouseDown && e.button == 1){
							if (slots[i].itemType == Item.ItemType.Consumable){

								print ("Used a " + slots[i].itemName);
								slots[i].itemStack--;

								/**********Do Whatever item does***************/

								UseComsumable(slots[i].itemID);

								 
								/*********************************************/

								if (slots[i].itemStack <= 0)
									RemoveItem(slots[i].itemID);
							}
						}
					}
				} else {
					if (slotBox.Contains(e.mousePosition)){
						if (e.type == EventType.mouseUp && draggingItem){
							inventory[i] = curDraggedItem;
							draggingItem = false;
							curDraggedItem = null;
						}
					}
				/*****End of Drag and Drop************/
				}
				if (toolTip == "")
					showToolTip = false;
				i++;
				/***end of ToolTip********/
			}
		}
	}

	string CreateToolTip (Item item){

		toolTip = "<color=#ffffff>" + item.itemName + "</color>\n" + item.itemDesc; /*Change to not shitty*/
		return toolTip;
	}

	// Update is called once per frame
	void OnGUI () {//have to use OnGUI for GUI stuff (obv)

		Event e = Event.current;
		toolTip = "";
		GUI.skin = skin; //add when you have a skin
		if (showInventory) {
			DrawInventory();
			if (showToolTip) {
				
				/**change to bring up a menu instead of having it follow mouse if wanted**/
				GUI.Box(new Rect(e.mousePosition.x + 15f, e.mousePosition.y, 200, 200), toolTip, skin.GetStyle("ToolTip"));
			}
		}
		if (draggingItem){
			GUI.DrawTexture(new Rect(e.mousePosition.x, e.mousePosition.y, iconSize, iconSize), curDraggedItem.itemIcon);
		}

	}

	public void AddItem(int id){

		if (InventoryContains(id)) {
			for (int i = 0; i < inventory.Count; i++) {
				if(inventory[i].itemType != Item.ItemType.Weapon){
					inventory[i].itemStack++;
					print (inventory[i].itemStack);
				}
			}
		
		} else {
			//Goes through the entire inventory, looks for empty slot, 
			//then goes thorugh the database and check to see we find a match to the id
			//then sets the empty slot to be equal to the item

			/****Can use another way for search instead of ID******/
			for (int i = 0; i < inventory.Count; i++) {
				if (inventory [i].itemName == null) {
					for (int j = 0; j < database.allOfTheItems.Count; j++) {
						if (database.allOfTheItems [j].itemID == id) {
							inventory [i] = database.allOfTheItems [j];
						}
					}
					break;
				}
			}
		}
	}

	public void RemoveItem(int id){
		//replaces item with a blank item
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory[i].itemID == id){
				inventory[i] = new Item();
				break;
			}
		}
	}

	bool InventoryContains(int id){
		//Checks to see if inventory contains _____________
		bool result = false;
		for (int i = 0; i < inventory.Count; i++) {
			result = inventory[i].itemID == id;
			if(result){
				break;
			}
		}
		return result;
	}

	void UseComsumable(int id){

		switch (id) {

			case 2:
			{
				print ("Do the thing");
				break;
			}
			default:
				break;
		}

	}

}














