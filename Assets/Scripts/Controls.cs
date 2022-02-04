using System.Collections;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //������ �� ��������� �������
    public float MinSwipe;
    public float MaxSwipe;
    //������ ������ ����
    public Player player;

    //���������� ������� ������
    private Vector3 LastPosition;
    private Quaternion LastRotation;

    //��������� ������� �� ������� ����
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
                //������������ �������� ���� ����� ������������ ���
                float Zdot = Vector3.Dot(delta.normalized, Vector3.up);

                if (Zdot > 0.95)
                    �oveUp();

                if (Zdot < -0.95)
                    �oveDown();

                //������������ �������� ���� ����� �������������� ���
                float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                if (Xdot > 0.95)
                    �oveRight();

                if (Xdot < -0.95)
                    �oveLeft();
            }
        }
        //��������� ������� �� ������� ����
        _previousMousePosition = Input.mousePosition;

        //���������� � ����������
        if (Input.GetKeyUp(KeyCode.W))
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Rotate(90.0f, 0.5f));
            }
        //�oveUp();

        if (Input.GetKeyUp(KeyCode.S))
            �oveDown();

        if (Input.GetKeyUp(KeyCode.D))
            �oveRight();

        if (Input.GetKeyUp(KeyCode.A))
            �oveLeft();
    }
    public void �oveUp ()   //������ ������ ��������� ����� �� ��� Z
    {
        SavePlayerPosition();
        Vector3 up = player.GetUpCorner();
        //������� ���� ������������ ��������� ���
        
        player.transform.RotateAround(up, Vector3.right, 90f);
    }
    public void �oveDown()  //������ ������ ��������� ����� �� ��� Z
    {
        SavePlayerPosition();
        Vector3 down = player.GetDownCorner();
        //������� ���� ������������ ��������� ���
        player.transform.RotateAround(down, Vector3.left, 90f);
    }
    public void �oveRight() //������ ������ ��������� ������ �� ��� X
    {
        SavePlayerPosition();
        Vector3 right = player.GetRightCorner();
        //������� ���� ������������ ��������� ���
        player.transform.RotateAround(right, Vector3.back, 90f);
    }
    public void �oveLeft()  //������ ������ ��������� ����� �� ��� X
    {
        SavePlayerPosition();
        Vector3 left = player.GetLeftCorner();
        //������� ���� ������������ ��������� ���
        player.transform.RotateAround(left, Vector3.forward, 90f);
    }
    private void SavePlayerPosition()   //���������� �������� ��������� ������� ������
    {
        LastPosition = player.transform.position;
        LastRotation = player.transform.rotation;
    }
    private void SetPlayerPosition()   //��������� ��������� ������� ������
    {
        player.transform.position = LastPosition;
        player.transform.rotation = LastRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))    //�� ������� ����� ���������� ������������ ������ �� ���������� ������� �������
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
