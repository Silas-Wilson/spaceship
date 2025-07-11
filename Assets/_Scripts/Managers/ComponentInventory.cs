using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInventory : MonoBehaviour
{
    public static ComponentInventory Instance;
    [SerializeField] int _inventorySize;
    [SerializeField] InventoryButton _button;
    [SerializeField] Image _inventoryUI;
    [field: SerializeField] public List<ShipComponent> components { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadInventory();
    }
    public void LoadInventory()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("componentButton");
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        for (int i = 0; i < components.Count; i++)
            {
                ShipComponent component = components[i];
                InventoryButton button = Instantiate(_button, _inventoryUI.transform);
                button.SetComponent(component);
                button.SetPosition(CalculatePosition(i));
            }
    }
    public void AddToInventory(ShipComponent component)
    {
        components.Add(component);
    }
    public void RemoveFromInventory(ShipComponent component)
    {
        components.Remove(component);
    }
    private Vector2 CalculatePosition(int index)
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
        return new Vector2(xPos, yPos);
    }
}
