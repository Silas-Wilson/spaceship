using UnityEngine;
using System.Collections.Generic;
public class ShipBuildData : MonoBehaviour
{
    public static ShipBuildData Instance;
    public ComponentGrid Grid { get; private set; }
    private ShipStats _activeShip;
    [SerializeField] ShipStats ShipPrefab;
    [SerializeField] ShipComponent CorePrefab;
    [SerializeField] ShipComponent GunPrefabFOR_DEBUG;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Grid = new ComponentGrid();
    }
    void Start()
    {
        Grid.AddComponent(CorePrefab, Vector2Int.zero, Quaternion.identity);

        BuildShip(Vector2.zero);
    }
    public ShipStats BuildShip(Vector2 location)
    {
        if (_activeShip == null)
        {
            _activeShip = Instantiate(ShipPrefab, location, Quaternion.identity);
        }
        BuildShipComponents();
        _activeShip.UpdateShipStats();
        return _activeShip;
    }
    private void BuildShipComponents()
    {
        //Destroy all current components before adding components
        foreach (Transform child in _activeShip.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var compData in Grid.GetAllValues())
        {
            ShipComponent componentAdded = Instantiate(compData.component, _activeShip.transform);
            componentAdded.transform.rotation = compData.rotation;
            componentAdded.transform.localPosition = new Vector3Int(compData.position.x, compData.position.y, 0);
        }
    }
}

public class ComponentGrid
{
    private List<ShipComponentData> _grid = new();
    public bool AddComponent(ShipComponent component, Vector2Int location, Quaternion rotation)
    {
        if (IsInValidLocation(location))
        {
            _grid.Add(new ShipComponentData(component, location, rotation));
            return true;
        }
        return false;
    }
    public ShipComponent RemoveComponent(Vector3Int loc)
    {
        Vector2Int loc2d = new Vector2Int(loc.x, loc.y);
        if (loc2d == Vector2Int.zero)
        {
            Debug.LogWarning("You cannot remove the ship's core!");
            return null;
        }
        foreach (ShipComponentData compData in _grid)
            {
                if (compData.position == loc2d)
                {
                    _grid.Remove(compData);
                    return compData.component;
                }
            }
        return null;
    }
    public List<ShipComponentData> GetAllValues()
    {
        return _grid;
    }
    public ShipComponent[] GetAllComponents()
    {
        ShipComponent[] components = new ShipComponent[_grid.Count];
        for (int i = 0; i < components.Length; i++)
        {
            components[i] = _grid[i].component;
        }
        return components;
    }
    public ShipComponent GetComponentAt(Vector2Int location)
    {
        foreach (ShipComponentData data in _grid)
        {
            if (data.position == location)
            {
                return data.component;
            }
        }
        return null;
    }
    public Vector2Int GetLocationOf(ShipComponent comp)
    {
        foreach (ShipComponentData data in _grid)
        {
            if (data.component == comp)
            {
                return data.position;
            }
        }
        return Vector2Int.zero;
    }
    private bool IsInValidLocation(Vector2Int location)
    {
        // Allows the first component (The core) to be added no matter what.
        if (_grid.Count < 1)
        {
            return true;
        }
        foreach (ShipComponentData data in _grid)
        {
            if (data.position == location)
            {
                Debug.LogWarning($"Location {location} already occupied!");
                return false;
            }
        }
        foreach (ShipComponentData data in _grid)
        {
            if (data.position == location + Vector2.up)
            {
                return true;
            }
            if (data.position == location + Vector2.down)
            {
                return true;
            }
            if (data.position == location + Vector2.right)
            {
                return true;
            }
            if (data.position == location + Vector2.left)
            {
                return true;
            }
        }
        Debug.LogWarning($"Location {location} is not connected to this ship!");
        return false;
    }
}
public class ShipComponentData
{
    public ShipComponent component { get; private set; }
    public Vector2Int position { get; private set; }
    public Quaternion rotation { get; private set; }

    public ShipComponentData(ShipComponent c, Vector2Int p, Quaternion r)
    {
        component = c;
        position = p;
        rotation = r;
    }
}