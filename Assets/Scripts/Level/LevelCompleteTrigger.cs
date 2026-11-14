using UnityEngine;

/// <summary>
/// Mendeteksi pemain memasuki area gerbang yang sudah terbuka
/// dan memberi tahu GameManager untuk menampilkan Level Completed.
/// FR-12
/// </summary>
public class LevelCompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Gunakan FindObjectOfType rather than static Instance
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.CompleteLevel();
        }
        else
        {
            Debug.LogWarning("GameManager not found in scene when completing level!");
        }
    }
}