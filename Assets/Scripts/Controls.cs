using UnityEngine;

public class Controls : MonoBehaviour
{
    //������ �� ��������� �������
    public float MinSwipe;
    public float MaxSwipe;
    //������ ������ ����
    public Player player;
    //��� ����� �������� ����
    public float Step;

    //��������� ������� �� ������� ����
    private Vector3 _previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            if ((delta.magnitude > MinSwipe) && (delta.magnitude < MaxSwipe))
            {
                //������������ �������� ���� ����� ������������ ���
                float Zdot = Vector3.Dot(delta.normalized, Vector3.up);

                if (Zdot > 0.95)
                {
                    Vector3 up = player.GetUpCorner();
                    //������� ���� ������������ ��������� ���
                    player.transform.RotateAround(up, Vector3.right, 90f);
                }
                if (Zdot < -0.95)
                {
                    Vector3 down = player.GetDownCorner();
                    //������� ���� ������������ ��������� ���
                    player.transform.RotateAround(down, Vector3.left, 90f);
                }

                //������������ �������� ���� ����� �������������� ���
                float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                if (Xdot > 0.95)
                {
                    Vector3 right = player.GetRightCorner();
                    //������� ���� ������������ ��������� ���
                    player.transform.RotateAround(right, Vector3.back, 90f);
                }
                if (Xdot < -0.95)
                {
                    Vector3 left = player.GetLeftCorner();
                    //������� ���� ������������ ��������� ���
                    player.transform.RotateAround(left, Vector3.forward, 90f);
                }
            }
        }
        //��������� ������� �� ������� ����
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
