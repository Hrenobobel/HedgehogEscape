using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Text text;
    //������ ������������ ��������
    public Game game;

    public void PauseGame()     //������ ����� � UI �����
    {
        game.OnPauseEnter();
    }
    public void ContinueGame()  //������ �������� �� UI �����
    {
        game.OnPauseExit();
    }
    public void Hints()
    {
        if ((game.StartLives > 1) && (game.LivesNumber > 1))
        {
            text.text = game.LevelHint();
            game.StartLives--;      //�� ������������� ��������� ��������� ���������� ������
            game.LivesNumber--;
        }
        else
            text.text = ("No More Lives");
    }
}