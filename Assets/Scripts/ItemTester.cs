using UnityEngine;

public class ItemTester : MonoBehaviour {
    
    public InventoryManager Manager;
    public Ingredient testIngred;
    
    void Start() {
        // add ingredient in 1 second
        Invoke("AddItem", 1f);
    }
    
    void AddItem() {
        Manager.addItem(testIngred);
    }
}
