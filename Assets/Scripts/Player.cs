using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //��� ����� �������� ����
    public float Step;
    //������ ���
    public GameObject Hedgehog;

    public bool Finish;

    //������ �� ������
    private List<Transform> Hedgehogs = new List<Transform>();

    public void AddHedgehogs(Transform FirstTransform)  //�������� "�"-�������� ������ �� ����
    {
        CreateHedgehog(FirstTransform.position);
        Vector3 SecondPosition = new Vector3(FirstTransform.position.x - Step, FirstTransform.position.y, FirstTransform.position.z);
        CreateHedgehog(SecondPosition);
        Vector3 ThirdPosition = new Vector3(SecondPosition.x, SecondPosition.y, SecondPosition.z + Step);
        CreateHedgehog(ThirdPosition);
        Vector3 FourthPosition = new Vector3(ThirdPosition.x, ThirdPosition.y, ThirdPosition.z + Step);
        CreateHedgehog(FourthPosition);
        Vector3 FifthPosition = new Vector3(FourthPosition.x + Step, FourthPosition.y, FourthPosition.z);
        CreateHedgehog(FifthPosition);
        CreateHedgehogs();
    }
    public void RotateHedgehogs(Transform AxisPosition, float Rotation) //������� ������ ���� ������������ ������������ ���
    {
        for (int i = 0; i < Hedgehogs.Count; i++)
            Hedgehogs[i].RotateAround(AxisPosition.position, Vector3.up, Rotation);
    }
    private void CreateHedgehog(Vector3 position)   //�������� ������ ���
    {
        Transform NewUnit = Instantiate(Hedgehog.transform, position, Quaternion.identity, transform);
        Hedgehogs.Add(NewUnit);
    }
    private void CreateHedgehogs()  //�������� "������� �����" �� ����
    {
        int number = Hedgehogs.Count;
        for (int i = 0; i < number; i++)
        {
            Vector3 UpPosition = new Vector3(Hedgehogs[i].position.x, Hedgehogs[i].position.y + Step, Hedgehogs[i].position.z);
            CreateHedgehog(UpPosition);
        }
    }
    public Vector3 GetLeftCorner()  //����� ���� ��� �������� ��� ��������
    {
        Vector3 leftPos = Hedgehogs[0].position;
        for (int i = 1; i < (Hedgehogs.Count); i++)
            if ((leftPos.x - Hedgehogs[i].position.x) > - 0.1f)         //�������� ������� (Hedgehogs[i].position.x <= leftPos.x)
                if ((leftPos.y - Hedgehogs[i].position.y) > - 0.1f)     //�������� ������� (Hedgehogs[i].position.y <= leftPos.y)
                    leftPos = Hedgehogs[i].position;
        return leftPos;
    }
    public Vector3 GetRightCorner() //������ ���� ��� �������� ��� �������
    {
        Vector3 rightPos = Hedgehogs[0].position;
        for (int i = 1; i < (Hedgehogs.Count); i++)
            if ((rightPos.x - Hedgehogs[i].position.x) < 0.1f)          //�������� ������� (Hedgehogs[i].position.x >= rightPos.x)
                if ((rightPos.y - Hedgehogs[i].position.y) > - 0.1f)    //�������� ������� (Hedgehogs[i].position.y <= rightPos.y)
                    rightPos = Hedgehogs[i].position;
        return rightPos;
    }
    public Vector3 GetUpCorner()    //������� ���� ��� �������� ��� ��������
    {
        Vector3 upPos = Hedgehogs[0].position;
        for (int i = 1; i < (Hedgehogs.Count); i++)
            if ((upPos.z - Hedgehogs[i].position.z) < 0.1f)             //�������� ������� (Hedgehogs[i].position.z >= upPos.z)
                if ((upPos.y - Hedgehogs[i].position.y) > - 0.1f)       //�������� ������� (Hedgehogs[i].position.y <= upPos.y)
                    upPos = Hedgehogs[i].position;
        return upPos;
    }
    public Vector3 GetDownCorner()  //������ ���� ��� �������� ��� ��������
    {
        Vector3 downPos = Hedgehogs[0].position;
        for (int i = 1; i < (Hedgehogs.Count); i++)
            if ((downPos.z - Hedgehogs[i].position.z) > -0.1f)         //�������� ������� (Hedgehogs[i].position.z <= downPos.z)
                if ((downPos.y - Hedgehogs[i].position.y) > -0.1f)       //�������� ������� (Hedgehogs[i].position.y <= downPos.y)
                    downPos = Hedgehogs[i].position;
        return downPos;
    }
}
