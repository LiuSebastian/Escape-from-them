using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameplayMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject deathMenuUI;
    public GameObject player;
    public GameObject mainCamera;
    bool paused;
    private void Start()
    {
        Resume();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }         
        }
    }
    public void Resume()
    {
        mainCamera.GetComponent<AudioListener>().enabled = true;
        player.GetComponent<PJ_Detective>().enabled = true;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked; //hace al cursor invisible y lo lockea al medio de la screen
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        mainCamera.GetComponent<AudioListener>().enabled = false;
        player.GetComponent<PJ_Detective>().enabled = false;
        paused = true;
        Cursor.lockState = CursorLockMode.Confined; //hace al cursor visible y lo lockea dentro de la pantalla del juego
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Win()
    {
        mainCamera.GetComponent<AudioListener>().enabled = false;
        player.GetComponent<PJ_Detective>().enabled = false;
        winMenuUI.SetActive(true);
        StartCoroutine(ReturnMainMenu());
    }
    public void Death()
    {
        StartCoroutine(Wait());       
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        mainCamera.GetComponent<AudioListener>().enabled = false;
        player.GetComponent<PJ_Detective>().enabled = false;
        deathMenuUI.SetActive(true);
        StartCoroutine(ReturnMainMenu());
    }
    IEnumerator ReturnMainMenu()
    {
        yield return new WaitForSeconds(5);
        Menu();
    }
    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu");
    }
}
