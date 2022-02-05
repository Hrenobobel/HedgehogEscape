using UnityEngine;

public class Generator : MonoBehaviour
{
    //��������� "������" ������ ����
    public Transform StartTransform;
    //��������� ������� ������ ����
    public float StartRotation;
    //������ ������ ����
    public Player player;
    //���������� ����� �� ������� ���� �� �����������
    public int LinesNumber;
    //���������� ����� �� ������� ���� �� �����������
    public int ColumnsNumber;
    //������ ������ �������� ����
    public GameObject Hole;
    //������ �����
    public GameObject Wall;
    //������ �����
    public GameObject Enemy;

    //�������� ���������
    private int offsetX;
    private int offsetZ;
    //������� Y � ������� �����
    private float Ypos;
    //��� ����� �������� ����
    private float step;

    private void Awake()
    {
        offsetX = (LinesNumber - 1) / 2;    //�������� ��������� (��� ��������� ���������� �����)
        offsetZ = (ColumnsNumber - 1) / 2;  //�������� ��������� (��� ��������� ���������� ��������)
        Ypos = Wall.transform.position.y;
        step = player.Step;

        player.AddHedgehogs(StartTransform);                    //�������� ������ �� ����
        player.RotateHedgehogs(StartTransform, StartRotation);  //�������� ������ ���� ������������ ���, ���������� ����� "������"
        CreateHoles();                                          //��������� ����� �������� ����
        CreateWalls();                                          //��������� ����
        CreateEnemies();                                        //��������� ������

        player.EnableControls = true;       //���������� ��������
    }    
    private void CreateHoles()                      //������ ���� �� �������� ����� � �������� �� ������
    {
        for (int i = 0; i < LinesNumber; i++)
            for (int n = 0; n < ColumnsNumber; n++)
                CreateObject(Hole, i, n);
        CreateObject(Hole, LinesNumber + 1, offsetZ);
        CreateObject(Hole, LinesNumber + 2, offsetZ);
        CreateObject(Hole, LinesNumber + 1, offsetZ - 1);
        CreateObject(Hole, LinesNumber + 2, offsetZ - 1);
    }    
    private void CreateObject(GameObject Object, int line, int column)   //��������� ������
    {
        Vector3 position = new Vector3((line - offsetX) * step, Ypos, (column - offsetZ) * step);
        Instantiate(Object.transform, position, Quaternion.identity, transform);
    }    
    private void CreateWalls()                      //��������� ����
    {        
        Vector3 First = new Vector3(-(offsetX + 1) * step, Ypos, (offsetZ + 1) * step);         //������� �������
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        FirstWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        
        Vector3 Second = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);       //������ �������
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        SecondWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        
        Vector3 Third = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);        //����� �������
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (LinesNumber + 1) * step);
        
        Vector3 Fourth = new Vector3((offsetX + 1) * step, Ypos, step / 2 + 0.025f);             //������ ������� �������
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3.475f * step);
        
        Vector3 Fifth = new Vector3((offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);         //������ ������ �������
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 2.475f * step);
        
        Vector3 �rick = new Vector3((offsetX + 1) * step, Ypos / 2, - 1.5f * step - 0.025f);    //������
        Transform �rickWall = CreateWall(�rick);
        �rickWall.localScale = new Vector3(1f, 0.95f, 2.05f * step);
    }
    private Transform CreateWall(Vector3 pos)       //�������� ������� ����
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
    private void CreateEnemies()
    {
        CreateObject(Enemy, 1, 2);
        CreateObject(Enemy, 4, 2);
        CreateObject(Enemy, 2, 4);
        CreateObject(Enemy, 4, 5);
    }
}