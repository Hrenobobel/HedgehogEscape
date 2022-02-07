using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void ReloadLevel()   //Перезапуск уровня
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()          //Выход из приложения
    {
        Application.Quit();
    }
}