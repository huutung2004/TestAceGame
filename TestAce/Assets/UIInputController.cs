using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputController : MonoBehaviour
{
    public static UIInputController Instance;
    public bool isMoveRight;
    public bool isMoveLeft;
    public bool isJump;
    private void Awake()
    {
        Instance = this;
    }
    public void OnLeftDown() => isMoveLeft = true;
    public void OnLeftUp() => isMoveLeft = false;
    public void OnRightDown() => isMoveRight = true;
    public void OnRightUp() => isMoveRight = false;
    public void OnJumpDown() => isJump = true;
    public void OnJumpUp() => isJump = false;


}
