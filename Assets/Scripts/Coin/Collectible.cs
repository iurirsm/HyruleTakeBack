using UnityEngine;

public class Collectible : MonoBehaviour
{
    public ItemData data;  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Collected {data.itemName} (+{data.value})");
            GameManager.I?.AddCoins(data.value);
            Destroy(gameObject);
        }
    }
}
