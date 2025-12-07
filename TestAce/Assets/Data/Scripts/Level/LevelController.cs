using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    [SerializeField] private List<LevelData> _levelDatas;
    [SerializeField] private int totalMapUnlocked;
    //event
    public static event Action<int, Character> OnLoadLevel;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadProgress();
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
    public void UnlockLevel()
    {
        totalMapUnlocked++;
    }
    public int GetTotalMapUnlocked()
    {
        return totalMapUnlocked;
    }
    public List<LevelData> getListLevelData()
    {
        return _levelDatas;
    }
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("TotalMapUnlocked", totalMapUnlocked);

        List<LevelSaveData> saveList = new List<LevelSaveData>();

        foreach (var level in _levelDatas)
        {
            saveList.Add(new LevelSaveData()
            {
                level = level.level,
                currentStar = level.currentStar
            });
        }

        string json = JsonUtility.ToJson(new Wrapper<LevelSaveData>(saveList));
        PlayerPrefs.SetString("LevelStars", json);

        PlayerPrefs.Save();
        Debug.Log("Progress Saved!!");
    }

    public void LoadProgress()
    {
        totalMapUnlocked = PlayerPrefs.GetInt("TotalMapUnlocked", 0);

        if (!PlayerPrefs.HasKey("LevelStars")) return;

        string json = PlayerPrefs.GetString("LevelStars");

        var wrapper = JsonUtility.FromJson<Wrapper<LevelSaveData>>(json);

        foreach (var save in wrapper.items)
        {
            var level = _levelDatas.Find(l => l.level == save.level);

            if (level != null)
            {
                level.currentStar = save.currentStar;
            }
        }
        Debug.Log("Progress Loaded!!");
    }

}
