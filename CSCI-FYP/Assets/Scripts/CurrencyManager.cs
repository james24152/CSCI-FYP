using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public static CurrencyManager currencyManager;
    [SerializeField]
    private int money;

    public Text moneyText;

    // Use this for initialization
    void Start() {
        currencyManager = this;
        UpdateUI();
    }

    // Update is called once per frame
    void Update() {

    }

    public void AddMoney(int amount) {
        money += amount;
        UpdateUI();
    }

    public void ReduceMoney(int amount)
    {
        money -= amount;
        UpdateUI();
    }

    public bool Request(int amount) {
        if (amount <= money) {
            return true;
        }
        return false;
    }

    public void UpdateUI() {
        moneyText.text = money.ToString();
    }
}
