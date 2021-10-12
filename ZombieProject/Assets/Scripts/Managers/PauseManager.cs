using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;

    private bool _pauseActived;
    private GunShoot _gunShoot;

    private void Awake()
    {
        _gunShoot = FindObjectOfType<GunShoot>();
    }

    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        PressPauseKey();
    }



    private void PressPauseKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused())
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _gunShoot.enabled = true;
        _pauseActived = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void PauseGame()
    {
        _gunShoot.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        _pauseActived = true;
    }

    private bool GamePaused()
    {
        if (_pauseActived)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
