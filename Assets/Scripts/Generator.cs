using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //Положение "головы" группы ежей
    public Transform StartTransform;
    //Начальный поворот группы ежей
    public float StartRotation;
    //Логика группы ежей
    public Player player;
    //Количество ячеек на игровом поле по горизонтали
    public int LinesNumber;
    //Количество ячеек на игровом поле по горизонтали
    public int ColumnsNumber;
    //Объект ячейки игрового поля
    public GameObject Hole;
    //Объект стены
    public GameObject Wall;

    private void Awake()
    {
        //Создание группы из ежей
        player.AddHedgehogs(StartTransform);
        //Вращение группы ежей относительно оси, проходящей через "голову"
        player.RotateHedgehogs(StartTransform, StartRotation);
        //Генерация ячеек игрового поля
        CreateHoles();
        //Генерация стен
        CreateWalls();
    }
    //Создаём поле из ячеек
    private void CreateHoles()
    {
        for (int i = 0; i < LinesNumber; i++)
            for (int n = 0; n < ColumnsNumber; n++)
                CreateHole(i, n);
    }
    //Генерация ячейки
    private void CreateHole(int line, int column)
    {
        Vector3 position = new Vector3((line - 3) * player.Step, Hole.transform.position.y, (column - 3) * player.Step);
        Instantiate(Hole.transform, position, Quaternion.identity, transform);
    }
    //Генератор стен
    private void CreateWalls()
    {
        //Верхняя граница
        Vector3 First = new Vector3(0f + (player.Step / 2), Wall.transform.position.y, (LinesNumber - 3) * player.Step);
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3((ColumnsNumber + 1) * player.Step, 2f, 1f);
        //Нижняя граница
        Vector3 Second = new Vector3(0f - (player.Step / 2), Wall.transform.position.y, -(LinesNumber - 3) * player.Step);
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3((ColumnsNumber + 1) * player.Step, 2f, 1f);
        //Левая граница
        Vector3 Third = new Vector3(-(ColumnsNumber - 3) * player.Step, Wall.transform.position.y, 0f + (player.Step / 2));
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * player.Step);
        //Правая верхняя граница
        Vector3 Fourth = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y, 2f * player.Step);
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3f * player.Step);
        //Правая нижняя граница
        Vector3 Fifth = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y, -3f * player.Step);
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 3f * player.Step);
        //Барьер
        Vector3 Вrick = new Vector3((ColumnsNumber - 3) * player.Step, Wall.transform.position.y / 2, 0f - (player.Step / 2));
        Transform ВrickWall = CreateWall(Вrick);
        ВrickWall.localScale = new Vector3(1f, 1f, 2f * player.Step);
    }
    private Transform CreateWall(Vector3 pos)
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
}