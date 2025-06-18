using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LayoutGroup : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject _scheduleItem;

    [Header("Child")]
    private GameObject _content;

    [Header("Reference")]
    private RectTransform _weekRect;
    private RectTransform _taskRect;

    [Header("Value")]
    public ScheduleData _Data;
    public int _DayIndex;
    private Vector2 _size;

    public void AssignVariable()
    {
        _weekRect = transform.GetChild(0).GetComponent<RectTransform>();
        _taskRect = transform.GetChild(1).GetComponent<RectTransform>();
        _content = _taskRect.GetChild(1).GetChild(0).GetChild(0).gameObject;

        _size = GetComponent<RectTransform>().sizeDelta;

        _weekRect.sizeDelta = new Vector2(_size.x, _size.y * 0.15f);
        _taskRect.sizeDelta = new Vector2(_size.x, _size.y * 0.85f);

        StartCoroutine(InitLayout());
    }

    public void SetColor(int index)
    {
        for (int i = 0; i < _weekRect.transform.childCount; i++)
        {
            // Week -> Day -> Date
            Image image = _weekRect.transform.GetChild(i).GetChild(1).GetComponent<Image>();

            if (i == index)
            {
                image.color = new Color(0.21f, 0.93f, 0.58f);
            }
            else
            {
                image.color = new Color(1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void SetSchedule(Schedule[] schedules)
    {
        ClearSchedule();

        Vector2 position = Vector2.zero;

        for (int i = 0; i < schedules.Length; i++)
        {
            GameObject item = Instantiate(_scheduleItem);
            TextMeshProUGUI scheduleText = item.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dateText = item.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();

            RectTransform backgroundRect = _taskRect.transform.GetChild(0).GetComponent<RectTransform>();

            RectTransform itemRect = item.GetComponent<RectTransform>();
            RectTransform buttonRect = item.transform.GetChild(0).GetComponent<RectTransform>();
            RectTransform contentRect = item.transform.GetChild(1).GetComponent<RectTransform>();

            RectTransform scheduleRect = scheduleText.GetComponent<RectTransform>();
            RectTransform dateRect = dateText.GetComponent<RectTransform>();

            CheckButton checkButton = buttonRect.GetComponent<CheckButton>();
            checkButton._Index = i;

            itemRect.sizeDelta = new Vector2(0.95f, 0.15f) * backgroundRect.sizeDelta;

            buttonRect.sizeDelta = 0.5f * itemRect.sizeDelta.y * Vector2.one;
            contentRect.sizeDelta = new Vector2(0.79f, 0.6f) * itemRect.sizeDelta;
            contentRect.sizeDelta = new Vector2(
                itemRect.sizeDelta.x - buttonRect.sizeDelta.x - 0.05f * itemRect.sizeDelta.x, 0.6f * itemRect.sizeDelta.y
            );

            scheduleRect.sizeDelta = new Vector2(0.84f, 1.0f) * contentRect.sizeDelta;
            dateRect.sizeDelta = new Vector2(0.15f, 1.0f) * contentRect.sizeDelta;

            item.name = $"Schdule_{i}";
            scheduleText.text = schedules[i].name;

            if (schedules[i].date == "")
            {
                dateText.text = "--";
            }
            else
            {
                dateText.text = schedules[i].date;
            }

            item.transform.SetParent(_content.transform);
            item.GetComponent<RectTransform>().anchoredPosition = position;

            position.y -= itemRect.sizeDelta.y * 1.05f;
        }
    }

    public void ClearSchedule()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddSchedule(AdditionalSchedule schedule)
    {
        Schedule newSchedule = new Schedule();

        newSchedule.name = schedule.schedule;
        newSchedule.date = $"{schedule.date[0]}/{schedule.date[1]}";

        foreach (int index in schedule.days)
        {
            List<Schedule> scheduleList = new List<Schedule>();

            for (int i = 0; i < _Data.GetDay(index).Length; i++)
            {
                scheduleList.Add(_Data.GetDay(index)[i]);
            }

            scheduleList.Add(newSchedule);
            _Data.SetDay(index, scheduleList.ToArray());
        }

        SetData(_Data);

        SetSchedule(_Data.GetDay(_DayIndex));
    }

    public void DeleteSchedule(int index)
    {

        Schedule[] schedules = _Data.GetDay(_DayIndex);
        List<Schedule> newSchedule = new List<Schedule>();

        for (int i = 0; i < schedules.Length; i++)
        {
            if (i != index)
            {
                newSchedule.Add(schedules[i]);
            }
        }

        _Data.SetDay(_DayIndex, newSchedule.ToArray());

        SetData(_Data);

        SetSchedule(_Data.GetDay(_DayIndex));
    }

    private void SetData(ScheduleData data = null)
    {
        string path = $"{Application.persistentDataPath}/DataFile.json";

        if (File.Exists(path) == false)
        {
            ScheduleData initData = new();
            string jsonData = JsonUtility.ToJson(initData, prettyPrint: true);

            File.WriteAllText(path, jsonData);
        }

        if (data != null)
        {
            string jsonData = JsonUtility.ToJson(_Data, prettyPrint: true);
            File.WriteAllText(path, jsonData);
        }

        string file = File.ReadAllText(path);
        _Data = JsonUtility.FromJson<ScheduleData>(file);
    }

    private void SetWeek()
    {
        _DayIndex = (int)DateTime.Now.DayOfWeek;
        DateTime dayDate = DateTime.Now.AddDays(-_DayIndex);

        for (int i = 0; i < _weekRect.transform.childCount; i++)
        {
            RectTransform dayRect = _weekRect.transform.GetChild(i).GetComponent<RectTransform>();

            LayoutElement dayElement = dayRect.transform.GetChild(0).GetComponent<LayoutElement>();
            LayoutElement dateElement = dayRect.transform.GetChild(1).GetComponent<LayoutElement>();

            RectTransform dateRect = dateElement.GetComponent<RectTransform>();
            RectTransform textRect = dateRect.transform.GetChild(0).GetComponent<RectTransform>();
            TextMeshProUGUI dateText = dateRect.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            dayRect.sizeDelta = new Vector2(dayRect.sizeDelta.x, _weekRect.sizeDelta.y * 0.75f);

            dayElement.preferredHeight = dayRect.sizeDelta.y * 0.2f;
            dateElement.preferredHeight = dayRect.sizeDelta.y * 0.8f;

            dateRect.sizeDelta = 0.5f * dateElement.preferredHeight * Vector2.one;
            textRect.sizeDelta = dateRect.sizeDelta;
            dateText.text = $"{dayDate.Day}";

            dayDate = dayDate.AddDays(1);
        }

        SetColor(_DayIndex);
    }

    private void SetTask()
    {
        // Background
        RectTransform background = _taskRect.transform.GetChild(0).GetComponent<RectTransform>();

        background.sizeDelta = new Vector2(0.98f, 0.95f) * _taskRect.sizeDelta;

        // ScrollView
        RectTransform scroll = _taskRect.transform.GetChild(1).GetComponent<RectTransform>();
        scroll.sizeDelta = background.sizeDelta;

        // AddButton
        RectTransform addRect = _taskRect.transform.GetChild(2).GetComponent<RectTransform>();
        Button addButton = addRect.GetComponent<Button>();
        float squareSize = Mathf.Min(background.sizeDelta.x, background.sizeDelta.y);
        Vector2 marginPosition = 0.05f * new Vector2(background.sizeDelta.x, -background.sizeDelta.y);

        addRect.sizeDelta = 0.1f * squareSize * Vector2.one;
        addRect.anchoredPosition = new Vector2(addRect.anchoredPosition.x - marginPosition.x, addRect.anchoredPosition.y - marginPosition.y);

        addButton.onClick.AddListener(delegate { StartCoroutine(FindFirstObjectByType<BottomBar>().EnableAddContents()); });

        SetSchedule(_Data.GetDay((int)DateTime.Now.DayOfWeek));
    }
    
    private IEnumerator InitLayout()
    {
        SetData();

        yield return new WaitForEndOfFrame();

        SetWeek();

        yield return new WaitForEndOfFrame();

        SetTask();

        yield return new WaitForEndOfFrame();
    }
}
