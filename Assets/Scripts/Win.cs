using UnityEngine;

public class Win : MonoBehaviour
{
    //������ ������ ����
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //����� ������ ������
        {
            Debug.Log("win");
            player.EnableControls = false;
        }
            
    }
}
