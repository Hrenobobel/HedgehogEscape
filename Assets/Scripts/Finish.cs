using UnityEngine;

public class Finish : MonoBehaviour
{
    //������ ������ ����
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //���� ����� "�� ������" ��������� ���������� �� ���������
            player.Finish = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            player.Finish = true;        
    }
    /*IEnumerator RightFinish()
    {
        while (true)
        {
            Debug.Log("finish coroutine");
            if (cntrl.coroutine == null)
            {
                Debug.Log("coroutine end");
                cntrl.RotateRight();
                yield break;                
            }
            yield return null;
        }        
    }*/
}