using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManagerUI : MonoBehaviour
{
    private TMP_Text _timeText;
    private int time = -1;
    private float _timeFloat;
    //event
    public static event Action OnTimestamp;
    private void Awake()
    {
        _timeText = GetComponent<TMP_Text>();
        if (_timeText == null)
        {
            Debug.LogError("TimeManagerUI: TMP_Text component not found!");
        }
    }
    public void OnEnable()
    {
        LoadLevelManager.OnStartTimer += SetTime;

    }
    public void OnDisable()
    {
        LoadLevelManager.OnStartTimer -= SetTime;

    }
    private void Update()
    {
        if (time != -1 && _timeFloat >0)
        {
            _timeFloat -= Time.deltaTime;
            int timeInt = (int)_timeFloat;
            if (timeInt <= 0)
            {
                OnTimestamp?.Invoke();
            }
            _timeText.text = timeInt.ToString();
        }

    }
    private void SetTime(int time)
    {
        this.time = time;
        _timeFloat = time;
    }

}
