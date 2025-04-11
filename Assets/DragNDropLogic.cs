using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class DragNDropLogic : MonoBehaviour {

	private Camera mainCam;
	public GameObject dropPrefab;
	private Drop draggedDrop;
	private Vector2 dragOffset;
	public GameObject testCursor;
	
	private void Awake() {
		mainCam = Camera.main;
	}
	
	//create a drop when mouse clicks an inventory slot
	public void OnClick(InputAction.CallbackContext context)
	{
		if (context.started) {
			PickupDrop();
		}
		else if (context.canceled) {
			DropDrop();
		}
	}
	
	public void OnPointerMove(InputAction.CallbackContext context) 
	{
		Vector2 worldMousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		testCursor.transform.position = worldMousePos;
		if (!draggedDrop) return;
		draggedDrop.transform.position = worldMousePos + dragOffset;
	}
	
	void PickupDrop() {
		Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, float.PositiveInfinity, LayerMask.GetMask("Interact"));
		if (!hit2D) return;
		
		Slot slot = hit2D.collider.GetComponent<Slot>();
		if (slot == null || !slot.isFull()) return;
		
		Ingredient ingredient = slot.item;
		Vector2 clickDist = hit2D.point - (Vector2)slot.transform.position;
		Vector2 spawnPoint = hit2D.point + clickDist.normalized * 0.5f;
		
		//instantiate a drop with same ingredient
		GameObject dropObj = Instantiate(dropPrefab, spawnPoint, Quaternion.identity);
		draggedDrop = dropObj.GetComponent<Drop>();
		draggedDrop.setItem(ingredient);
		// slot.clear();
	}

	void DropDrop() {
		if (!draggedDrop) return;
		draggedDrop.setFalling();
		draggedDrop = null;
	}

}
