using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Spiel wird beendet..."); // Funktioniert nur im Editor
        Application.Quit();
    }
}