using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private int _StartlevelUnlocked = 1;
    [SerializeField] private List<LevelData> _levelDatas;
    [SerializeField] private int totalMapUnlocked;
    //event
    public static event Action<int,Character> OnLoadLevel;
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
        SceneManager.LoadScene("Play");
        StartCoroutine(InvokeAfterLoad(level));
    }
    private IEnumerator InvokeAfterLoad(int level)
    {
        yield return null;
        OnLoadLevel?.Invoke(level, CharacterManager.Instance.GetCharacterSelected());
    }
    public void UnlockLevel() {
        totalMapUnlocked++;
    }
    public int GetTotalMapUnlocked() {
        return totalMapUnlocked;
    }


}
