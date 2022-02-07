using UnityEngine;

public class Generator : MonoBehaviour
{
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
    //��������� ��������� ������
    public int[] StartPlayer_X;
    public int[] StartPlayer_Z;
    public float[] StartRotation;
    //��������� ������� ����� �� �����
    public int[] Enemy1_X;
    public int[] Enemy1_Z;
    public int[] Enemy2_X;
    public int[] Enemy2_Z;
    public int[] Enemy3_X;
    public int[] Enemy3_Z;
    public int[] Enemy4_X;
    public int[] Enemy4_Z;
    //���������
    public string[] Hint;

    //�������� ���������
    private int offsetX;
    private int offsetZ;
    //������� Y ������� �����
    private float Ypos;
    //��� ����� �������� ����
    private float step;

    private void Awake()
    {
        offsetX = (LinesNumber - 1) / 2;            //�������� ��������� (��� ��������� ���������� �����)
        offsetZ = (ColumnsNumber - 1) / 2;          //�������� ��������� (��� ��������� ���������� ��������)
        Ypos = Wall.transform.position.y;
        step = player.Step;
        CreateWalls();                              //��������� ����
        CreateHoles();                              //��������� ����� �������� ����
    }
    public void LevelGenerator(int LevelIndex)
    {
        Vector3 StartPos = GetPos(StartPlayer_X[LevelIndex], StartPlayer_Z[LevelIndex]) + new Vector3(0f, 0.5f, 0f);
        player.AddHedgehogs(StartPos);              //�������� ������ �� ����
        player.RotateHedgehogs(StartPos, StartRotation[LevelIndex]);    //�������� ������ ���� ������������ ���, ���������� ����� "������"
        CreateEnemies(LevelIndex);                  //��������� ������
    }    
    private void CreateHoles()                      //������ ���� �� �������� ����� � �������� �� ������
    {
        for (int i = 1; i <= LinesNumber; i++)
            for (int n = 1; n <= ColumnsNumber; n++)
                CreateObject(Hole, i, n);
        CreateObject(Hole, LinesNumber + 2, offsetZ + 1);
        CreateObject(Hole, LinesNumber + 3, offsetZ + 1);
        CreateObject(Hole, LinesNumber + 2, offsetZ);
        CreateObject(Hole, LinesNumber + 3, offsetZ);
    }    
    private void CreateObject(GameObject Object, int line, int column)  //��������� ������
    {
        Vector3 position = GetPos(line, column);
        Instantiate(Object.transform, position, Quaternion.identity, transform);
    }
    private Vector3 GetPos(int line, int column)    //��������� ������� �� ������ ������ � �������
    {
        Vector3 position = new Vector3((line - offsetX - 1) * step, Ypos, (column - offsetZ - 1) * step);
        return position;
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
        
        Vector3 Fourth = new Vector3((offsetX + 1) * step, Ypos, step / 2 + 0.025f);            //������ ������� �������
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3.475f * step);
        
        Vector3 Fifth = new Vector3((offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);         //������ ������ �������
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 2.475f * step);
        
        Vector3 �rick = new Vector3((offsetX + 1) * step, Ypos / 2, - 1.5f * step - 0.025f);    //������
        Transform �rickWall = CreateWall(�rick);
        �rickWall.localScale = new Vector3(1f, 1f, 2.05f * step);
    }
    private Transform CreateWall(Vector3 pos)       //�������� ������� ����
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
    private void CreateEnemies(int LevelInd)        //��������� ������
    {
        CreateObject(Enemy, Enemy1_X[LevelInd], Enemy1_Z[LevelInd]);
        CreateObject(Enemy, Enemy2_X[LevelInd], Enemy2_Z[LevelInd]);
        CreateObject(Enemy, Enemy3_X[LevelInd], Enemy3_Z[LevelInd]);
        CreateObject(Enemy, Enemy4_X[LevelInd], Enemy4_Z[LevelInd]);
    }
}