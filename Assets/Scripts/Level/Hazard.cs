using UnityEngine;

/// <summary>
/// Rintangan / ranjau: saat player menyentuh trigger, respawn ke checkpoint.
/// Pasang komponen ini + BoxCollider (isTrigger=true) pada objek rintangan.
/// </summary>
public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckpointManager.Instance?.Respawn();
    }

    // Juga handle OnControllerColliderHit untuk CharacterController
    // yang kadang tidak trigger OnTriggerEnter
    private void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        CheckpointManager.Instance?.Respawn();
    }
}
