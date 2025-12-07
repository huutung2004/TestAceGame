using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour
{
    public static LoadLevelManager Instance { get; private set; }
    [SerializeField] private List<LevelData> _listLevel;
    [SerializeField] private TMP_Text _levelText;
    private int currentLevel;
    private LevelData _currentLevelData;
    //event
    public static event Action<int> OnStartTimer;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        LevelController.OnLoadLevel += Load;
    }
    private void OnDisable()
    {
        LevelController.OnLoadLevel -= Load;
    }
    public void Load(int level, Character character)
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        if (GameObject.FindGameObjectWithTag("Map") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Map"));
        }
        LevelData levelData = _listLevel[level - 1];
        SetCurrentLevelData(levelData);
        GameObject obj = Instantiate(_listLevel[level - 1]._mapPrefab, Vector3.zero, Quaternion.identity);
        Transform _spawnPos =  GameObject.FindGameObjectWithTag("CheckPoint").transform;
        Instantiate(character._characterPrefabs, _spawnPos.position, Quaternion.identity);
        StartCoroutine(GetCameraFollow(obj));
        DialogManager.Instance.ShowDialog("WELLCOME AND GOODLUCK", 2f);
        _levelText.text = $"{level}";
        SetCurrentLevel(level);
        OnStartTimer?.Invoke(levelData.timeLimit);
    }
    public IEnumerator GetCameraFollow(GameObject obj)
    {
        yield return null;
        CinemachineVirtualCamera cameraFollow = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
        cameraFollow.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        cameraFollow.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        CinemachineConfiner2D cc2d = cameraFollow.gameObject.GetComponent<CinemachineConfiner2D>();
        cc2d.m_BoundingShape2D = obj.GetComponentInChildren<CompositeCollider2D>();
    }
    public void SetCurrentLevelData(LevelData _levelData)
    {
        _currentLevelData = _levelData;
    }
    public LevelData GetCurrentLevelData()
    {
        return _currentLevelData;
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
    }
}
