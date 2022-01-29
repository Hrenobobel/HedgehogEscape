using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //Шаг сетки игрового поля
    public float Step;
    //Transform ежа
    public GameObject Hedgehog;

    public GameObject Head;
    //
    private List<Vector3> Hedgehogs = new List<Vector3>();

    private void Awake()
    {
        Hedgehogs.Add(Head.transform.position);
    }
}
