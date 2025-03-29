using UnityEngine;

public class StarPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            StarsController.StarsCount++;
        }
    }

}
