using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private int _StartlevelUnlocked = 1;
    private int totalMapUnlocked;
    //event
    public static event Action<int> OnLoadLevel;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           Destroy(this.gameObject);
           return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if(!PlayerPrefs.HasKey("LevelUnlocked"))
        {
            PlayerPrefs.SetInt("LevelUnlocked", _StartlevelUnlocked);
        }
    }
    public void Loadlevel(int level)
    {
        SceneManager.LoadScene($"level{level}");
        OnLoadLevel?.Invoke(level);
    }
    public void UnlockLevel() {
        totalMapUnlocked++;
    }
    public int GetTotalMapUnlocked() {
        return totalMapUnlocked;
    }


}
