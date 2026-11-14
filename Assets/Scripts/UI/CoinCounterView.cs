using TMPro;
using UnityEngine;

/// <summary>
/// Menampilkan "Coins: n / 10" dan teks Objective di HUD.
/// Referensi bisa di-assign lewat Inspector atau via HUDAutoSetup.SetReferences().
/// FR-10
/// </summary>
public class CoinCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _objectiveText;

    /// <summary>Dipanggil oleh HUDAutoSetup saat membuat UI secara runtime.</summary>
    public void SetReferences(TextMeshProUGUI coinText, TextMeshProUGUI objectiveText)
    {
        _coinText      = coinText;
        _objectiveText = objectiveText;
    }

    private void OnEnable()
    {
        CoinCounter.OnCoinCountChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        CoinCounter.OnCoinCountChanged -= UpdateDisplay;
    }

    private void Start()
    {
        int target = CoinCounter.Instance != null ? CoinCounter.Instance.TargetCount : 10;
        UpdateDisplay(CoinCounter.Instance != null ? CoinCounter.Instance.CurrentCount : 0, target);
    }

    private void UpdateDisplay(int current, int target)
    {
        if (_coinText != null)
            _coinText.text = $"Coins: {current} / {target}";

        if (_objectiveText != null)
            _objectiveText.text = current >= target
                ? "Objective: Reach the gate!"
                : "Objective: Collect all coins";
    }
}
