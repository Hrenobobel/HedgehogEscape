using System.Collections.Generic;
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

    private void Awake()
    {
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
        Vector3 position = new Vector3((line - 3) * player.Step, Hole.transform.position.y, (column - 3) * player.Step);
        Instantiate(Hole.transform, position, Quaternion.identity, transform);
    }
    //��������� ����
    private void CreateWalls()
    {
        //������� �������
        Vector3 First = new Vector3(0f + (player.Step / 2), Wall.transform.position.y, (LinesNumber - 3) * player.Step);
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3((ColumnsNumber + 1) * player.Step, 2f, 1f);
        //������ �������
        Vector3 Second = new Vector3(0f - (player.Step / 2), Wall.transform.position.y, -(LinesNumber - 3) * player.Step);
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3((ColumnsNumber + 1) * player.Step, 2f, 1f);
        //����� �������
        Vector3 Third = new Vector3(-(ColumnsNumber - 3) * player.Step, Wall.transform.position.y, 0f + (player.Step / 2));
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * player.Step);
        //������ ������� �������
        Vector3 Fourth = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y, 2f * player.Step);
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3f * player.Step);
        //������ ������ �������
        Vector3 Fifth = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y, -3f * player.Step);
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 3f * player.Step);
        //������
        Vector3 �rick = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y / 2, 0f - (player.Step / 2));
        Transform �rickWall = CreateWall(�rick);
        �rickWall.localScale = new Vector3(1f, 1f, 2f * player.Step);
    }
    private Transform CreateWall(Vector3 pos)
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
}