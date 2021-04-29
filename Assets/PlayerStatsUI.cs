using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public Transform itemsParent;
	InventorySlot[] slots;
	Inventory inventory;

	[SerializeField] private TextMeshProUGUI healthTxt;
	[SerializeField] private TextMeshProUGUI attackSpeedTxt;
	[SerializeField] private TextMeshProUGUI moveSpeedTxt;
	[SerializeField] private TextMeshProUGUI armorTxt;
	[SerializeField] private TextMeshProUGUI coinTxt;


	private void Start()
	{
		inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
		inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		UpdateUI();
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)  // If there is an item to add
			{
				if (inventory.ItemEquiped[i] == true)
				{
					slots[i].AddItem(inventory.items[i]);   // Add it
				}
			}
			else
			{
				// clear slot
				slots[i].ClearSlot();
			}
		}
	}

    public void UpdateText()
    {
        
    }
}
