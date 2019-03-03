using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateItemCountUIScript : MonoBehaviour {

    private Text text;
    private string itemName;
    public int amount;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        if (gameObject.transform.name == "PotionAmount")
        {
            itemName = "potion";
        }
        else if (gameObject.transform.name == "SpeedBoostAmount")
            itemName = "speedBoost";
	}

    private void Update()
    {
        if (itemName == "potion")
        {
            UpdateCount(ShopScript.shopScript.pAmount);
            amount = ShopScript.shopScript.pAmount;
        }
        else if (itemName == "speedBoost") {
            UpdateCount(ShopScript.shopScript.sbAmount);
            amount = ShopScript.shopScript.sbAmount;
        }
    }

    private void UpdateCount(int amount) {
        text.text = "X " + amount; 
    }
}
