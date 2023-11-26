using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = CoinsHub.Instance.coins.ToString();
    }
}
