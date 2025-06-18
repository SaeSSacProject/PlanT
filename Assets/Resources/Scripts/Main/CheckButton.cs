using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    [Header("Component")]
    private Button _button;
    private Animator _animator;

    [Header("Reference")]
    private Reward _reward;
    private LayoutGroup _layout;

    [Header("Value")]
    public int _Index;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();

        _layout = FindFirstObjectByType<LayoutGroup>();
        _reward = FindFirstObjectByType<Main>()._Reward;

        _button.onClick.AddListener(delegate { StartCoroutine(CompleteTask()); });
    }

    private IEnumerator CompleteTask()
    {
        PlanT_Event.Disable();
        _animator.SetBool("isChecked", true);

        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        yield return MoveTask();

        yield return new WaitForEndOfFrame();

        PlanT_Event.Enable();
        _reward.ShowReward();

        _layout.DeleteSchedule(_Index);

        _layout.SetSchedule(_layout._Data.GetDay(_layout._DayIndex));
    }

    private IEnumerator MoveTask()
    {
        RectTransform itemRect = transform.parent.GetComponent<RectTransform>();
        Vector3 position = itemRect.anchoredPosition;

        float duration = 0.15f;
        float elapsed = -2.5f * Time.deltaTime;

        while (elapsed < duration)
        {
            position.x += itemRect.sizeDelta.x * Time.deltaTime / duration;
            itemRect.anchoredPosition = position;

            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}
