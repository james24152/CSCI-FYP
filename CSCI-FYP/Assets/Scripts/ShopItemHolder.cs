using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemHolder : MonoBehaviour {

    public int itemID;
    public Image itemIcon;
    public Text itemPrice;
    public Text itemDescription;
    public Button itemBuyButton;
     
	// Use this for initialization
	void Start () {
        itemBuyButton.onClick.AddListener(BuyItem);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BuyItem() {
        Debug.Log(itemPrice.text.ToString().Substring(7));
        int price = int.Parse(itemPrice.text.ToString().Substring(7));
        Debug.Log(price);
        if (CurrencyManager.currencyManager.Request(price))
        {
            CurrencyManager.currencyManager.ReduceMoney(price);
            if (itemID < 2)
            {
                if (itemID == 0)
                    ShopScript.shopScript.AddPotion();
                if (itemID == 1)
                    ShopScript.shopScript.AddSpeedBoost();
                ShopScript.shopScript.UpdateItemAmount();
            }
            else
            {
                ElementCombineBehaviour[] scripts = FindObjectsOfType<ElementCombineBehaviour>();
                foreach (ElementCombineBehaviour script in scripts) {
                    switch (itemID) {
                        case 2:
                            script.AddSteamCombination();
                            break;
                        case 3:
                            script.AddFogCombination();
                            break;
                        case 4:
                            script.AddThunderCombination();
                            break;
                        case 5:
                            script.AddLavaCombination();
                            break;
                        case 6:
                            script.AddLifeCombination();
                            break;
                        case 7:
                            script.AddSandCombination();
                            break;

                    }
                }
                itemBuyButton.interactable = false;
            }
        }
        else {
            Debug.Log("you have no money!");
        }
    }
}
