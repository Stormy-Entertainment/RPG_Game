using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotScript : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Text priceText;
    public int buyPrice;

    
    private void Start()
    {
        if(item != null)
        {
            icon.sprite = item.icon;
            priceText.text = buyPrice.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void PurchaseItem()
    {
        Debug.Log("Purchase Item Called");
        if (EnoughCoins())
        {
            Debug.Log("Enough Coins to Purchase" + item);
            if (Inventory.instance.Add(item))
            {
                Debug.Log("Item Purchased");
                CurrencyManager.instance.RemoveCoin(buyPrice);
            }
            else
            {
                Debug.Log("Not Enough Room in Inventory to Purchase" + item);
            }
         }
        else
        {
            Debug.Log("Need More Coins to Purchase" + item);
        }
    }

    public bool EnoughCoins()
    {
        if(CurrencyManager.instance.coinAmount - buyPrice >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
