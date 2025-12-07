using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelManagerUI : MonoBehaviour
{
    [SerializeField] private Image _panelWinLevel;
    [SerializeField] private Image _panelFailLevel;
    //event
    public static event Action OnPlay;
    private void Awake()
    {
        if (_panelFailLevel != null && _panelWinLevel != null)
        {
            _panelFailLevel.gameObject.SetActive(false);
            _panelWinLevel.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        TimeManagerUI.OnTimestamp += ShowPanelFailLevel;
        WinLevel.OnCompleteLevel += ShowPanelWinLevel;
        CharacterHealth.OnCharacterDie += ShowPanelFailLevel;
    }
    private void OnDisable()
    {
        TimeManagerUI.OnTimestamp -= ShowPanelFailLevel;
        WinLevel.OnCompleteLevel -= ShowPanelWinLevel;
        CharacterHealth.OnCharacterDie -= ShowPanelFailLevel;

    }
    private void ShowPanelWinLevel()
    {
        _panelWinLevel.gameObject.SetActive(true);
        LevelData levelData = LoadLevelManager.Instance.GetCurrentLevelData();

        int collected = FruitsManager.Instance.GetCurrentFruits();
        int required = levelData.fruitRequirment;

        int star = 0;

        if (collected >= required)
        {
            star = 3;
        }
        else if (collected >= required / 2)
        {
            star = 2;
        }
        else
        {
            star = 1;
        }
        if (StarEffectDO.Instance != null)
        {
            StarEffectDO.Instance.PlayStars(star);
            StarEffectDO.Instance.PlayCoinReward(levelData.goldReward);
            GoldManager.Instance.ChangeCurrentGold(levelData.goldReward);
        }
        if (LoadLevelManager.Instance.GetCurrentLevel() - 1 == LevelController.Instance.GetTotalMapUnlocked())
            LevelController.Instance.UnlockLevel();
        levelData.currentStar = levelData.currentStar >= star ? levelData.currentStar : star;
        LevelController.Instance.SaveProgress();
        Time.timeScale = 0f;
    }
    private void ShowPanelFailLevel()
    {
        _panelFailLevel.gameObject.SetActive(true);

    }
    public void NextLevel()
    {
        LoadLevelManager load = LoadLevelManager.Instance;
        load.Load(load.GetCurrentLevel() + 1, CharacterManager.Instance.GetCharacterSelected());
        _panelWinLevel.gameObject.SetActive(false);
        FruitsManager.Instance.ResetFruit();
        OnPlay?.Invoke();
        Time.timeScale = 1f;

    }
    public void RePlay()
    {
        LoadLevelManager load = LoadLevelManager.Instance;
        load.Load(load.GetCurrentLevel(), CharacterManager.Instance.GetCharacterSelected());
        _panelWinLevel.gameObject.SetActive(false);
        _panelFailLevel.gameObject.SetActive(false);
        FruitsManager.Instance.ResetFruit();
        OnPlay?.Invoke();
        Time.timeScale = 1f;

    }
    public void BackToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }
}
