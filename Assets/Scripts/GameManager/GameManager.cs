using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Mengelola state permainan: Playing, Paused, Completed.
/// Tidak menggunakan DontDestroyOnLoad agar fresh tiap scene load.
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Completed }

    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    [Header("UI Panels (assign di Inspector)")]
    public GameObject pauseMenuUI;
    public GameObject levelCompleteUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pauseClip;
    public AudioClip resumeClip;
    public AudioClip levelCompleteClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;

        // Pastikan kedua panel tersembunyi
        if (pauseMenuUI != null)       pauseMenuUI.SetActive(false);
        if (levelCompleteUI != null)   levelCompleteUI.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    // ── Pause ──────────────────────────────────────────────
    public void PauseGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        PlaySFX(pauseClip);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused) return;
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        PlaySFX(resumeClip);
    }

    // ── Level Complete ──────────────────────────────────────
    public void CompleteLevel()
    {
        if (CurrentState == GameState.Completed) return;
        CurrentState = GameState.Completed;
        StartCoroutine(CompleteLevelRoutine());
    }

    private IEnumerator CompleteLevelRoutine()
    {
        PlaySFX(levelCompleteClip);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        if (levelCompleteUI != null) levelCompleteUI.SetActive(true);
    }

    // ── Navigation ─────────────────────────────────────────
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
        SceneManager.LoadScene(0);
    }

    // ── Helpers ────────────────────────────────────────────
    private void PlaySFX(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
