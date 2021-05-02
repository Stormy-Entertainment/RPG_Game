using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public Transform itemsParent;
	EquipmentSlot[] slots;
	Inventory inventory;
	PlayerStats playerStats;

	[SerializeField] private TextMeshProUGUI healthTxt;
	[SerializeField] private TextMeshProUGUI attackSpeedTxt;
	[SerializeField] private TextMeshProUGUI moveSpeedTxt;
	[SerializeField] private TextMeshProUGUI armorTxt;
	[SerializeField] private TextMeshProUGUI coinTxt;


	private void Awake()
	{
		inventory = GameObject.FindWithTag("PlayerInventory").GetComponent<Inventory>();
		//inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback
		//inventory.onCoinChangedCallback += UpdateText;

		playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();


		slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
		UpdateUI();
		UpdateText();
	}

	public void UpdateUI()
	{
		inventory = GameObject.FindWithTag("PlayerInventory").GetComponent<Inventory>();
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.equipedItems.Count)  // If there is an item to add
			{
				
				slots[i].AddItem(inventory.equipedItems[i]);   // Add it
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
		playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
		if (playerStats != null)
		{
			healthTxt.text = playerStats.m_Health.ToString();
			attackSpeedTxt.text = playerStats.m_AttackSpeed.ToString();
			moveSpeedTxt.text = playerStats.GetMovementSpeed().ToString();
			armorTxt.text = playerStats.m_Armor.ToString();
			coinTxt.text = GameObject.FindWithTag("PlayerInventory").GetComponent<Inventory>().money.ToString();
		}
	}
}
