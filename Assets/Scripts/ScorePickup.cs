using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
        {
            return;
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(points);
        }

        Destroy(gameObject);
    }
}