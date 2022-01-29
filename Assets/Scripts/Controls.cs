using UnityEngine;

public class Controls : MonoBehaviour
{
    //чувствительность управления по каждой оси
    public float SensivityX;
    public float SensivityZ;
    //защита от случайных касаний
    public float MinSwipe;
    public float MaxSwipe;
    //Transform десятерых ежат
    public GameObject Player;
    //Шаг сетки игрового поля
    public float Step;

    public GameObject DownForwardElement;
    public GameObject DownBackElement;
    public GameObject DownRightElement;
    public GameObject DownRLeftElement;

    private Vector3 _previousMousePosition;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            if ((delta.magnitude > MinSwipe) && (delta.magnitude < MaxSwipe))
            {
                float Zdot = Vector3.Dot(delta.normalized, Vector3.up);
                //обрабатываем движения только вдоль вертикальной оси
                if (Zdot > 0.95)
                {
                    Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + Step);
                    Player.transform.Rotate(90f, 0f, 0f);
                }
                if (Zdot < -0.95)
                {
                    Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - Step);
                    Player.transform.Rotate(-90f, 0f, 0f);
                }

                float Xdot = Vector3.Dot(delta.normalized, Vector3.right);
                //обрабатываем движения только вдоль горизонтальной оси
                if (Xdot > 0.95)
                {
                    Player.transform.position = new Vector3(Player.transform.position.x + Step, Player.transform.position.y, Player.transform.position.z);
                    Player.transform.Rotate(0f, 0f, 90f);
                }
                if (Xdot < -0.95)
                {
                    Player.transform.position = new Vector3(Player.transform.position.x - Step, Player.transform.position.y, Player.transform.position.z);
                    Player.transform.Rotate(0f, 0f, -90f);
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            Player.transform.RotateAround(DownForwardElement.transform.position, Vector3.right, 90f);
            Debug.Log("W");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Player.transform.RotateAround(DownBackElement.transform.position, Vector3.left, 90f);
            Debug.Log("S");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Player.transform.RotateAround(DownRightElement.transform.position, Vector3.back, 90f);
            Debug.Log("D");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Player.transform.RotateAround(DownRLeftElement.transform.position, Vector3.forward, 90f);
            Debug.Log("A");
        }
        _previousMousePosition = Input.mousePosition;
    }
}
