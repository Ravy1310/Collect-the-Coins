using System;
using UnityEngine;

/// <summary>
/// Menyimpan jumlah koin yang sudah dikumpulkan, menyiarkan perubahan,
/// dan mengirimkan event saat semua koin terkumpul.
/// FR-10, FR-11, NFR-03, NFR-04
/// </summary>
public class CoinCounter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _targetCoinCount = 16;

    // Singleton ringan — hanya untuk akses mudah, bukan dependensi global
    public static CoinCounter Instance { get; private set; }

    // Event untuk UI (CoinCounterView) dan Gate
    public static event Action<int, int> OnCoinCountChanged;  // (current, target)
    public static event Action           OnAllCoinsCollected;

    public int CurrentCount { get; private set; } = 0;
    public int TargetCount  => _targetCoinCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    /// <summary>
    /// Dipanggil oleh Coin saat dikumpulkan.
    /// </summary>
    public void RegisterCoin()
    {
        if (CurrentCount >= _targetCoinCount) return;

        CurrentCount++;
        OnCoinCountChanged?.Invoke(CurrentCount, _targetCoinCount);

        if (CurrentCount >= _targetCoinCount)
            OnAllCoinsCollected?.Invoke();
    }
}
