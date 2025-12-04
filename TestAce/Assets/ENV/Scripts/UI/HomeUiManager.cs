using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUiManager : MonoBehaviour
{
    [SerializeField] private Image _listLevelUI;
    [SerializeField] private Image _selectCharacterUI;
    [SerializeField] private Image _settingUI;
    private void Start()
    {
        _listLevelUI.gameObject.SetActive(false);
        _selectCharacterUI.gameObject.SetActive(false);
        _settingUI.gameObject.SetActive(false);

    }
    public void ToggleListLevelUI()
    {
        if(_listLevelUI.gameObject.activeSelf)
        {
            _listLevelUI.gameObject.SetActive(false);
        }
        else
        {
            _listLevelUI.gameObject.SetActive(true);
        }
    }
    public void ToggleSelectCharacterUI()
    {
        if (_selectCharacterUI.IsActive())
        {
            _selectCharacterUI.gameObject.SetActive(false);

        }
        else
        {
            _selectCharacterUI.gameObject.SetActive(true);
        } 
    }
    public void ToggleSettingUI()
    {
        if (_settingUI.IsActive())
        {
            _settingUI.gameObject.SetActive(false);
        }
        else
        {
            _settingUI.gameObject.SetActive(true);
        }
    }
}
