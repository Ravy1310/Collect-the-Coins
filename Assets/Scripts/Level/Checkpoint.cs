using UnityEngine;

/// <summary>
/// Checkpoint: aktif saat player menyentuh trigger-nya.
/// Menyimpan posisi respawn ke CheckpointManager.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private Renderer _flagRenderer;
    [SerializeField] private Material _activeMaterial;

    public bool IsActivated { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (IsActivated) return;
        Activate();
    }

    private void Activate()
    {
        IsActivated = true;
        CheckpointManager.Instance?.SetCheckpoint(transform.position + Vector3.up * 0.5f);

        if (_flagRenderer != null && _activeMaterial != null)
            _flagRenderer.material = _activeMaterial;

        Debug.Log("Checkpoint activated at " + transform.position);
    }
}
