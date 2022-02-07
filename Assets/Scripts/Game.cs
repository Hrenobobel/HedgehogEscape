using UnityEngine;

public class Game : MonoBehaviour
{
    //������ ������������ ��������
    public Player player;
    public Generator generator;
    public Controls controls;
    //��������� ������� UI
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject PauseUI;
    //��������� ���������� ������
    public int LivesNumber;

    //��������� �������� �� ����
    private bool start;

    private void Start()        //��������� ��������� UI
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
        if (start)              //���� �� ������ ���������� ����� ��� ������� ������
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                OnPlayerStart();
        }
    }
    public void OnPlayerStart() //������ ���� �� ���������� ���������
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
        start = false;
        generator.LevelGenerator(LevelIndex);
        player.EnableControls = true;
    }
    public void OnPlayerDie()   //���� ����������� �����
    {
        controls.PlayerAudio.PlayOneShot(controls.LoseAudio, 0.25f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        LoseUI.SetActive(true);
    }
    public void OnPlayerWin()   //�������� ��� ���������� ������
    {
        controls.PlayerAudio.PlayOneShot(controls.WinAudio, 0.75f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        WinUI.SetActive(true);
        if (LevelIndex < 9)
        {
            LevelIndex++;
            if (LevelIndex > 3)
                StartLives++;   //����� ����������� 4 ������ ��� �������������� ��������� ����� �� �������� �����������
        }            
        else
            LevelIndex = 0;
    }
    public void OnPauseEnter()  //���� �����
    {
        player.EnableControls = false;
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    public void OnPauseExit()   //����� �� ���� �����
    {
        PauseUI.SetActive(false);
        GameUI.SetActive(true);
        player.EnableControls = true;
    }
    public void OnPlayerHurt()  //��� ������� ����� ��������� ���������� ������
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
    public int LevelIndex       //����� ������ ������ � PlayerPrefs
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";
    public int StartLives       //���������� ������ ������ � PlayerPrefs
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