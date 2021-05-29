using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 2f);
    }

    public void StartGame()
    {
        gameStarted = true;
    }
    public void RestartGame()
    {
        Invoke("restart", 1f);
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
