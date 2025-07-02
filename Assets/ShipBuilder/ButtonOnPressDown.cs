using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonOnPressDown : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnMouseDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button pressed down!");
        OnMouseDown.Invoke();
    }
}
