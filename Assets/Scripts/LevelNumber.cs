using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    public Text Text;
    //Логика используемых скриптов
    public Game Game;

    private void Start()    //Номер уровня выводим на GameUI
    {
        Text.text = (Game.LevelIndex + 1).ToString();
    }
}