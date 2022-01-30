using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Wall trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("back");
        }
    }
}
