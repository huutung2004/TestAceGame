using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadLevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelData> _listLevel;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private TMP_Text _levelText;
    //event
    public static event Action<int> OnStartTimer;
    private void OnEnable()
    {
        LevelController.OnLoadLevel += Load;
    }
    private void OnDisable()
    {
        LevelController.OnLoadLevel -= Load;
    }
    private void Load(int  level, Character character)
    {
        LevelData levelData = _listLevel[level - 1];
        GameObject obj = Instantiate(_listLevel[level-1]._mapPrefab, Vector3.zero, Quaternion.identity);
        Instantiate(character._characterPrefabs,_spawnPos.position, Quaternion.identity);
        CinemachineVirtualCamera cameraFollow = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
        cameraFollow.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        cameraFollow.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        CinemachineConfiner2D cc2d =  cameraFollow.gameObject.GetComponent<CinemachineConfiner2D>();
        cc2d.m_BoundingShape2D = obj.GetComponentInChildren<CompositeCollider2D>();
        DialogManager.Instance.ShowDialog("WELLCOME AND GOODLUCK",2f);
        _levelText.text = $"{level}";
        OnStartTimer?.Invoke(levelData.timeLimit);
    }
}
