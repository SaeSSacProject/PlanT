using System;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    [Header("Reference")]
    private RectTransform _dateRect;
    private RectTransform _settingRect;

    [Header("Value")]
    [SerializeField] private string _dateFormat;
    private Vector2 _size;

    public void AssignVariable()
    {
        _dateRect = transform.GetChild(0).GetComponent<RectTransform>();
        _settingRect = transform.GetChild(1).GetComponent<RectTransform>();
        _size = GetComponent<RectTransform>().sizeDelta;

        _dateRect.sizeDelta = _size * new Vector2(0.85f, 1.0f);
        _settingRect.sizeDelta = _size.y * new Vector2(0.06f, 0.24f);

        SetDate(DateTime.Now);
    }

    public void CheckMonth(int index)
    {
        int dayIndex = (int)DateTime.Now.DayOfWeek;
        DateTime date = DateTime.Now.AddDays(index - dayIndex);

        if (date.Month != DateTime.Now.Month)
        {
            SetDate(date);
        }
    }

    private void SetDate(DateTime date)
    {
        TextMeshProUGUI text = _dateRect.GetComponent<TextMeshProUGUI>();

        text.text = date.ToString(_dateFormat);
        text.fontSize = _dateRect.sizeDelta.y / 5.0f;
    }
}
