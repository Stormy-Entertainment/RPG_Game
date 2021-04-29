using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
	public bool shopOpen = false;
	[HideInInspector] public Shopkeeper shopkeeper;
	private Inventory playerInventory;

	[Header("UI")]
	public GameObject shopUI;
	public TextMeshProUGUI playerMoney;
	public TextMeshProUGUI PlayerStatsUIMoneyText;
	public TextMeshProUGUI shopName;
	public RectTransform InventoryContent;
	public RectTransform ShopContent;

	public ItemListing[] inventoryItemListing;
	public ItemListing[] shopItemListing;

    private void Awake()
    {
		playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		inventoryItemListing = InventoryContent.GetComponentsInChildren<ItemListing>();
		shopItemListing = ShopContent.GetComponentsInChildren<ItemListing>();
	}

    public void Start()
    {
		ClearListings();
	}

    public void OpenShop(Shopkeeper keeper)
	{
		if (!GameState.isPaused)
		{
			shopOpen = true;
			shopkeeper = keeper;
			shopName.text = shopkeeper.shopName;
			shopUI.SetActive(true);
			UpdateMoneyUI();
			LoadPlayerItems();
			LoadShopItems();		
			GameState.instance.PauseTheGame();
		}
	}

	void ClearListings()
	{
		for (int i = 0; i < inventoryItemListing.Length; i++)
		{
			inventoryItemListing[i].ClearSlot();
		}

		for (int i = 0; i < shopItemListing.Length; i++)
		{
			shopItemListing[i].ClearSlot();
		}
	}

	void LoadPlayerItems()
	{
		playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		for (int i = 0; i < inventoryItemListing.Length; i++)
		{
			inventoryItemListing[i].shopSystem = this;
			if (i < playerInventory.items.Count)  // If there is an item to add
			{
				inventoryItemListing[i].ListItem(playerInventory.items[i], ItemListing.ListingMode.Sell);   // Add it
			}
			else
			{
				// clear slot
				inventoryItemListing[i].ClearSlot();
			}		
		}
	}

	void LoadShopItems()
	{
		for (int i = 0; i < shopItemListing.Length; i++)
		{
			shopItemListing[i].shopSystem = this;
			if (i < shopkeeper.shopInventory.items.Count)  // If there is an item to add
			{
				shopItemListing[i].ListItem(shopkeeper.shopInventory.items[i], ItemListing.ListingMode.Buy);   // Add it			
			}
			else
			{
				// clear slot
				shopItemListing[i].ClearSlot();
			}
		}
	}

	public void SellToShop(Item item)
	{
		playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		if (item != null)
		{
			if (!shopkeeper.canSellTo)
			{
				return;
			}
			playerInventory.IncreaseCoins(item.price);
			playerInventory.Remove(item);
			LoadPlayerItems();
			UpdateMoneyUI();
		}
		else
		{
			Debug.Log("No Item assigned to the button.");
		}
	}

	public void BuyFromShop(Item item)
	{
		playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		if (item != null)
		{
			if (playerInventory.money - item.price < 0)
			{
				return;
			}

			playerInventory.DecreaseCoin(item.price);
			playerInventory.Add(item);
			LoadPlayerItems();
			UpdateMoneyUI();
		}
        else
        {
			Debug.Log("No Item assigned to the button.");
        }
	}

	public void UpdateMoneyUI()
	{
		playerInventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
		playerMoney.text = playerInventory.money.ToString();
		PlayerStatsUIMoneyText.text = playerInventory.money.ToString();
	}

	public void CloseShop()
	{
		shopkeeper = null;
		shopUI.SetActive(false);
		shopOpen = false;
		GameState.instance.ResumeTheGame();
	}
}
