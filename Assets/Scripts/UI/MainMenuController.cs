using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller untuk scene Main Menu.
/// Attach ke GameObject di scene MainMenu.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Level1 ada di build index 1
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
