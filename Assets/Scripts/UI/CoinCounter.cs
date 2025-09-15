using System;
using TMPro;
using UnityEngine;

public class CoinCounter : Singleton<CoinCounter>
{
    private TMP_Text coinText;
    private int currentCoinAmount = 0;

    public void UpdateCurrentCoin()
    {
        currentCoinAmount++;

        if (coinText == null)
        {
            coinText = GameObject.Find("Coin Amount").GetComponent<TMP_Text>();
        }

        coinText.text = currentCoinAmount.ToString("D3");
    }
}
