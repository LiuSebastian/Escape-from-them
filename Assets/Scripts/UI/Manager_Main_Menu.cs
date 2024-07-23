using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Main_Menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void PlayScene()
    {
        //MenuMusic.Instance.gameObject.GetComponent<AudioSource>().Pause();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    public void MainMenuScene()
    {
        //MenuMusic.Instance.gameObject.GetComponent<AudioSource>().Play();
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }
    public void HowToPlayScene()
    {
        SceneManager.LoadScene("How To Play");
    }
    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

