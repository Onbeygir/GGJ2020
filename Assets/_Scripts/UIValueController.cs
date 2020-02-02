using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIValueController : MonoBehaviour
{
    public TextMeshProUGUI ValueText;
    public Button IncreaseButton;
    public Button DecreaseButton;

    [SerializeField] private int _startValue = 0;
    [SerializeField] private int _currentValue = 0;
    [SerializeField] private int _maxValue = 10;

    public int CurrentValue { get { return _currentValue; } }


    private void Awake()
    {
        IncreaseButton.onClick.AddListener(OnValueUp);
        DecreaseButton.onClick.AddListener(OnValueDown);

        
        _currentValue = _startValue;
        UpdateValueText();
    }

    private void OnValueUp()
    {
        _currentValue++;
        if(_currentValue >= _maxValue)
        {
            _currentValue = _maxValue;
        }
        UpdateValueText();
    }

    private void OnValueDown()
    {
        _currentValue--;
        if (_currentValue < 0)
        {
            _currentValue = 0;
        }
        UpdateValueText();
    }

    private void UpdateValueText()
    {
        ValueText.text = _currentValue.ToString();
    }
}
