using UnityEngine;

/// <summary>
/// Legacy Spike script — sekarang mendelegasikan ke CheckpointManager.
/// Digantikan oleh Hazard.cs untuk obstacle baru.
/// </summary>
public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckpointManager.Instance?.Respawn();
    }
}
