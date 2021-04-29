using System;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
	public ShopSystem shopSystem;
	[SerializeField] private GameObject Info;

	bool inRange = false;

	public string shopName;
	public bool canSellTo = true;
	public Inventory shopInventory;
	public bool ShopOpened = false;

    private void Awake()
    {
		shopSystem = GameObject.FindGameObjectWithTag("OverlayCanvas").GetComponent<ShopSystem>();
	}

    private void Update()
	{
		if (Input.GetButtonDown("Interact"))
		{
			if (!GameState.isGameOver && !GameState.isStageCompleted)
			{
				PauseUI pause = FindObjectOfType<PauseUI>();
				InventoryUI InventoryUI = FindObjectOfType<InventoryUI>();
				if (!pause.isPaused && !InventoryUI.isInventoryOpen)
				{
					StartTrade();
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			inRange = true;
			Info.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			inRange = false;
			Info.SetActive(false);
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
				ShopOpened = false;
			}
		}
	}
}
