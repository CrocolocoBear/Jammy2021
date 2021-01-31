using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject GameOverUI;
    public GameObject crosshair;
    public bool GameOver = false;

    private void Start()
    {
        Resume();
    }

    void Update()
    {
        if (GameOver)
        {
            Pause();
        }
    }

    public void Resume()
    {
        GameOverUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1F;
        GameIsPaused = false;

        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        GameOverUI.SetActive(true);
        crosshair.SetActive(false);
        Time.timeScale = 0F;
        GameIsPaused = true;

        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
