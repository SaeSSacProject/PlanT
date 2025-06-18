using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Reward : PlanT_Canvas
{
    [Header("Reference")]
    private Image _rewardImage;
    private TextMeshProUGUI _text;

    [Header("Value")]
    [SerializeField] private Sprite[] _imageList;
    [SerializeField] private string[] _nameList;

    private void Awake()
    {
        AssignVariable();
    }

    public override void AssignVariable()
    {
        base.AssignVariable();

        // Background
        RectTransform background = transform.GetChild(0).GetComponent<RectTransform>();
        Button backgroundButton = background.GetComponent<Button>();

        background.sizeDelta = GetComponent<RectTransform>().sizeDelta;
        backgroundButton.onClick.AddListener(delegate { gameObject.SetActive(false); });

        // RewardGroup
        RectTransform rewardGroup = transform.GetChild(1).GetComponent<RectTransform>();
        rewardGroup.sizeDelta = background.sizeDelta * new Vector2(0.8f, 0.4f);

        // Reward
        RectTransform rewardRect = rewardGroup.transform.GetChild(0).GetComponent<RectTransform>();
        float minSize = Mathf.Min(rewardGroup.sizeDelta.x, rewardGroup.sizeDelta.y);

        rewardRect.sizeDelta = minSize * 0.5f * Vector2.one;
        rewardRect.anchoredPosition += new Vector2(0.0f, rewardGroup.sizeDelta.y * 0.1f);
        _rewardImage = rewardRect.GetComponent<Image>();

        // Name
        RectTransform nameRect = rewardGroup.transform.GetChild(1).GetComponent<RectTransform>();

        nameRect.sizeDelta = rewardRect.sizeDelta * new Vector2(1.0f, 0.15f);
        nameRect.anchoredPosition -= new Vector2(0.0f, rewardGroup.sizeDelta.y * 0.25f);
        _text = nameRect.GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void ShowReward()
    {
        gameObject.SetActive(true);

        GetPlant();
    }

    private void GetPlant()
    {
        int index = Random.Range(0, 8);
        _rewardImage.sprite = _imageList[index];
        _text.text = _nameList[index];
    }
}
