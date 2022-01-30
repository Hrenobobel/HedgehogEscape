using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //Шаг сетки игрового поля
    public float Step;
    //Положение "головы" группы ежей
    public Transform StartTransform;
    //Начальный поворот группы ежей
    public float StartRotation;
    //Логика группы ежей
    public Player player;

    private void Awake()
    {
        //Создание группы из ежей
        player.AddHedgehogs(StartTransform);
        //Вращение группы ежей относительно оси, проходящей через "голову"
        player.RotateHedgehogs(StartTransform, StartRotation);
    }

}