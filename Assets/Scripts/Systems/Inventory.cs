using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();
	public int money;

	public void RemoveItem(Item item)
	{
		items.Remove(item);
	}

	public void AddItem(Item item)
	{
		items.Add(item);
	}
}
