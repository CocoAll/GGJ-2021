using UnityEngine.SceneManagement;

using UnityEngine;

public class GameOverPanelActions : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
