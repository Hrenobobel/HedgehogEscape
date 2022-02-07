using UnityEngine;

public class Enemy : MonoBehaviour
{
    //��������� �������� ������
    public float Amplitude = 0.5f;
    //������� �������� ������
    public float MinFrequency = 1;
    public float MaxFrequency = 3;
    //������ ��� ������� Sin
    private float offset = 0;
    private float t = 0;
    private float frequency;
    private float phase;
    private Vector3 startPosition;

    void Start()    //��� �������� ����� ������ ��� ��������� ���� ��������� �����-����
    {
        startPosition = transform.position;
        phase = Random.Range(0f, 1f);
        frequency = Random.Range(MinFrequency, MaxFrequency);
    }
    void Update()   //������ �������� ��������� �����-���� �����
    {
        t += Time.deltaTime;
        offset = Amplitude * Mathf.Sin(t * frequency + phase);
        transform.position = startPosition + new Vector3(0f, offset, 0f);
        transform.Rotate(new Vector3(0f, 0.1f, 0f));
    }
}