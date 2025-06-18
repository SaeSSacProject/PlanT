using UnityEngine;

public class Main : PlanT_Canvas
{
    [Header("Reference")]
    public Reward _Reward;
    private RectTransform _topBarRect;
    private RectTransform _layoutRect;
    private RectTransform _bottomBarRect;

    private void Awake()
    {
        AssignVariable();
    }

    public override void AssignVariable()
    {
        base.AssignVariable();

        _topBarRect = transform.GetChild(0).GetComponent<RectTransform>();
        _layoutRect = transform.GetChild(1).GetComponent<RectTransform>();
        _bottomBarRect = transform.GetChild(2).GetComponent<RectTransform>();

        _topBarRect.sizeDelta = Scaler.referenceResolution * new Vector2(1.0f, 0.15f);
        _layoutRect.sizeDelta = Scaler.referenceResolution * new Vector2(1.0f, 0.75f);
        _bottomBarRect.sizeDelta = Scaler.referenceResolution * new Vector2(1.0f, 0.1f);

        _topBarRect.GetComponent<TopBar>().AssignVariable();
        _layoutRect.GetComponent<LayoutGroup>().AssignVariable();
        _bottomBarRect.GetComponent<BottomBar>().AssignVariable();
    }
}
