using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<New_Item> items = new List<New_Item>();
	public int money;

	public void RemoveItem(New_Item item)
	{
		items.Remove(item);
	}

	public void AddItem(New_Item item)
	{
		items.Add(item);
	}
}
