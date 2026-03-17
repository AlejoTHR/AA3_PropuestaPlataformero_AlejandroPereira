using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [Header("objects")]
    public Canvas _canvas;

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void EXIT()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        _canvas.enabled = false;
    }

}
