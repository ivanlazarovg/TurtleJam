using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = "Current balance: " + CoinsHub.Instance.coins.ToString();
    }
}
