using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Transform itemsParent;
	public GameObject inventoryUI;
	public static bool isInventoryOpen = false;

	Inventory inventory;
	InventorySlot[] slots;

	private void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

		// Populate slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	private void Update()
	{
		// Open and Close Inventory Menu
		if (Input.GetButtonDown("Inventory"))
		{
			if (!PauseUI.isPaused)
			{
				if (inventoryUI.activeSelf)
				{
					inventoryUI.SetActive(false);
					GameState.instance.ResumeTheGame();
					isInventoryOpen = false;
				}
				else
				{
					inventoryUI.SetActive(true);
					GameState.instance.PauseTheGame();
					isInventoryOpen = true;
				}
			}
		}
	}

	// Called by delegate on the Inventory
	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)  // If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);   // Add it
			}
			else
			{
				// clear slot
				slots[i].ClearSlot();
			}
		}
	}
}
