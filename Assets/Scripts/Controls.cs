using System.Collections;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //Защита от случайных касаний
    public float MinSwipe;
    public float MaxSwipe;
    //Логика группы ежей
    public Player player;

    //Предыдущая позиция игрока
    private Vector3 LastPosition;
    private Quaternion LastRotation;

    //Отработка реакции на нажатие мыши
    private Vector3 _previousMousePosition;

    Coroutine coroutine;
    private Transform end;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            if ((delta.magnitude > MinSwipe) && (delta.magnitude < MaxSwipe))
            {
                //Обрабатываем движения мыши вдоль вертикальной оси
                float Zdot = Vector3.Dot(delta.normalized, Vector3.up);

                if (Zdot > 0.95)
                    МoveUp();

                if (Zdot < -0.95)
                    МoveDown();

                //Обрабатываем движения мыши вдоль горизонтальной оси
                float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                if (Xdot > 0.95)
                    МoveRight();

                if (Xdot < -0.95)
                    МoveLeft();
            }
        }
        //Отработка реакции на нажатие мыши
        _previousMousePosition = Input.mousePosition;

        //Управление с клавиатуры
        if (Input.GetKeyUp(KeyCode.W))
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Rotate(90.0f, 0.5f));
            }
        //МoveUp();

        if (Input.GetKeyUp(KeyCode.S))
            МoveDown();

        if (Input.GetKeyUp(KeyCode.D))
            МoveRight();

        if (Input.GetKeyUp(KeyCode.A))
            МoveLeft();
    }
    public void МoveUp ()   //Объект игрока двигается вперёд по оси Z
    {
        SavePlayerPosition();
        Vector3 up = player.GetUpCorner();
        //Поворот ежей относительно требуемой оси
        
        player.transform.RotateAround(up, Vector3.right, 90f);
    }
    public void МoveDown()  //Объект игрока двигается назад по оси Z
    {
        SavePlayerPosition();
        Vector3 down = player.GetDownCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(down, Vector3.left, 90f);
    }
    public void МoveRight() //Объект игрока двигается вправо по оси X
    {
        SavePlayerPosition();
        Vector3 right = player.GetRightCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(right, Vector3.back, 90f);
    }
    public void МoveLeft()  //Объект игрока двигается влево по оси X
    {
        SavePlayerPosition();
        Vector3 left = player.GetLeftCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(left, Vector3.forward, 90f);
    }
    private void SavePlayerPosition()   //Сохранение текущего положения объекта игрока
    {
        LastPosition = player.transform.position;
        LastRotation = player.transform.rotation;
    }
    private void SetPlayerPosition()   //Установка положения объекта игрока
    {
        player.transform.position = LastPosition;
        player.transform.rotation = LastRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))    //На объекте стены установлен возвращающий игрока на предыдущую позицию триггер
        {
            SetPlayerPosition();
        }
    }

    IEnumerator Rotate(float angle, float intensity)
    {
        end = player.transform;
        Vector3 up = player.GetUpCorner();
        end.RotateAround(up, Vector3.right, angle);

        while (true)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, end.rotation, intensity * Time.deltaTime);

            if (Quaternion.Angle(player.transform.rotation, end.rotation) < 0.01f)
            {
                coroutine = null;
                player.transform.rotation = end.rotation;
                yield break;
            }

            yield return null;
        }
    }
}
