using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //��� ����� �������� ����
    public float Step;
    //Transform ���
    public GameObject Hedgehog;

    public GameObject Head;
    //
    private List<Vector3> Hedgehogs = new List<Vector3>();

    private void Awake()
    {
        Hedgehogs.Add(Head.transform.position);
    }
}
