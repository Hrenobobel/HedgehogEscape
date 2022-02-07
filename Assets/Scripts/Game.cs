using UnityEngine;

public class Game : MonoBehaviour
{
    //Логика соседних скриптов
    public Player player;
    public Generator generator;
    public Controls controls;
    //Имеющиеся объекты UI
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject PauseUI;
    //Положение "головы" группы ежей
    public Transform StartTransform;
    public Transform[] StartHengehogsPosition;
    //Начальный поворот группы ежей
    public float StartRotation;
    public float[] StartHengehogsRotation;
    //Имеющееся количество жизней
    public int LivesNumber;

    //Проверяем запущена ли игра
    private bool start;

    private void Start()
    {
        MenuUI.SetActive(true);
        GameUI.SetActive(false);
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        PauseUI.SetActive(false);

        LivesNumber = 3;
        start = true;
        player.EnableControls = false;
    }
    private void Update()       
    {
        if (start)              //Стартовое положения
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
        generator.LevelGenerator(StartTransform, StartRotation);
        player.EnableControls = true;
    }
    public void OnPlayerDie()   //Если закончились жизни
    {
        controls.PlayerAudio.PlayOneShot(controls.LoseAudio, 0.5f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        LoseUI.SetActive(true);
    }
    public void OnPlayerWin()   //Действия при достижении выхода
    {
        controls.PlayerAudio.PlayOneShot(controls.WinAudio, 1f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        WinUI.SetActive(true);
        LevelIndex++;
    }
    public void OnPauseEnter()
    {
        player.EnableControls = false;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    public void OnPauseExit()
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
}