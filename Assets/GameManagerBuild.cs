using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBuild : MonoBehaviour
{
    public void FinishBuilding()
    {
        SceneManager.LoadScene("Space");
    }
}
