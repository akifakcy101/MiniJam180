using System;
using UnityEngine;

public class GameUIActions : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject howToPlayMenu;
    public bool isPaused = false;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        Debug.Log(Time.timeScale.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!howToPlayMenu.activeSelf)
            {
                if (isPaused == true)
                {
                    Time.timeScale = 1f;
                    pauseMenu.SetActive(false);
                    isPaused = false;
                }
                else
                {
                    Time.timeScale = 0f;
                    pauseMenu.SetActive(true);
                    isPaused = true;
                }
            }

        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void ClosePaused()
    {
        pauseMenu.SetActive(false);
    }

    public void ReturnPaused()
    {
        pauseMenu.SetActive(true);
    }
    public void pauseScreenResume()
    {
        isPaused = false;
    }

}
