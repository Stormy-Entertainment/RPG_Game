using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";
	public Sprite icon = null;
	public bool isDefaultItem = false;

	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
		// Use the item
		FindObjectOfType<PlayerStats>().IncreaseHealth(20);
		Debug.Log("Using " + name);
		RemoveFromInventory();
	}

	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}
}
