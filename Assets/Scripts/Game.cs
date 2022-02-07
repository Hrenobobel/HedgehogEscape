using UnityEngine;

public class Game : MonoBehaviour
{
    //������ �������� ��������
    public Player player;
    public Generator generator;
    public Controls controls;
    //��������� ������� UI
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject PauseUI;
    //��������� "������" ������ ����
    public Transform StartTransform;
    public Transform[] StartHengehogsPosition;
    //��������� ������� ������ ����
    public float StartRotation;
    public float[] StartHengehogsRotation;
    //��������� ���������� ������
    public int LivesNumber;

    //��������� �������� �� ����
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
        if (start)              //��������� ���������
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
        generator.LevelGenerator(StartTransform, StartRotation);
        player.EnableControls = true;
    }
    public void OnPlayerDie()   //���� ����������� �����
    {
        controls.PlayerAudio.PlayOneShot(controls.LoseAudio, 0.5f);
        player.EnableControls = false;
        GameUI.SetActive(false);
        LoseUI.SetActive(true);
    }
    public void OnPlayerWin()   //�������� ��� ���������� ������
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
    public void OnPlayerHurt()  //��� ������� ����� ��������� ���������� ������
    {
        if (LivesNumber > 1)
            LivesNumber--;
        else
            OnPlayerDie();
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
}