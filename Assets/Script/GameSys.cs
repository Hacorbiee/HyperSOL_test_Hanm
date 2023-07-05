using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void StartGame()
    {
        Time.timeScale = 1f;
        playerController.Instance.buttonQui.gameObject.SetActive(false);
        playerController.Instance.buttonSt.gameObject.SetActive(false);
        playerController.Instance.buttonP.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        playerController.Instance.buttonQui.gameObject.SetActive(true);
        playerController.Instance.buttonSt.gameObject.SetActive(true);
        playerController.Instance.buttonP.gameObject.SetActive(false);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene1");
    }
}
