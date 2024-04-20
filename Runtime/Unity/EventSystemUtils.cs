using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class EventSystemUtils
{
    public static bool HasSelectable(this EventSystem eventSystem)
    {
        return eventSystem.currentSelectedGameObject != null;
    }
    public static bool TryGetSelectable(this EventSystem eventSystem, out Selectable selectable)
    {
        if (!HasSelectable(eventSystem))
        {
            selectable = null;
            return false;
        }

        selectable = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        return true;
    }
}
