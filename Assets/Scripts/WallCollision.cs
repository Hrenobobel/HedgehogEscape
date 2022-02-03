using UnityEngine;

public class WallCollision : MonoBehaviour
{
    //������ ������ ����
    public Player player;

    public Controls Ctrl;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Wall trigger");
        if (other.CompareTag("Player"))
        {
            if (player.LastMove == "Left")
                Ctrl.�oveRight();
        }
    }
}
