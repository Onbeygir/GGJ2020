using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinscreenController : MonoBehaviour
{
    public TextMeshProUGUI TMP_Percentage;
    public string PercentageText = "PERCENTAGE:";

    public void ShowWinscreen(float perc)
    {
        gameObject.SetActive(true);
        float percentage = perc * 100;
        TMP_Percentage.text = PercentageText + percentage.ToString("0.00") + "%";
    }

    internal void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
