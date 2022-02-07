using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Амплитуда движения врагов
    public float Amplitude = 0.5f;
    //Частота движений врагов
    public float MinFrequency = 1;
    public float MaxFrequency = 3;
    //Данные для функции Sin
    private float offset = 0;
    private float t = 0;
    private float frequency;
    private float phase;
    private Vector3 startPosition;

    void Start()    //При создании врага задаем ему случайную фазу колебаний вверх-вниз
    {
        startPosition = transform.position;
        phase = Random.Range(0f, 1f);
        frequency = Random.Range(MinFrequency, MaxFrequency);
    }
    void Update()   //Задаем анимацию колебаний вверх-вниз врага
    {
        t += Time.deltaTime;
        offset = Amplitude * Mathf.Sin(t * frequency + phase);
        transform.position = startPosition + new Vector3(0f, offset, 0f);
        transform.Rotate(new Vector3(0f, 0.1f, 0f));
    }
}