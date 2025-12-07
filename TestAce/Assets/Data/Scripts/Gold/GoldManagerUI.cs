using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManagerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;
    private void OnEnable()
    {
        GoldManager.OnGoldChanged += RefreshUI;
        if (GoldManager.Instance != null)
            RefreshUI(GoldManager.Instance.GetCurrentGold());
    }
    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= RefreshUI;

    }
    public void RefreshUI(int value)
    {
        _goldText.text = $"{value}";
    }
}
