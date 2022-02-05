using UnityEngine;

public class Win : MonoBehaviour
{
    //Логика группы ежей
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //Игрок достиг финиша
        {
            Debug.Log("win");
            player.EnableControls = false;
        }
            
    }
}
