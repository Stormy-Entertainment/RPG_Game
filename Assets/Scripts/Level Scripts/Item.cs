using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";
	public Sprite icon = null;
	public int id;
	public int price;
	public bool isDefaultItem = false;
	public ItemType itemType;

	//Stats
	public int Health;
	public int Agility;
	public int Intellect;
	public int Stamina;
	public int Strength;

	// Called when the item is pressed in the inventory
	public virtual void Equip(Inventory inventory)
	{
		// Use the item
		// Something might 
		if (itemType == ItemType.HealthPotion)
        {
			FindObjectOfType<PlayerStats>().IncreaseHealth(Health);
		}
		else if(itemType == ItemType.Armor)
        {
		}
		else if (itemType == ItemType.Helmet)
		{
		}
		else if (itemType == ItemType.Clock)
		{
		}
		else if (itemType == ItemType.Pants)
		{
		}
		else if (itemType == ItemType.Boot)
		{
		}
		else if (itemType == ItemType.Gloves)
		{
		}
		RemoveFromInventory(inventory);
		//Remove Item after using It
	}

	public void RemoveFromInventory(Inventory inventory)
	{
		inventory.Remove(this);
	}
}

public enum ItemType
{
	HealthPotion,
	Armor,
	Helmet,
	Clock,
	Pants,
	Boot,
	Gloves
}
