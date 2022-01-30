using UnityEngine;

public class Controls : MonoBehaviour
{
    //Защита от случайных касаний
    public float MinSwipe;
    public float MaxSwipe;
    //Логика группы ежей
    public Player player;
    //Шаг сетки игрового поля
    public float Step;

    //Отработка реакции на нажатие мыши
    private Vector3 _previousMousePosition;

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
                {
                    Vector3 up = player.GetUpCorner();
                    //Поворот ежей относительно требуемой оси
                    player.transform.RotateAround(up, Vector3.right, 90f);
                }
                if (Zdot < -0.95)
                {
                    Vector3 down = player.GetDownCorner();
                    //Поворот ежей относительно требуемой оси
                    player.transform.RotateAround(down, Vector3.left, 90f);
                }

                //Обрабатываем движения мыши вдоль горизонтальной оси
                float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                if (Xdot > 0.95)
                {
                    Vector3 right = player.GetRightCorner();
                    //Поворот ежей относительно требуемой оси
                    player.transform.RotateAround(right, Vector3.back, 90f);
                }
                if (Xdot < -0.95)
                {
                    Vector3 left = player.GetLeftCorner();
                    //Поворот ежей относительно требуемой оси
                    player.transform.RotateAround(left, Vector3.forward, 90f);
                }
            }
        }
        //Отработка реакции на нажатие мыши
        _previousMousePosition = Input.mousePosition;

        if (Input.GetKeyUp(KeyCode.W))
        {
            Vector3 up = player.GetUpCorner();
            player.transform.RotateAround(up, Vector3.right, 90f);            
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Vector3 down = player.GetDownCorner();
            player.transform.RotateAround(down, Vector3.left, 90f);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Vector3 right = player.GetRightCorner();
            player.transform.RotateAround(right, Vector3.back, 90f);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Vector3 left = player.GetLeftCorner();
            player.transform.RotateAround(left, Vector3.forward, 90f);
        }
    }
}
