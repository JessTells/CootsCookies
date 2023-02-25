using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject ControlsUI;
    [SerializeField] GameObject RecipeUI;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private bool controlMenuUp = false;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowControls()
    {
        ControlsUI.SetActive(true);
    }

    public void HideControls()
    {
        ControlsUI.SetActive(false);
    }

    public void ShowRecipe()
    {
        RecipeUI.SetActive(true);
    }

    public void HideRecipe()
    {
        RecipeUI.SetActive(false);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }

}
