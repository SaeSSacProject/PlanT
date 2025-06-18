using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    [Header("Component")]
    private Button _button;
    private Image _image;
    private TextMeshProUGUI _text;

    [Header("Value")]
    public bool _IsSelected = false;
    public int _Index;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _selectedSprite;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        _button.onClick.AddListener(ClickButton);
    }

    public void ClickButton()
    {
        _IsSelected = !_IsSelected;
        _text.color = _IsSelected ? Color.black : Color.white;
        _image.sprite = _IsSelected ? _selectedSprite : _defaultSprite;
    }
}
