using System;
using UnityEngine;
using UnityEngine.UI;

public class DateGroup : MonoBehaviour
{
    [Header("Component")]
    private Button _button;

    [Header("Reference")]
    private TopBar _topBar;
    private LayoutGroup _layout;

    [Header("Value")]
    [SerializeField] private int _index;

    #region Attribute
    private enum Week
    {
        Sun = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thr = 4,
        Fri = 5,
        Sat = 6
    }
    #endregion

    private void Awake()
    {
        _button = GetComponent<Button>();
        _topBar = FindFirstObjectByType<TopBar>();
        _layout = FindFirstObjectByType<LayoutGroup>();

        _button.onClick.AddListener(delegate { ClickEvents(_index); });
    }

    public void ClickEvents(int index)
    {
        _topBar.CheckMonth(index);
        _layout.SetColor(index);
        _layout._DayIndex = index;
        _layout.SetSchedule(_layout._Data.GetDay(_index));
    }
}

[Serializable]
public class Schedule
{
    public string name;
    public string date;
}

[Serializable]
public class ScheduleData
{
    public Schedule[] Mon;
    public Schedule[] Tue;
    public Schedule[] Wed;
    public Schedule[] Thr;
    public Schedule[] Fri;
    public Schedule[] Sat;
    public Schedule[] Sun;

    public Schedule[] GetDay(int index)
    {
        return index switch
        {
            0 => Sun,
            1 => Mon,
            2 => Tue,
            3 => Wed,
            4 => Thr,
            5 => Fri,
            6 => Sat,
            _ => null
        };
    }

    public void SetDay(int index, Schedule[] newData)
    {
        switch (index)
        {
            case 0:
                Sun = newData;
                break;
            case 1:
                Mon = newData;
                break;
            case 2:
                Tue = newData;
                break;
            case 3:
                Wed = newData;
                break;
            case 4:
                Thr = newData;
                break;
            case 5:
                Fri = newData;
                break;
            case 6:
                Sat = newData;
                break;                
        }
    }
}