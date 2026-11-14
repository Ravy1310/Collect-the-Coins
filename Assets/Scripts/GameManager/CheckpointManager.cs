using UnityEngine;

/// <summary>
/// Menyimpan posisi respawn terakhir dan menangani respawn player.
/// Juga mendeteksi jatuh ke air (Y < threshold).
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [Header("Spawn")]
    [SerializeField] private Vector3 _defaultSpawn = new Vector3(5f, 3.5f, 2f);

    [Header("Fall Detection")]
    [SerializeField] private float _fallYThreshold = -1f;

    private Vector3 _currentRespawn;
    private Transform _player;
    private CharacterController _cc;
    private bool _isRespawning;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        _currentRespawn = _defaultSpawn;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    private void Start()
    {
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            _player = playerGO.transform;
            _cc = playerGO.GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (_player == null || _isRespawning) return;

        if (_player.position.y < _fallYThreshold)
            Respawn();
    }

    public void SetCheckpoint(Vector3 pos)
    {
        _currentRespawn = pos;
        Debug.Log("Checkpoint saved: " + pos);
    }

    public void Respawn()
    {
        if (_isRespawning || _player == null) return;
        _isRespawning = true;

        // Nonaktifkan CharacterController saat teleport
        if (_cc != null) _cc.enabled = false;
        _player.position = _currentRespawn;
        if (_cc != null) _cc.enabled = true;

        _isRespawning = false;
        Debug.Log("Player respawned at " + _currentRespawn);
    }
}
