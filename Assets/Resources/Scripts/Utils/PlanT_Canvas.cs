using UnityEngine;
using UnityEngine.UI;

public class PlanT_Canvas : MonoBehaviour
{
    [HideInInspector] public Canvas Canvas
    {
        get{ return _canvas; }
    }
    [HideInInspector] public CanvasScaler Scaler
    {
        get{ return _scaler; }
    }
    private Canvas _canvas;
    private CanvasScaler _scaler;

    public virtual void AssignVariable()
    {
        _canvas = GetComponent<Canvas>();
        _scaler = GetComponent<CanvasScaler>();

        SetScaler();
    }

    private void SetScaler()
    {
        Scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        Scaler.referenceResolution = new Vector2(
            GameManager.Instance.Resolution.Width, 
            GameManager.Instance.Resolution.Height
        );
        Scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;
        Scaler.referencePixelsPerUnit = 108.0f;
    }
}
