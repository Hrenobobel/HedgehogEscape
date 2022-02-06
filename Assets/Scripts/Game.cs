using UnityEngine;

public class Game : MonoBehaviour
{

    //Логика группы ежей
    public Player player;

    public Generator generator;
    public Controls controls;

    //Имеющиеся объекты UI
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject WinUI;
    public GameObject LoseUI;    

    private void Start()
    {
        MenuUI.SetActive(true);
        GameUI.SetActive(false);
        WinUI.SetActive(false);
        LoseUI.SetActive(false);

        generator.Level_0();
        player.EnableControls = false;
    }
        public void OnPlayerStart()
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
        player.EnableControls = true;
    }
    public void OnPlayerDie()
    {
        player.EnableControls = false;
        GameUI.SetActive(false);
        LoseUI.SetActive(true);
    }
    public void OnPlayerWin()
    {
        player.EnableControls = false;
        GameUI.SetActive(false);
        WinUI.SetActive(true);
    }
}
