using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void EXIT()
    {
        Application.Quit();
    }

}
