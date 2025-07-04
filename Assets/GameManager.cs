using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        PlayerInput playerInput = ShipBuildData.Instance.BuildShip(Vector2.zero).GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }
}
