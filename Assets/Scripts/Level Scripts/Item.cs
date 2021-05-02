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
	public int AttackSpeed;
	public int MoveSpeed;
	public int Armor;

	// Called when the item is pressed in the inventory
	public virtual void Use(Inventory inventory)
	{
		// Use the item
		// Something might 
		if (itemType == ItemType.HealthPotion)
        {
			FindObjectOfType<PlayerStats>().IncreaseHealth(Health);
		}
        else
        {
			PlayerStats playerStat = FindObjectOfType<PlayerStats>();
			playerStat.IncreaseAttackSpeed(AttackSpeed);
			playerStat.IncreaseMoveSpeed(MoveSpeed);
			playerStat.IncreaseArmor(Armor);
			FindObjectOfType<PlayerStatsUI>().UpdateText();
		}
		RemoveFromInventory(inventory);
		//Remove Item after using It
	}

	public virtual void DecreaseStats()
    {
		if(itemType != ItemType.HealthPotion)
		{
			PlayerStats playerStat = FindObjectOfType<PlayerStats>();
			playerStat.DecreaseAttackSpeed(AttackSpeed);
			playerStat.DecreaseMoveSpeed(MoveSpeed);
			playerStat.DecreaseArmor(Armor);
			FindObjectOfType<PlayerStatsUI>().UpdateText();
		}
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
