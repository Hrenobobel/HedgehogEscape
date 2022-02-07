using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Text text;
    //Логика используемых скриптов
    public Game game;

    public void PauseGame()     //Кнопка входа в UI паузы
    {
        game.OnPauseEnter();
    }
    public void ContinueGame()  //Кнопка возврата из UI паузы
    {
        game.OnPauseExit();
    }
    public void Hints()
    {
        if ((game.StartLives > 1) && (game.LivesNumber > 1))
        {
            text.text = game.LevelHint();
            game.StartLives--;      //За использование подсказки уменьшаем количество жизней
            game.LivesNumber--;
        }
        else
            text.text = ("No More Lives");
    }
}