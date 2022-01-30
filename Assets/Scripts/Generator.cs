using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //��� ����� �������� ����
    public float Step;
    //��������� "������" ������ ����
    public Transform StartTransform;
    //��������� ������� ������ ����
    public float StartRotation;
    //������ ������ ����
    public Player player;

    private void Awake()
    {
        //�������� ������ �� ����
        player.AddHedgehogs(StartTransform);
        //�������� ������ ���� ������������ ���, ���������� ����� "������"
        player.RotateHedgehogs(StartTransform, StartRotation);
    }

}