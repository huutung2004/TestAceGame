using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] private int currentGold = 0;
    public static GoldManager Instance { get; private set; }
    //event 
    public static event Action<int> OnGoldChanged;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (PlayerPrefs.HasKey("Gold"))
        {
            currentGold =  PlayerPrefs.GetInt("Gold");
            Debug.Log("load gold");
        }
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }
    public void ChangeCurrentGold(int gold)
    {
        currentGold = Mathf.Clamp(currentGold + gold, 0, int.MaxValue);
        OnGoldChanged?.Invoke(GetCurrentGold());
        PlayerPrefs.SetInt("Gold",GetCurrentGold());
        Debug.Log("save");
    }
}
