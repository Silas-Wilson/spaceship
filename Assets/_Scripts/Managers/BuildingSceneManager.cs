using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingSceneManager : MonoBehaviour
{
    public void OpenSpaceScene()
    {
        SceneManager.LoadScene("Space");
        ShipBuildData.Instance.BuildShip(Vector2.zero);
    }
}
