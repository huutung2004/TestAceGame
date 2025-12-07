using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public static event Action OnCompleteLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCompleteLevel?.Invoke();
            Debug.Log("CompleteLevel");
        }
    }
}
