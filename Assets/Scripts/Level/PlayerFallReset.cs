using UnityEngine;

public class PlayerFallReset : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [Tooltip("Array of checkpoints in the level, ordered by position")]
    public Checkpoint[] checkpoints;

    [Tooltip("Original spawn point if no checkpoint is activated")]
    public Transform originalSpawnPoint;

    [Header("Respawn Settings")]
    [Tooltip("Y threshold below which player respawns if no checkpoint hit")]
    public float fallThreshold = -1.2f;

    // Cached references
    private int lastCheckpointIndex = -1;

    private void Awake()
    {
        // Ensure original spawn is set if not assigned
        if (originalSpawnPoint == null)
        {
            originalSpawnPoint = transform;
        }
    }

    private void Start()
    {
        // Try to find Checkpoint references if not assigned
        if (checkpoints == null || checkpoints.Length == 0)
        {
            // Find all Checkpoint objects in the scene
            checkpoints = FindObjectsOfType<Checkpoint>();
        }

        // Sort checkpoints by Y position (lowest first = first checkpoint)
        if (checkpoints != null)
        {
            System.Array.Sort(checkpoints, (a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
        }
    }

    private void Update()
    {
        // Check if player fell below threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    // Call this when a checkpoint is activated
    public void SetLastCheckpoint(Vector3 checkpointPos)
    {
        if (checkpoints != null)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].IsActivated)
                {
                    lastCheckpointIndex = i;
                    return;
                }
            }
        }
        lastCheckpointIndex = -1;
    }

    public void Respawn()
    {
        Transform spawn = null;

        if (lastCheckpointIndex >= 0 && lastCheckpointIndex < checkpoints.Length && checkpoints[lastCheckpointIndex].IsActivated)
        {
            spawn = checkpoints[lastCheckpointIndex].transform;
        }
        else if (originalSpawnPoint != null)
        {
            spawn = originalSpawnPoint;
        }

        if (spawn != null)
        {
            // Reset position
            transform.position = spawn.position + Vector3.up * 0.5f; // Slightly above spawn

            // Reset rotation
            transform.rotation = Quaternion.identity;

            // Reset CharacterController - enable/disable to restart
            var cc = GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                cc.enabled = true;
            }
        }
        else
        {
            // Ultimate fallback: reset to original spawn
            transform.position = originalSpawnPoint.position;
        }
    }
}