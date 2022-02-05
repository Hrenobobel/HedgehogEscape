using System.Collections;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //Защита от случайных касаний
    public float MinSwipe;
    public float MaxSwipe;
    //Логика группы ежей
    public Player player;
    //Скорость поворота
    public float MoveSpeed;
    //Управление включено
    public bool EnableControls;

    //Предыдущая позиция игрока
    private Vector3 LastPosition;
    private Quaternion LastRotation;
    //Отработка реакции на нажатие мыши
    private Vector3 _previousMousePosition;
    //Указатель на столкновение со стеной
    private bool WallEnemyTrigger;
    //Корутины плавного поворота
    public Coroutine coroutine;

    void Update()
    {
        if (player.EnableControls)      //Если управление включено
        {
            //Отработка реакции на нажатие мыши
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 delta = Input.mousePosition - _previousMousePosition;
                if ((delta.magnitude > MinSwipe) && (delta.magnitude < MaxSwipe))
                {
                    //Обрабатываем движения мыши вдоль вертикальной оси
                    float Zdot = Vector3.Dot(delta.normalized, Vector3.up);

                    if (Zdot > 0.95)
                        if (coroutine == null)  //Выполняем движение если другого не производится
                            coroutine = StartCoroutine(RotateUp());

                    if (Zdot < -0.95)
                        if (coroutine == null)  //Выполняем движение если другого не производится
                            coroutine = StartCoroutine(RotateDown());

                    //Обрабатываем движения мыши вдоль горизонтальной оси
                    float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                    if (Xdot > 0.95)
                        if (coroutine == null)  //Выполняем движение если другого не производится
                            coroutine = StartCoroutine(RotateRight());

                    if (Xdot < -0.95)
                        if (coroutine == null)  //Выполняем движение если другого не производится
                            coroutine = StartCoroutine(RotateLeft());
                }
            }
            _previousMousePosition = Input.mousePosition;

            //Управление с клавиатуры
            if (Input.GetKeyUp(KeyCode.W))
                if (coroutine == null)  //Выполняем движение если другого не производится
                    coroutine = StartCoroutine(RotateUp());

            if (Input.GetKeyUp(KeyCode.S))
                if (coroutine == null)  //Выполняем движение если другого не производится
                    coroutine = StartCoroutine(RotateDown());

            if (Input.GetKeyUp(KeyCode.D))
                if (coroutine == null)  //Выполняем движение если другого не производится
                    coroutine = StartCoroutine(RotateRight());

            if (Input.GetKeyUp(KeyCode.A))
                if (coroutine == null)  //Выполняем движение если другого не производится
                    coroutine = StartCoroutine(RotateLeft());
        }       
    }
    private void SavePlayerPosition()   //Сохранение текущей позиции объекта игрока
    {
        LastPosition = player.transform.position;
        LastRotation = player.transform.rotation;
        WallEnemyTrigger = false;            //Сбрасываем указатель столкновения со стеной
    }
    private void SetPlayerPosition()    //Установка объекта игрока в передыдущую позицию
    {
        player.transform.position = LastPosition;
        player.transform.rotation = LastRotation;
    }
    private void OnTriggerEnter(Collider other)     
    {
        if (other.gameObject.CompareTag("Wall"))    //при касании стены срабатывает возвращающий игрока на предыдущую позицию триггер
        {
            WallEnemyTrigger = true;         //Выход из функции плавного поворота
            Debug.Log("Wall");
            SetPlayerPosition();        //Возвращем игрока в предыдущее положение
        }
        if (other.gameObject.CompareTag("Enemy"))   //при касании врага срабатывает возвращающий игрока на предыдущую позицию триггер
        {
            WallEnemyTrigger = true;         //Выход из функции плавного поворота
            Debug.Log("Enemy");
            SetPlayerPosition();        //Возвращем игрока в предыдущее положение
        }
    }
    public void МoveUp ()   //Позиция игрока поворачивается на 90 градусов вперёд по оси Z
    {
        Vector3 up = player.GetUpCorner();                          //Берем переднюю нижнюю ось группы ежей для поворота
        player.transform.RotateAround(up, Vector3.right, 90f);      //Поворот ежей вперёд
    }
    public void МoveDown()  //Позиция игрока поворачивается на 90 градусов назад по оси Z
    {
        Vector3 down = player.GetDownCorner();                      //Берем заднюю нижнюю ось группы ежей для поворота
        player.transform.RotateAround(down, Vector3.left, 90f);     //Поворот ежей назад
    }
    public void МoveRight() //Позиция игрока поворачивается на 90 градусов вправо по оси X
    {
        Vector3 right = player.GetRightCorner();                    //Берем правую нижнюю ось группы ежей для поворота
        player.transform.RotateAround(right, Vector3.back, 90f);    //Поворот ежей вправо
    }
    public void МoveLeft()  //Позиция игрока поворачивается на 90 градусов влево по оси X
    {
        Vector3 left = player.GetLeftCorner();                      //Берем левую нижнюю ось группы ежей для поворота
        player.transform.RotateAround(left, Vector3.forward, 90f);  //Поворот ежей влево
    }
    IEnumerator RotateUp()      //Реализация плавного поворота игрока вперёд
    {
        SavePlayerPosition();               //Сохраняем стартовое положение игрока
        Vector3 up = player.GetUpCorner();  //Берем переднюю нижнюю ось группы ежей для поворота
        float rotation = 0f;                //Счетчик для контроля "доворота до 90 градусов"        
        while (true) 
        {
            if (WallEnemyTrigger)    //Если столкнулись со стеной, выходим
            {
                StopCoroutine();
                yield break;
            }
            //Вычисляем угол поворота
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(up, Vector3.right, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //Вращаем игрока пока угол поворота не приблизится к 90 градусам на угол менее шага, равного скорости
            {
                SetPlayerPosition();        //Для установки игрока в точное положение возвращаем его в предыдущее состояние
                МoveUp();                   // и проворачиваем точно на 90 градусов
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator RotateDown()    //Реализация плавного поворота игрока назад
    {
        SavePlayerPosition();                   //Сохраняем стартовое положение игрока
        Vector3 down = player.GetDownCorner();  //Берем заднюю нижнюю ось группы ежей для поворота
        float rotation = 0f;                    //Счетчик для контроля "доворота до 90 градусов"        
        while (true)
        {
            if (WallEnemyTrigger)    //Если столкнулись со стеной, выходим
            {
                StopCoroutine();
                yield break;
            }

            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(down, Vector3.left, angle);
            rotation += angle;
            //Вычисляем угол поворота
            if (rotation > 90f - MoveSpeed) //Вращаем игрока пока угол поворота не приблизится к 90 градусам на угол менее шага, равного скорости
            {
                SetPlayerPosition();        //Для установки игрока в точное положение возвращаем его в предыдущее состояние
                МoveDown();                 // и проворачиваем точно на 90 градусов
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator RotateRight()   //Реализация плавного поворота игрока вправо
    {
        SavePlayerPosition();                       //Сохраняем стартовое положение игрока
        Vector3 right = player.GetRightCorner();    //Берем правую нижнюю ось группы ежей для поворота
        float rotation = 0f;                        //Счетчик для контроля "доворота до 90 градусов"        
        while (true)
        {
            if (WallEnemyTrigger)    //Если столкнулись со стеной, выходим
            {
                StopCoroutine();
                yield break;
            }
            //Вычисляем угол поворота
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(right, Vector3.back, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //Вращаем игрока пока угол поворота не приблизится к 90 градусам на угол менее шага, равного скорости
            {
                SetPlayerPosition();        //Для установки игрока в точное положение возвращаем его в предыдущее состояние
                МoveRight();                // и проворачиваем точно на 90 градусов
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator RotateLeft()    //Реализация плавного поворота игрока влево
    {
        SavePlayerPosition();                   //Сохраняем стартовое положение игрока
        Vector3 left = player.GetLeftCorner();  //Берем левую нижнюю ось группы ежей для поворота
        float rotation = 0f;                    //Счетчик для контроля "доворота до 90 градусов"        
        while (true)
        {
            if (WallEnemyTrigger)    //Если столкнулись со стеной, выходим
            {
                StopCoroutine();
                yield break;   
            }
            //Вычисляем угол поворота
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(left, Vector3.forward, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //Вращаем игрока пока угол поворота не приблизится к 90 градусам на угол менее шага, равного скорости
            {                
                SetPlayerPosition();        //Для установки игрока в точное положение возвращаем его в предыдущее состояние
                МoveLeft();                 // и проворачиваем точно на 90 градусов
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    private void StopCoroutine()
    {
        coroutine = null;
    }
}
