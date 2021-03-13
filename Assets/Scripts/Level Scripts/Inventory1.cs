using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory1 : MonoBehaviour
{
	public static Inventory instance;

	void Awake()
	{
		if (instance != null)
		{
			return;
		}
		//instance = this;
	}

	// Item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;  // Amount of slots in the inventory

	// List of items in inventory
	public List<Item> items = new List<Item>();

	// Add a new item
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

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
		return true;
	}

	// Remove an item
	public void Remove(Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

}