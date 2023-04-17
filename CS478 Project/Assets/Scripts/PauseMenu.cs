using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
    }
    public void ResumeGame()
    {
        // Set the Time.timeScale back to 1 to resume the game
        Time.timeScale = 1f;

        // Load the game scene
        SceneManager.LoadScene("GameScreen");

        // Close the pause menu scene
        SceneManager.UnloadSceneAsync("PauseMenuScene");
    }
}
