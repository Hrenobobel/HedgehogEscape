using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public Game game;

    public void PauseGame()
    {
        game.OnPauseEnter();
    }
    public void ContinueGame()
    {
        game.OnPauseExit();
    }
}