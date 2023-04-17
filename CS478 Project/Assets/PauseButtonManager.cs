using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    public void PauseButtonClicked()
    {
        SceneManager.LoadScene("PauseMenuScene");
    }
}