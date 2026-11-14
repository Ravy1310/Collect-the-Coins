using UnityEngine;

/// <summary>
/// Mengontrol status terkunci/terbuka gerbang berdasarkan event CoinCounter.
/// Saat terkunci: GateBarrier aktif (menghalangi pemain).
/// Saat terbuka: GateBarrier dinonaktifkan, LevelCompleteTrigger diaktifkan.
/// FR-11, NFR-04
/// </summary>
public class Gate : MonoBehaviour
{
    [Header("Gate Parts")]
    [SerializeField] private GameObject _gateBarrier;
    [SerializeField] private GameObject _levelCompleteTrigger;

    [Header("Visual")]
    [SerializeField] private Renderer _gateRenderer;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private Material _openMaterial;

    private bool _isOpen = false;

    private void OnEnable()
    {
        CoinCounter.OnAllCoinsCollected += Open;
    }

    private void OnDisable()
    {
        CoinCounter.OnAllCoinsCollected -= Open;
    }

    private void Start()
    {
        SetLocked();
    }

    private void Open()
    {
        if (_isOpen) return;
        _isOpen = true;

        if (_gateBarrier != null)
            _gateBarrier.SetActive(false);

        if (_levelCompleteTrigger != null)
            _levelCompleteTrigger.SetActive(true);

        if (_gateRenderer != null && _openMaterial != null)
            _gateRenderer.material = _openMaterial;
    }

    private void SetLocked()
    {
        if (_gateBarrier != null)
            _gateBarrier.SetActive(true);

        if (_levelCompleteTrigger != null)
            _levelCompleteTrigger.SetActive(false);

        if (_gateRenderer != null && _lockedMaterial != null)
            _gateRenderer.material = _lockedMaterial;
    }

    public bool IsOpen => _isOpen;
}
