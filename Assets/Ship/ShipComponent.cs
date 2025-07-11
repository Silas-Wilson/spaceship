using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [field: SerializeField] public ComponentStats Stats { get; private set; }
}
