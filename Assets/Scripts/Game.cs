using UnityEngine;

public class Game : MonoBehaviour
{
    //Логика используемых скриптов
    public Player player;
    public Generator generator;
    public Controls controls;
    //Имеющиеся объекты UI
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject PauseUI;
    //Имеющееся количество жизней
    public int LivesNumber;

    //Проверяем запущена ли игра
    private bool start;

    private void Start()        //Открываем начальный UI
    {
        MenuUI.SetActive(true);
        GameUI.SetActive(false);
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        PauseUI.SetActive(false);

        LivesNumber = StartLives;
        start = true;
        player.EnableControls = false;
    }
    private void Update()       
    {
        if (start)              //Ждем от игрока начального ввода для запуска уровня
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                OnPlayerStart();
        }
    }
    public void OnPlayerStart() //Запуск игры из стартового положения
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
        start = false;
        generator.LevelGenerator(LevelIndex);
        player.EnableControls = true;
    }
    public void OnPlayerDie()   //Если закончились жизни
    {
        controls.PlayerAudio.PlayOneShot(controls.LoseAudio, 0.25f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        LoseUI.SetActive(true);
    }
    public void OnPlayerWin()   //Действия при достижении выхода
    {
        controls.PlayerAudio.PlayOneShot(controls.WinAudio, 0.75f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        WinUI.SetActive(true);
        if (LevelIndex < 9)
        {
            LevelIndex++;
            if (LevelIndex > 3)
                StartLives++;   //После прохождения 4 уровня даём дополнительную стартовую жизнь за успешное прохождение
        }            
        else
            LevelIndex = 0;
    }
    public void OnPauseEnter()  //Меню паузы
    {
        player.EnableControls = false;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    public void OnPauseExit()   //Выход из меню паузы
    {
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
        player.EnableControls = true;
    }
    public void OnPlayerHurt()  //При касании врага уменьшаем количество жизней
    {
        if (LivesNumber > 1)
            LivesNumber--;
        else
            OnPlayerDie();
    }
    public string LevelHint()
    {
        return generator.Hint[LevelIndex];
    }
    public int LevelIndex       //Номер уровня храним в PlayerPrefs
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";
    public int StartLives       //Количество жизней храним в PlayerPrefs
    {
        get => PlayerPrefs.GetInt(StartLivesKey, 3);
        set
        {
            PlayerPrefs.SetInt(StartLivesKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string StartLivesKey = "StartLives";
}