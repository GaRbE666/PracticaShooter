using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Text porcentajeText;
    [SerializeField] private AudioSource menuAudio;

    private void Start()
    {
        loadingScreen.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        menuAudio.Play();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            porcentajeText.text = (progress * 100f).ToString("00") + "%";
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
