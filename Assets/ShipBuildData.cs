using UnityEngine;
using System.Collections.Generic;
public class ShipBuildData : MonoBehaviour
{
    public static ShipBuildData Instance;

    public Dictionary<Vector2Int, ShipComponent> ShipBlueprint = new Dictionary<Vector2Int, ShipComponent>();
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
    }
    void Start()
    {
        ShipBlueprint.Add(new Vector2Int(0, 0), CorePrefab);

        //For Debug Purposes:
        ShipBlueprint.Add(new Vector2Int(0, 1), GunPrefabFOR_DEBUG);
        ShipBlueprint.Add(new Vector2Int(0, -1), GunPrefabFOR_DEBUG);

        BuildShip(Vector2.zero);
    }

    public void AddShipComponent(Vector2Int location, ShipComponent type)
    {
        if (ShipBlueprint[location] == null)
        {
            ShipBlueprint.Add(location, type);
        }
        else
        {
            Debug.Log("ERROR: There's already a component here!");
        }
    }

    public void BuildShip(Vector2 location)
    {
        ShipStats activeShip = Instantiate(ShipPrefab, location, Quaternion.identity);

        foreach (var pair in ShipBlueprint)
        {
            ShipComponent comp = Instantiate(pair.Value, activeShip.transform);
            comp.transform.localPosition = new Vector3(pair.Key.x, pair.Key.y, 0);
        }
    }
}
