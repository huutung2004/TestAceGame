using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerUI : MonoBehaviour
{
    [SerializeField] private Image _listConainer;
    private void Start()
    {
        int totalMapUnlocked = LevelController.Instance.GetTotalMapUnlocked();
        if(_listConainer == null)
        {
            Debug.LogWarning("List Container is missing");

        }
        //foreach( )
       
    }
}
