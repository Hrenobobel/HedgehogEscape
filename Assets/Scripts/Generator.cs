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

    //смещение координат
    private float offsetX;
    private float offsetZ;
    //позиция Y у объекта стены
    private float Ypos;
    //Шаг сетки игрового поля
    private float step;

    private void Awake()
    {
        offsetX = (LinesNumber - 1) / 2;    //смещение координат для нечетного количества строк
        offsetZ = (ColumnsNumber - 1) / 2;  //смещение координат для нечетного количества столбцов
        Ypos = Wall.transform.position.y;
        step = player.Step;

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
        Vector3 position = new Vector3((line - offsetX) * step, Ypos, (column - offsetZ) * step);
        Instantiate(Hole.transform, position, Quaternion.identity, transform);
    }
    //Генератор стен
    private void CreateWalls()
    {
        //Верхняя граница
        Vector3 First = new Vector3(-(offsetX + 1) * step, Ypos, (offsetZ + 1) * step);
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        FirstWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        //Нижняя граница
        Vector3 Second = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        SecondWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        //Левая граница
        Vector3 Third = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (LinesNumber + 1) * step);
        //Правая верхняя граница
        Vector3 Fourth = new Vector3((offsetX + 1) * step, Ypos, step / 2);
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3.5f * step);
        //Правая нижняя граница
        Vector3 Fifth = new Vector3((offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 2.5f * step);
        //Барьер
        Vector3 Вrick = new Vector3((offsetX + 1) * step, Ypos / 2, - 1.5f * step);
        Transform ВrickWall = CreateWall(Вrick);
        ВrickWall.localScale = new Vector3(1f, 1f, 2f * step);
    }
    //Создание объекта стен
    private Transform CreateWall(Vector3 pos)
    {
        Transform brick = Instantiate(Wall.transform, pos, Quaternion.identity, transform);
        return brick;
    }
}