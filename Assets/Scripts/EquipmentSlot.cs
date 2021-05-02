﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
	public Image icon;
	public TextMeshProUGUI itemName;
	public TextMeshProUGUI removeXText;
	public Button removeButton;

	Item item;
	Inventory inventory;

	private void Awake()
	{
		inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
	}

	// Add item to the slot
	public void AddItem(Item newItem)
	{
		item = newItem;
		itemName.text = newItem.name;
		removeXText.text = "X";
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot()
	{
		item = null;
		itemName.text = null;
		removeXText.text = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	// Called when the remove button is pressed
	public void OnRemoveButton()
	{
		inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		inventory.DecreaseStats(item);
		inventory.UnEquipItem(item);	
	}

	// Called when the item is pressed
	public void UseItem()
	{
		if (item != null)
		{
			inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
			if (inventory.items.Count <= inventory.space)
			{
				//UnEquip
				inventory.DecreaseStats(item);
				inventory.Add(item);
				inventory.UnEquipItem(item);
			}
		}
	}
}
