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
    //Префаб врага
    public GameObject Enemy;

    //смещение координат
    private int offsetX;
    private int offsetZ;
    //позиция Y у объекта стены
    private float Ypos;
    //Шаг сетки игрового поля
    private float step;

    private void Awake()
    {
        offsetX = (LinesNumber - 1) / 2;    //смещение координат (для нечетного количества строк)
        offsetZ = (ColumnsNumber - 1) / 2;  //смещение координат (для нечетного количества столбцов)
        Ypos = Wall.transform.position.y;
        step = player.Step;

        player.AddHedgehogs(StartTransform);                    //Создание группы из ежей
        player.RotateHedgehogs(StartTransform, StartRotation);  //Вращение группы ежей относительно оси, проходящей через "голову"
        CreateHoles();                                          //Генерация ячеек игрового поля
        CreateWalls();                                          //Генерация стен
        CreateEnemies();                                        //Генерация врагов

        player.EnableControls = true;       //Управление включено
    }    
    private void CreateHoles()                      //Создаём поле из префабов ячеек и площадку на выходе
    {
        for (int i = 0; i < LinesNumber; i++)
            for (int n = 0; n < ColumnsNumber; n++)
                CreateObject(Hole, i, n);
        CreateObject(Hole, LinesNumber + 1, offsetZ);
        CreateObject(Hole, LinesNumber + 2, offsetZ);
        CreateObject(Hole, LinesNumber + 1, offsetZ - 1);
        CreateObject(Hole, LinesNumber + 2, offsetZ - 1);
    }    
    private void CreateObject(GameObject Object, int line, int column)   //Генерация ячейки
    {
        Vector3 position = new Vector3((line - offsetX) * step, Ypos, (column - offsetZ) * step);
        Instantiate(Object.transform, position, Quaternion.identity, transform);
    }    
    private void CreateWalls()                      //Генератор стен
    {        
        Vector3 First = new Vector3(-(offsetX + 1) * step, Ypos, (offsetZ + 1) * step);         //Верхняя граница
        Transform FirstWall = CreateWall(First);
        FirstWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        FirstWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        
        Vector3 Second = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);       //Нижняя граница
        Transform SecondWall = CreateWall(Second);
        SecondWall.localScale = new Vector3(1f, 2f, (ColumnsNumber + 1) * step);
        SecondWall.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        
        Vector3 Third = new Vector3(-(offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);        //Левая граница
        Transform ThirdWall = CreateWall(Third);
        ThirdWall.localScale = new Vector3(1f, 2f, (LinesNumber + 1) * step);
        
        Vector3 Fourth = new Vector3((offsetX + 1) * step, Ypos, step / 2 + 0.025f);             //Правая верхняя граница
        Transform FourthWall = CreateWall(Fourth);
        FourthWall.localScale = new Vector3(1f, 2f, 3.475f * step);
        
        Vector3 Fifth = new Vector3((offsetX + 1) * step, Ypos, -(offsetZ + 1) * step);         //Правая нижняя граница
        Transform FifthWall = CreateWall(Fifth);
        FifthWall.localScale = new Vector3(1f, 2f, 2.475f * step);
        
        Vector3 Вrick = new Vector3((offsetX + 1) * step, Ypos / 2, - 1.5f * step - 0.025f);    //Барьер
        Transform ВrickWall = CreateWall(Вrick);
        ВrickWall.localScale = new Vector3(1f, 0.95f, 2.05f * step);
    }
    private Transform CreateWall(Vector3 pos)       //Создание объекта стен
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