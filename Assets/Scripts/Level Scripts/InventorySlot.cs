﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;

	Item item;

	// Add item to the slot
	public void AddItem(Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	// Called when the remove button is pressed
	public void OnRemoveButton()
	{
		Inventory1.instance.Remove(item);
	}

	// Called when the item is pressed
	public void UseItem()
	{
		if (item != null)
		{
			item.Use();
		}
	}
}
