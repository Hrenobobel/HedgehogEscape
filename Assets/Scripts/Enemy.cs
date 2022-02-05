using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Амплитуда движения врагов
    public float Amplitude = 0.5f;
    //Частота движений врагов
    public float MinFrequency = 1;
    public float MaxFrequency = 3;
    //Данные для функции Sin
    private float Offset = 0;
    private float t = 0;
    private float frequency;
    private float phase;
    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
        phase = Random.Range(0f, 1f);
        frequency = Random.Range(MinFrequency, MaxFrequency);
    }
    void Update()
    {
        t += Time.deltaTime;
        Offset = Amplitude * Mathf.Sin(t * frequency + phase);

        transform.position = StartPosition + new Vector3(0f, Offset, 0f);
        transform.Rotate(new Vector3(0f, 0.1f, 0f));
    }
}
