using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	
	List<Slot> slots = new();
	
	void Start()
	{
		foreach (Slot slot in GetComponentsInChildren<Slot>())
		{
			slots.Add(slot);
			// TEST slot findeing by coloring
			//slot.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
		}
		Debug.Log("loaded " + slots.Count + " slots.");
	}

	public void clearItems() {
		foreach (Slot slot in slots) {
			slot.clear();
		}
	}
	
	public void addItem(Ingredient ingredient) {
		bool found = false;
		foreach (Slot slot in slots) {
			if (!slot.isFull()) {
				slot.setItem(ingredient);
				found = true;
				return;
			}
		}		
		if (!found) {
			Debug.Log("Inventory is full!");
		}
	}
}
