using UnityEngine;

/// <summary>
/// Menangani input Escape untuk pause/resume dan menghubungkan tombol pause menu.
/// Attach ke GameObject mana saja yang aktif di scene Level.
/// FR-13
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance == null) return;

            if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
                GameManager.Instance.ResumeGame();
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
                GameManager.Instance.PauseGame();
        }
    }
}
