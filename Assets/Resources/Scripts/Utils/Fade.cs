using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Header("Component")]
    private Image _fadeImage;

    private void Awake()
    {
        _fadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeIn(float time=1.0f)
    {
        Color color = _fadeImage.color;

        while(color.a > 0.0f)
        {
            color.a -= Time.deltaTime * time;
            _fadeImage.color = color;

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOut(float time=1.0f)
    {
        Color color = _fadeImage.color;

        gameObject.SetActive(true);

        while(color.a < 1.0f)
        {
            color.a += Time.deltaTime * time;
            _fadeImage.color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeIn(Image image, float time=1.0f)
    {
        Color color = image.color;

        while(color.a > 0.0f)
        {
            color.a -= Time.deltaTime * time;
            image.color = color;

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOut(Image image, float time=1.0f)
    {
        Color color = image.color;

        gameObject.SetActive(true);

        while(color.a < 1.0f)
        {
            color.a += Time.deltaTime * time;
            image.color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeIn(TextMeshProUGUI text, float time=1.0f)
    {
        Color color = text.color;

        while(color.a > 0.0f)
        {
            color.a -= Time.deltaTime * time;
            text.color = color;

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOut(TextMeshProUGUI text, float time=1.0f)
    {
        Color color = text.color;

        gameObject.SetActive(true);

        while(color.a < 1.0f)
        {
            color.a += Time.deltaTime * time;
            text.color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeIn(TextMeshPro text, float time=1.0f)
    {
        Color color = text.color;

        while(color.a > 0.0f)
        {
            color.a -= Time.deltaTime * time;
            text.color = color;

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOut(TextMeshPro text, float time=1.0f)
    {
        Color color = text.color;

        gameObject.SetActive(true);

        while(color.a < 1.0f)
        {
            color.a += Time.deltaTime * time;
            text.color = color;

            yield return new WaitForEndOfFrame();
        }
    }
}
