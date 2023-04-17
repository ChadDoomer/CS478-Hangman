using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseMenu;

    public void OnResumeButtonClicked()
    {
        pauseMenu.GetComponent<PauseMenu>().ResumeGame();
    }
}