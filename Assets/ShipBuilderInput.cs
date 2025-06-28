using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public string playerName;

    public void OnSubmit()
    {
        playerName = inputField.text;
        Debug.Log("Player name is: " + playerName);
    }
}
