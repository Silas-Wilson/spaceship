using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] ShipComponent _associatedComponent;
    [SerializeField] Image buttonSprite;
    public void SetComponent(ShipComponent component)
    {
        _associatedComponent = component;
    }
    public void SetPosition(int index)
    {
        float yPos;
        float xPos;
        if (index % 2 == 0)
        {
            xPos = 8;
        }
        else
        {
            xPos = 40;
        }

        yPos = ((index + 1 - index % 2) * -16) + 8;
        
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(xPos, yPos);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ShipConstructManager.Instance.CreateDraggableComponent(_associatedComponent);
        ComponentInventory.Instance.components.Remove(_associatedComponent);
        Destroy(gameObject);
    }
    void Start()
    {
        Sprite sourceSprite = _associatedComponent.GetComponent<SpriteRenderer>().sprite;
        buttonSprite.sprite = sourceSprite;
    }
}
