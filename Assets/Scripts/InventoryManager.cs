using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	
	List<Slot> slots = new();
	
	void Start()
	{
		foreach (Slot slot in GetComponentsInChildren<Slot>()){ slots.Add(slot);}
	}

	public void ClearItems() {
		foreach (Slot slot in slots) {slot.Clear();}
	}

}
