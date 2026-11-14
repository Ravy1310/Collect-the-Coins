using UnityEngine;

/// <summary>
/// Koin berputar dan terambil saat pemain menyentuhnya via Trigger Collider.
/// Deteksi menggunakan OnTriggerEnter saja — tidak ada deteksi jarak manual di Update
/// agar koin tidak langsung tercollect saat scene dimuat.
/// FR-08, FR-09
/// </summary>
public class Coin : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed = 90f;

    [Header("Audio")]
    [SerializeField] private AudioClip _collectSound;

    private bool _collected = false;

    private void Update()
    {
        if (_collected) return;

        // Animasi berputar
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        if (!other.CompareTag("Player")) return;
        Collect();
    }

    private void Collect()
    {
        if (_collected) return;
        _collected = true;

        // SFX
        if (_collectSound != null)
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);

        // Lapor ke CoinCounter
        CoinCounter.Instance?.RegisterCoin();

        // Hancurkan hanya GameObject koin ini
        Destroy(gameObject, _collectSound != null ? _collectSound.length : 0f);
    }
}
