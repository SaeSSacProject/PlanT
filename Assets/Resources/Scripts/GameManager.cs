using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }

            return _instance;
        }
    }
    private static GameManager _instance;

    [Header("Child")]
    public GameObject Canvas
    {
        get{ return _canvas; }
    }
    public Fade Fade
    {
        get{ return _fade; }
    }
    private GameObject _canvas;
    private Fade _fade;

    [Header("Component")]
    public Resolution Resolution
    {
        get{ return _resolution; }
    }
    private Resolution _resolution;

    private void Awake()
    {
        _resolution = new Resolution();

        _canvas = transform.GetChild(0).gameObject;
        Canvas.AddComponent<PlanT_Canvas>().AssignVariable();

        SetFade();

        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(Fade.FadeIn());
    }

    private void SetFade()
    {
        RectTransform fade = Canvas.transform.GetChild(0).GetComponent<RectTransform>();

        fade.sizeDelta = Canvas.GetComponent<PlanT_Canvas>().Scaler.referenceResolution;
        fade.AddComponent<Fade>();

        _fade = fade.GetComponent<Fade>();

        fade.gameObject.SetActive(false);
    }
}
