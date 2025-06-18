using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : PlanT_Canvas
{
    [Header("Reference")]
    private TextMeshProUGUI _loadingText;
    private TextMeshPro _logoText;
    private TextMeshPro _oneLineText;

    private void Awake()
    {
        AssignVariable();
    }

    public override void AssignVariable()
    {
        base.AssignVariable();

        _loadingText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _logoText = GameObject.Find("LogoText").GetComponent<TextMeshPro>();
        _oneLineText = GameObject.Find("OneLineText").GetComponent<TextMeshPro>();

        StartCoroutine(LoadingProcess());
    }

    private IEnumerator LoadingProcess()
    {
        yield return new WaitForSeconds(0.5f);

        yield return GameManager.Instance.Fade.FadeOut(_loadingText, 2.0f);

        string text = _loadingText.text;
        float duration = 0.0f;
        int count = 0;

        while(duration < 3.0f)
        {
            if(count == 3)
            {
                count = -1;
                _loadingText.text = text;
            }
            else
            {
                _loadingText.text += '.';
            }

            count++;
            duration += 0.5f;

            yield return new WaitForSeconds(0.5f);
        }

        yield return GameManager.Instance.Fade.FadeIn(_loadingText, 3f);

        duration = 0.0f;

        while(duration < 1.0f)
        {
            Color cameraColor = Camera.main.backgroundColor;
            Color targetColor = new Color(0.85f, 0.875f, 0.685f); // #D9DFAF
            Camera.main.backgroundColor = Color.Lerp(cameraColor, targetColor, duration);

            duration += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return GameManager.Instance.Fade.FadeOut(_logoText, 2.5f);

        yield return GameManager.Instance.Fade.FadeOut(_oneLineText, 2.5f);

        yield return new WaitForSeconds(2.0f);

        yield return GameManager.Instance.Fade.FadeOut();

        SceneManager.LoadScene("MainScene");
    }
}
