using System;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
	public ShopSystem shopSystem;

	bool inRange = false;

	public string shopName;
	public bool canSellTo = true;
	//public bool finiteMoney = true;
	//public bool finiteItems = true;
	public Inventory shopInventory;
	private bool ShopOpened = false;

	private void Update()
	{
		if (Input.GetButtonDown("Interact"))
		{
			StartTrade();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			ChangeState(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			ChangeState(false);
			ShopOpened = false;
			try
			{
				if (shopSystem.shopkeeper.GetInstanceID() == this.GetInstanceID())
				{
					shopSystem.CloseShop();
				}
			}
			catch (Exception e) { }
		}
	}

	void ChangeState(bool state)
	{
		inRange = state;
	}

	public void StartTrade()
	{
		if (inRange)
		{
			if (!ShopOpened)
			{
				ShopOpened = true;
				shopSystem.OpenShop(this);
			}
            else
            {
				shopSystem.CloseShop();
            }
		}
	}
}
