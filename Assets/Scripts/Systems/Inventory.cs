using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public int money;
	// Item gets added/removed.
	//public delegate void OnItemChanged();
	//public OnItemChanged onItemChangedCallback;

	//public delegate void OnCoinChanged();
	//public OnCoinChanged onCoinChangedCallback;

	public int space = 16;  // Amount of slots in the inventory
	public int equipmentSpace = 6;  // Amount of slots in the inventory

	// List of items in inventory
	public List<Item> items = new List<Item>();
	public List<Item> equipedItems = new List<Item>();


	// Make global
	public static Inventory Instance
	{
		get;
		set;
	}

	void Awake()
	{
		if (this.gameObject.tag == "PlayerInventory")
		{
			if(GameObject.FindGameObjectsWithTag("PlayerInventory").Length > 1)
            {
				Destroy(transform.gameObject);
            }
			DontDestroyOnLoad(transform.gameObject);
			Instance = this;
		}
	}

	// Add a new item to inventory
	public bool Add(Item item)
	{
		if (!item.isDefaultItem)
		{
			// Check if out of space
			if (items.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			items.Add(item);

			//if (onItemChangedCallback != null)
			//	onItemChangedCallback.Invoke();
			FindObjectOfType<InventoryUI>().UpdateUI();
		}
		return true;
	}

	// Equip New Item
	public bool EquipItem(Item item)
	{
		if (!item.isDefaultItem)
		{
			// Check if out of space
			if (equipedItems.Count >= equipmentSpace)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			equipedItems.Add(item);

			//if (onItemChangedCallback != null)
			//	onItemChangedCallback.Invoke();
			FindObjectOfType<InventoryUI>().UpdateUI();
			FindObjectOfType<PlayerStatsUI>().UpdateUI();
			FindObjectOfType<PlayerStatsUI>().UpdateText();
		}
		return true;
	}

	public void UnEquipItem(Item item)
    {
		equipedItems.Remove(item);

		//if (onItemChangedCallback != null)
		//	onItemChangedCallback.Invoke();
		FindObjectOfType<InventoryUI>().UpdateUI();
		FindObjectOfType<PlayerStatsUI>().UpdateUI();
		FindObjectOfType<PlayerStatsUI>().UpdateText();
	}

	public void DecreaseStats(Item item)
    {
		item.DecreaseStats();
    }

	// Remove an item
	public void Remove(Item item)
	{
		items.Remove(item);

		//if (onItemChangedCallback != null)
		//	onItemChangedCallback.Invoke();
		FindObjectOfType<InventoryUI>().UpdateUI();
	}

	public void IncreaseCoins(int Coins)
    {
		money += Coins;
		SaveCoins();
		//if (onCoinChangedCallback != null)
		//	onCoinChangedCallback.Invoke();
		FindObjectOfType<ShopSystem>().UpdateMoneyUI();
	}

	public void DecreaseCoin(int Coins)
    {
		money -= Coins;
		SaveCoins();
		//if (onCoinChangedCallback != null)
		//	onCoinChangedCallback.Invoke();
		FindObjectOfType<ShopSystem>().UpdateMoneyUI();
	}

	public void LoadCoins()
    {
		money = PlayerPrefs.GetInt("CurrentMoney", 500);
		//if (onCoinChangedCallback != null)
		//	onCoinChangedCallback.Invoke();
		FindObjectOfType<ShopSystem>().UpdateMoneyUI();
	}

	public void SaveCoins()
    {
		PlayerPrefs.SetInt("CurrentMoney", money);
	}
}
