using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Image textStackQuanityBackground;
    public Text textStackQuality;
    public Button removeButton;
    public int currentQuality;
    public GameObject sellButtonParent;
    public GameObject equipHover;

    public GameObject inventoryManager;

    public void UpdateSlot(Item item)
    {
        icon.sprite = item.icon;
    }

    public void ClearSlots()
    {
        icon.sprite = null;
    }

    public void RemoveItem()
    {
        currentQuality--;
        //InventoryUI.UpdateUI();
    }

    public void SellItem()
    {
        if(currentQuality > 0)
        {
            //CurrencyManager.instance.AddCoin(item.value);
            RemoveItem();
        }
    }

    public void InstantiateEquipmentUIHover()
    {
        //if(item != null && item is Equipment)
        {
            //inventoryManager.GetComponent<InventoryUI>().InstantiateEquipmentHoverUI(transform, item as Equipment);
        }
    }

    public void DestroyEquipmentHoverUI()
    {
        //if(inventoryManager.GetComponent<InventoryUI>().newEquipmentHoverUI != null)
        {
            //Destroy(inventoryManager.GetComponent<InventoryUI>().newEquipmentHoverUI);
        }
    }

}
