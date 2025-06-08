using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Quit()
    {
        #if UNITYEDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
        #endif
        Application.Quit();
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Score", 0); //* Reset score when starting the game
        SceneManager.LoadScene(1);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
