using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory1 : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        //instance = this;
    }

    #endregion

    private void Start()
    {
        
    }

    public int maxInventorySlots = 20;
    public InventorySlotScript[] slotScripts;
    public GameObject inventoryUI;

    public void GetSlotScriptsArray()
    {
        slotScripts = inventoryUI.GetComponentsInChildren<InventorySlotScript>();
    }

    //public bool Add(Item item)
    //{
        //if (item.stackable)
        //{
            //foreach(InventorySlotScript s in slotScripts)
            //{
                //if(s.item == item && s.currencyQuantity < item.maxStack) //Find existing stack with item tht is less than the item's max stack Qty
                //{
                    //s.currentQuality += 1;//Add Item
                    //InventoryUI.UpdateUI();
                    //return true;
                //}
            //}
        //}

        //for (int i = 0; i < slotScripts.Length; i++)
        //{
            //if(slotScripts[i].currentQuality == 0)
            //{
                //slotScripts[i].item = item;
                //slotScripts[i].currentQuantity += 1;
               // InventoryUI.UpdateUI();
                //return true;
            //}
        //}

        //return false;
    //}

    //public bool AddMultiple(Item item, int qty)
    //{
        //if (item.stackable)
        //{
            //foreach(InventorySlotScript s in slotScripts)
            //{
                //if(s.item == item && s.currentQuantity + qty < item.maxStack)
                //{
                    //s.currentQuantity += qty;//Add Items
                    //Debug.Log(qty + item.name + "Added");
                    //StartCoroutine(GetComponent<ItemReceived>().InstantiateAddItemUI(item, 3));
                    //InventoryUI.UpdateUI();
                    //retuen true;
                //}
            //}
        //}
    //}

}
