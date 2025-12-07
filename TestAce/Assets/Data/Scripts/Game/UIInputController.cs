using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputController : MonoBehaviour
{
    public static UIInputController Instance;
    public bool isMoveRight;
    public bool isMoveLeft;
    public bool isJump;
    public bool isPause;
    [SerializeField] private Image _panelSetting;
    private void Awake()
    {
        Instance = this;
        _panelSetting.gameObject.SetActive(false);
    }
    public void OnLeftDown() => isMoveLeft = true;
    public void OnLeftUp() => isMoveLeft = false;
    public void OnRightDown() => isMoveRight = true;
    public void OnRightUp() => isMoveRight = false;
    public void OnJumpDown() => isJump = true;
    public void OnJumpUp() => isJump = false;
    public void OnPause()
    {
        if (isPause)
        {
            Time.timeScale = 1f;
            isPause = false;
        }
        else
        {
            Time.timeScale = 0f;
            isPause = true;
        }
    }
    public void ToggleSetting()
    {
        if (_panelSetting.IsActive())
        {
            _panelSetting.gameObject.SetActive(false);
        }
        else _panelSetting.gameObject.SetActive(true);
    }

}
