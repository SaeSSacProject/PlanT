using UnityEngine;
using UnityEngine.EventSystems;

public class PlanT_Event : MonoBehaviour
{
    public static void Enable()
    {
        EventSystem system = FindFirstObjectByType<EventSystem>();

        system.enabled = true;
    }

    public static void Disable()
    {
        EventSystem system = FindFirstObjectByType<EventSystem>();

        system.enabled = false;
    }
}
