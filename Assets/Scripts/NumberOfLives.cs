using UnityEngine.UI;
using UnityEngine;

public class NumberOfLives : MonoBehaviour
{
    public Text text;
    //������ ������������ ��������
    public Game game;

    private void Update()   //���������� ������ ������� �� GameUI
    {
        text.text = game.LivesNumber.ToString();
    }
}