using System.Collections;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //������ �� ��������� �������
    public float MinSwipe;
    public float MaxSwipe;
    //������ ������ ����
    public Player player;
    //�������� ��������
    public float MoveSpeed;
    //���������� ��������
    public bool EnableControls;

    //���������� ������� ������
    private Vector3 LastPosition;
    private Quaternion LastRotation;
    //��������� ������� �� ������� ����
    private Vector3 _previousMousePosition;
    //��������� �� ������������ �� ������
    private bool WallEnemyTrigger;
    //�������� �������� ��������
    public Coroutine coroutine;

    void Update()
    {
        if (player.EnableControls)      //���� ���������� ��������
        {
            //��������� ������� �� ������� ����
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 delta = Input.mousePosition - _previousMousePosition;
                if ((delta.magnitude > MinSwipe) && (delta.magnitude < MaxSwipe))
                {
                    //������������ �������� ���� ����� ������������ ���
                    float Zdot = Vector3.Dot(delta.normalized, Vector3.up);

                    if (Zdot > 0.95)
                        if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                            coroutine = StartCoroutine(RotateUp());

                    if (Zdot < -0.95)
                        if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                            coroutine = StartCoroutine(RotateDown());

                    //������������ �������� ���� ����� �������������� ���
                    float Xdot = Vector3.Dot(delta.normalized, Vector3.right);

                    if (Xdot > 0.95)
                        if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                            coroutine = StartCoroutine(RotateRight());

                    if (Xdot < -0.95)
                        if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                            coroutine = StartCoroutine(RotateLeft());
                }
            }
            _previousMousePosition = Input.mousePosition;

            //���������� � ����������
            if (Input.GetKeyUp(KeyCode.W))
                if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                    coroutine = StartCoroutine(RotateUp());

            if (Input.GetKeyUp(KeyCode.S))
                if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                    coroutine = StartCoroutine(RotateDown());

            if (Input.GetKeyUp(KeyCode.D))
                if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                    coroutine = StartCoroutine(RotateRight());

            if (Input.GetKeyUp(KeyCode.A))
                if (coroutine == null)  //��������� �������� ���� ������� �� ������������
                    coroutine = StartCoroutine(RotateLeft());
        }       
    }
    private void SavePlayerPosition()   //���������� ������� ������� ������� ������
    {
        LastPosition = player.transform.position;
        LastRotation = player.transform.rotation;
        WallEnemyTrigger = false;            //���������� ��������� ������������ �� ������
    }
    private void SetPlayerPosition()    //��������� ������� ������ � ����������� �������
    {
        player.transform.position = LastPosition;
        player.transform.rotation = LastRotation;
    }
    private void OnTriggerEnter(Collider other)     
    {
        if (other.gameObject.CompareTag("Wall"))    //��� ������� ����� ����������� ������������ ������ �� ���������� ������� �������
        {
            WallEnemyTrigger = true;         //����� �� ������� �������� ��������
            Debug.Log("Wall");
            SetPlayerPosition();        //��������� ������ � ���������� ���������
        }
        if (other.gameObject.CompareTag("Enemy"))   //��� ������� ����� ����������� ������������ ������ �� ���������� ������� �������
        {
            WallEnemyTrigger = true;         //����� �� ������� �������� ��������
            Debug.Log("Enemy");
            SetPlayerPosition();        //��������� ������ � ���������� ���������
        }
    }
    public void �oveUp ()   //������� ������ �������������� �� 90 �������� ����� �� ��� Z
    {
        Vector3 up = player.GetUpCorner();                          //����� �������� ������ ��� ������ ���� ��� ��������
        player.transform.RotateAround(up, Vector3.right, 90f);      //������� ���� �����
    }
    public void �oveDown()  //������� ������ �������������� �� 90 �������� ����� �� ��� Z
    {
        Vector3 down = player.GetDownCorner();                      //����� ������ ������ ��� ������ ���� ��� ��������
        player.transform.RotateAround(down, Vector3.left, 90f);     //������� ���� �����
    }
    public void �oveRight() //������� ������ �������������� �� 90 �������� ������ �� ��� X
    {
        Vector3 right = player.GetRightCorner();                    //����� ������ ������ ��� ������ ���� ��� ��������
        player.transform.RotateAround(right, Vector3.back, 90f);    //������� ���� ������
    }
    public void �oveLeft()  //������� ������ �������������� �� 90 �������� ����� �� ��� X
    {
        Vector3 left = player.GetLeftCorner();                      //����� ����� ������ ��� ������ ���� ��� ��������
        player.transform.RotateAround(left, Vector3.forward, 90f);  //������� ���� �����
    }
    IEnumerator RotateUp()      //���������� �������� �������� ������ �����
    {
        SavePlayerPosition();               //��������� ��������� ��������� ������
        Vector3 up = player.GetUpCorner();  //����� �������� ������ ��� ������ ���� ��� ��������
        float rotation = 0f;                //������� ��� �������� "�������� �� 90 ��������"        
        while (true) 
        {
            if (WallEnemyTrigger)    //���� ����������� �� ������, �������
            {
                StopCoroutine();
                yield break;
            }
            //��������� ���� ��������
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(up, Vector3.right, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //������� ������ ���� ���� �������� �� ����������� � 90 �������� �� ���� ����� ����, ������� ��������
            {
                SetPlayerPosition();        //��� ��������� ������ � ������ ��������� ���������� ��� � ���������� ���������
                �oveUp();                   // � ������������� ����� �� 90 ��������
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator RotateDown()    //���������� �������� �������� ������ �����
    {
        SavePlayerPosition();                   //��������� ��������� ��������� ������
        Vector3 down = player.GetDownCorner();  //����� ������ ������ ��� ������ ���� ��� ��������
        float rotation = 0f;                    //������� ��� �������� "�������� �� 90 ��������"        
        while (true)
        {
            if (WallEnemyTrigger)    //���� ����������� �� ������, �������
            {
                StopCoroutine();
                yield break;
            }

            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(down, Vector3.left, angle);
            rotation += angle;
            //��������� ���� ��������
            if (rotation > 90f - MoveSpeed) //������� ������ ���� ���� �������� �� ����������� � 90 �������� �� ���� ����� ����, ������� ��������
            {
                SetPlayerPosition();        //��� ��������� ������ � ������ ��������� ���������� ��� � ���������� ���������
                �oveDown();                 // � ������������� ����� �� 90 ��������
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    public IEnumerator RotateRight()   //���������� �������� �������� ������ ������
    {
        SavePlayerPosition();                       //��������� ��������� ��������� ������
        Vector3 right = player.GetRightCorner();    //����� ������ ������ ��� ������ ���� ��� ��������
        float rotation = 0f;                        //������� ��� �������� "�������� �� 90 ��������"        
        while (true)
        {
            if (WallEnemyTrigger)    //���� ����������� �� ������, �������
            {
                StopCoroutine();
                yield break;
            }
            //��������� ���� ��������
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(right, Vector3.back, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //������� ������ ���� ���� �������� �� ����������� � 90 �������� �� ���� ����� ����, ������� ��������
            {
                SetPlayerPosition();        //��� ��������� ������ � ������ ��������� ���������� ��� � ���������� ���������
                �oveRight();                // � ������������� ����� �� 90 ��������
                StopCoroutine();
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator RotateLeft()    //���������� �������� �������� ������ �����
    {
        SavePlayerPosition();                   //��������� ��������� ��������� ������
        Vector3 left = player.GetLeftCorner();  //����� ����� ������ ��� ������ ���� ��� ��������
        float rotation = 0f;                    //������� ��� �������� "�������� �� 90 ��������"        
        while (true)
        {
            if (WallEnemyTrigger)    //���� ����������� �� ������, �������
            {
                StopCoroutine();
                yield break;   
            }
            //��������� ���� ��������
            float angle = Mathf.LerpAngle(0f, 90f, MoveSpeed * Time.deltaTime);
            player.transform.RotateAround(left, Vector3.forward, angle);
            rotation += angle;

            if (rotation > 90f - MoveSpeed) //������� ������ ���� ���� �������� �� ����������� � 90 �������� �� ���� ����� ����, ������� ��������
            {                
                SetPlayerPosition();        //��� ��������� ������ � ������ ��������� ���������� ��� � ���������� ���������
                �oveLeft();                 // � ������������� ����� �� 90 ��������
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
