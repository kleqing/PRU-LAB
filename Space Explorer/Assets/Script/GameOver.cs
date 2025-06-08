using System;
using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool isGameOver = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship");
    }

    private void Update()
    {
        if (player == null)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }

        StartCoroutine(LoadGameOverScene());
    }
    
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
