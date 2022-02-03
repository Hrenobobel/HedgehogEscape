using UnityEngine;

public class Controls : MonoBehaviour
{
    //Защита от случайных касаний
    public float MinSwipe;
    public float MaxSwipe;
    //Логика группы ежей
    public Player player;

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
            МoveUp();

        if (Input.GetKeyUp(KeyCode.S))
            МoveDown();

        if (Input.GetKeyUp(KeyCode.D))
            МoveRight();

        if (Input.GetKeyUp(KeyCode.A))
            МoveLeft();
    }
    public void МoveUp ()
    {
        Vector3 up = player.GetUpCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(up, Vector3.right, 90f);
    }
    public void МoveDown()
    {
        Vector3 down = player.GetDownCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(down, Vector3.left, 90f);
    }
    public void МoveRight()
    {
        Vector3 right = player.GetRightCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(right, Vector3.back, 90f);
    }
    public void МoveLeft()
    {
        Vector3 left = player.GetLeftCorner();
        //Поворот ежей относительно требуемой оси
        player.transform.RotateAround(left, Vector3.forward, 90f);
    }
}
