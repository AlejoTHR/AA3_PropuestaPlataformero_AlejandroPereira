using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [Header("objects")]
    [SerializeField] public Canvas _canvas;
    [SerializeField] public Player_Controller _plyrCntrllr;

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void EXIT()
    {
        Application.Quit();
    }

    public void PAUSE()
    {
        Time.timeScale = 0f;
        _canvas.enabled = true;
        _plyrCntrllr.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        _canvas.enabled = false;
        _plyrCntrllr.enabled = false;

    }

}
