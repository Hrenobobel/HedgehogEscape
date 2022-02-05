using UnityEngine;

public class Finish : MonoBehaviour
{
    //Логика группы ежей
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  //Если игрок "на выходе" отключаем управление по вертикали
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