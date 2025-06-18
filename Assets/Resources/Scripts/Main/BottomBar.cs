using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    [Header("Reference")]
    private LayoutGroup _layout;
    private RectTransform _addRect;

    public void AssignVariable()
    {
        _layout = FindFirstObjectByType<LayoutGroup>();
        _addRect = transform.GetChild(0).GetComponent<RectTransform>();

        _addRect.sizeDelta = GetComponent<RectTransform>().sizeDelta;

        StartCoroutine(InitBottomBar());
    }

    public IEnumerator EnableAddContents()
    {
        PlanT_Event.Disable();

        RectTransform contentsRect = _addRect.transform.GetChild(1).GetComponent<RectTransform>();
        Vector3 position = contentsRect.anchoredPosition;

        float duration = 0.3f;
        float elapsed = -Time.deltaTime * 2.0f;

        _addRect.transform.GetChild(0).gameObject.SetActive(true);

        while (elapsed < duration)
        {
            position.y += contentsRect.sizeDelta.y * Time.deltaTime / duration;
            contentsRect.anchoredPosition = position;

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        position.y += contentsRect.sizeDelta.y * Time.deltaTime / duration;

        PlanT_Event.Enable();
    }

    private IEnumerator DisableAddContents()
    {
        PlanT_Event.Disable();

        RectTransform contentsRect = _addRect.transform.GetChild(1).GetComponent<RectTransform>();
        Vector3 position = contentsRect.anchoredPosition;

        float duration = 0.3f;
        float elapsed = -Time.deltaTime * 2.0f;

        _addRect.transform.GetChild(0).gameObject.SetActive(false);

        while (elapsed < duration)
        {
            position.y -= contentsRect.sizeDelta.y * Time.deltaTime / duration;
            contentsRect.anchoredPosition = position;

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        position.y -= contentsRect.sizeDelta.y * Time.deltaTime / duration;

        ClearAddContents();

        PlanT_Event.Enable();
    }

    private void SetAdd()
    {
        // Background
        RectTransform background = _addRect.transform.GetChild(0).GetComponent<RectTransform>();
        Button backgroundButton = background.GetComponent<Button>();


        background.sizeDelta = new Vector2(GameManager.Instance.Resolution.Width, GameManager.Instance.Resolution.Height);
        backgroundButton.onClick.AddListener(delegate { StartCoroutine(DisableAddContents()); });

        // Contents
        RectTransform contents = _addRect.transform.GetChild(1).GetComponent<RectTransform>();

        contents.sizeDelta = background.sizeDelta * new Vector2(0.9f, 0.25f);

        // Schedule Input
        RectTransform scheduleRect = contents.transform.GetChild(0).GetComponent<RectTransform>();
        TMP_InputField inputSchedule = scheduleRect.GetComponent<TMP_InputField>();

        scheduleRect.sizeDelta = contents.sizeDelta * new Vector2(0.96f, 0.15f);
        scheduleRect.anchoredPosition -= contents.sizeDelta * 0.26f * Vector2.up;

        // Day Buttons
        RectTransform dayGroup = contents.transform.GetChild(1).GetComponent<RectTransform>();
        HorizontalLayoutGroup dayHorizontal = dayGroup.GetComponent<HorizontalLayoutGroup>();

        dayGroup.sizeDelta = contents.sizeDelta * new Vector2(0.96f, 0.2f);
        dayGroup.anchoredPosition = scheduleRect.anchoredPosition - new Vector2(0.0f, scheduleRect.sizeDelta.y + contents.sizeDelta.y * 0.05f);
        dayHorizontal.padding.left = (int)(contents.sizeDelta.x * 0.02f);
        dayHorizontal.padding.right = (int)(contents.sizeDelta.x * 0.02f);
        dayHorizontal.spacing = (int)(contents.sizeDelta.x * 0.005f);

        RectTransform dateGroup = contents.transform.GetChild(2).GetComponent<RectTransform>();
        RectTransform monthRect = dateGroup.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform slash = dateGroup.transform.GetChild(1).GetComponent<RectTransform>();
        RectTransform dayRect = dateGroup.transform.GetChild(2).GetComponent<RectTransform>();
        TMP_InputField inputMonth = monthRect.GetComponent<TMP_InputField>();
        TMP_InputField inputDay = dayRect.GetComponent<TMP_InputField>();

        dateGroup.sizeDelta = contents.sizeDelta * new Vector2(0.4f, 0.15f);
        dateGroup.anchoredPosition = dayGroup.anchoredPosition - new Vector2(0.0f, dayGroup.sizeDelta.y + contents.sizeDelta.y * 0.05f);
        dateGroup.anchoredPosition += new Vector2((int)(contents.sizeDelta.x * 0.05f), 0.0f);
        monthRect.sizeDelta = dateGroup.sizeDelta * new Vector2(0.4f, 1.0f);
        slash.sizeDelta = dateGroup.sizeDelta * new Vector2(0.1f, 1.0f);
        dayRect.sizeDelta = dateGroup.sizeDelta * new Vector2(0.4f, 1.0f);

        inputMonth.onEndEdit.AddListener(delegate { ChangeValue(inputMonth, 12); });
        inputDay.onEndEdit.AddListener(delegate { ChangeValue(inputDay, -1); });

        // Add Button
        RectTransform addRect = contents.transform.GetChild(3).GetComponent<RectTransform>();
        Button addButton = addRect.GetComponent<Button>();

        addRect.sizeDelta = contents.sizeDelta.y * 0.75f * new Vector2(0.3f, 0.2f);
        addRect.anchoredPosition = dayGroup.anchoredPosition - new Vector2(0.0f, dayGroup.sizeDelta.y + contents.sizeDelta.y * 0.1f);
        addRect.anchoredPosition += new Vector2(-(int)(contents.sizeDelta.x * 0.075f), 0.0f);

        addButton.onClick.AddListener(delegate { CreateSchedule(inputSchedule.text, inputMonth.text, inputDay.text, dayGroup.gameObject); });

        background.gameObject.SetActive(false);
    }

    private void ChangeValue(TMP_InputField field, int limit)
    {
        if (field.text == "")
        {
            return;
        }
        else if (int.Parse(field.text) < 10 && field.text.Length < 2)
        {
            field.text = $"0{field.text}";
        }

        if (limit == -1)
        {
            string month = field.transform.parent.GetChild(0).GetComponent<TMP_InputField>().text;

            if (month == "")
            {
                return;
            }

            int month2int = int.Parse(month);
            int lastDate = DateTime.DaysInMonth(2025, month2int);

            field.text = int.Parse(field.text) > lastDate ? $"{lastDate}" : field.text;
        }
        else
        {
            field.text = int.Parse(field.text) > limit ? $"{limit}" : field.text;
        }
    }

    private void CreateSchedule(string name, string month, string day, GameObject dayGroup)
    {
        StartCoroutine(DisableAddContents());

        if (name == "" || name == null)
        {
            return;
        }

        List<int> dayList = new List<int>();

        for (int i = 0; i < dayGroup.transform.childCount; i++)
        {
            DayButton button = dayGroup.transform.GetChild(i).GetComponent<DayButton>();

            if (button._IsSelected)
            {
                dayList.Add(button._Index);
            }
        }

        AdditionalSchedule schedule = new AdditionalSchedule
        {
            schedule = name,
            days = dayList,
            date = new string[2] { month, day }
        };

        _layout.AddSchedule(schedule);
    }

    private void ClearAddContents()
    {
        GameObject contents = _addRect.transform.GetChild(1).gameObject;

        TMP_InputField inputSchedule = contents.transform.GetChild(0).GetComponent<TMP_InputField>();

        inputSchedule.text = "";

        for (int i = 0; i < 7; i++)
        {
            DayButton button = contents.transform.GetChild(1).GetChild(i).GetComponent<DayButton>();
            if (button._IsSelected) button.ClickButton();
        }

        TMP_InputField inputMonth = contents.transform.GetChild(2).GetChild(0).GetComponent<TMP_InputField>();
        TMP_InputField inputDay = contents.transform.GetChild(2).GetChild(2).GetComponent<TMP_InputField>();

        inputMonth.text = "";
        inputDay.text = "";
    }

    private IEnumerator InitBottomBar()
    {
        SetAdd();

        yield return new WaitForEndOfFrame();
    }
}

public class AdditionalSchedule
{
    public string schedule;
    public List<int> days;
    public string[] date;
}
