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

    //�������� ���������
    private float offsetX;
    private float offsetZ;
    //������� Y � ������� �����
    private float Ypos;
    //��� ����� �������� ����
    private float step;

    private void Awake()
    {
        offsetX = (LinesNumber - 1) / 2;    //�������� ��������� ��� ��������� ���������� �����
        offsetZ = (ColumnsNumber - 1) / 2;  //�������� ��������� ��� ��������� ���������� ��������
        Ypos = Wall.transform.position.y;
        step = player.Step;

        //�������� ������ �� ����
        player.AddHedgehogs(StartTransform);
        //�������� ������ ���� ������������ ���, ���������� ����� "������"
        player.RotateHedgehogs(StartTransform, StartRotation);
        //��������� ����� �������� ����
        CreateHoles();
        //��������� ����
        CreateWalls();
    }
    //������ ���� �� �����
    private void CreateHoles()
    {
        for (int i = 0; i < LinesNumber; i++)
            for (int n = 0; n < ColumnsNumber; n++)
                CreateHole(i, n);
    }
    //��������� ������
    private void CreateHole(int line, int column)
    {
        Vector3 position = new Vector3((line - offsetX) * step, Ypos, (column - offsetZ) * step);
        Instantiate(Hole.transform, position, Quaternion.identity, transform);
    }
    //��������� ����
    private void CreateWalls()
    {
        //������� �������
        Vector3 First = new Vector3(-(offsetX + 1) * step, Ypos, (offsetZ + 1) * step);
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        FirstWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        //������ �������
        Vector3 Second = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        SecondWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        //����� �������
        Vector3 Third = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (LinesNumber + 1) * step);
        //������ ������� �������
        Vector3 Fourth = new Vector3((offsetX + 1) * step, Ypos, step / 2);
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3.5f * step);
        //������ ������ �������
        Vector3 Fifth = new Vector3((offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 2.5f * step);
        //������
        Vector3 �rick = new Vector3((offsetX + 1) * step, Ypos / 2, - 1.5f * step);
        Transform �rickWall = CreateWall(�rick);
        �rickWall.localScale = new Vector3(1f, 1f, 2f * step);
    }
    //�������� ������� ����
    private Transform CreateWall(Vector3 pos)
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
}