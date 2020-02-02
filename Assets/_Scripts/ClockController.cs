using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ClockController : MonoBehaviour
{
    public TextMeshProUGUI ClockText;
    [SerializeField] private float _currentTime;
    private int _prevSecond;

    private UnityAction timeUp;

    private void Awake()
    {
        enabled = false;
    }

    public void StartClock(float ClockTime, UnityAction onTimeUp)
    {
        _currentTime = ClockTime;
        _prevSecond = Mathf.FloorToInt(_currentTime);
        enabled = true;
        timeUp = onTimeUp;
    }

    public void StopClock()
    {
        enabled = false;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        int currentSecond = Mathf.FloorToInt(_currentTime);
        if (_currentTime <= 0)
        {
            _currentTime = 0;
            if (currentSecond < 0) currentSecond = 0;
            ClockText.text = currentSecond.ToString();
            timeUp?.Invoke();
            StopClock();            
        }
        else if (currentSecond < _prevSecond)
        {
            _prevSecond = currentSecond;
            ClockText.text = currentSecond.ToString();
        }


    }
}
