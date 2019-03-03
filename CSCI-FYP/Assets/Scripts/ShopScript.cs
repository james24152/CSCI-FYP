using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    public static ShopScript shopScript;
    public List<ShopItem> itemList = new List<ShopItem>();

    public GameObject itemHolderPrefab;
    public Transform grid;
    public Text potionAmount;
    public Text speedBoostAmount;

    public int pAmount = 0;
    public int sbAmount = 0;

	// Use this for initialization
	void Start () {
        shopScript = this;
        FillShop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FillShop() {
        for (int i = 0; i < itemList.Count; i++) {
            GameObject holder = Instantiate(itemHolderPrefab, grid);
            ShopItemHolder holderScript = holder.GetComponent<ShopItemHolder>();
            holderScript.itemID = itemList[i].itemID;
            holderScript.itemPrice.text = "PRICE: " + itemList[i].price.ToString();
            holderScript.itemIcon.sprite = itemList[i].icon;
            holderScript.itemDescription.text = itemList[i].description;
        }
    }

    public void AddPotion() {
        pAmount += 1;
    }
    public void AddSpeedBoost()
    {
        sbAmount += 1;
    }
    public void ReducePotion()
    {
        pAmount -= 1;
    }
    public void ReduceSpeedBoost()
    {
        sbAmount -= 1;
    }

    public void UpdateItemAmount() {
        potionAmount.text = "x" + pAmount.ToString();
        speedBoostAmount.text = "x" + sbAmount.ToString();
    }
}
