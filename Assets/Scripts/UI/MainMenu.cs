using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteKey("tier1Ore");
        PlayerPrefs.DeleteKey("tier2Ore");
        PlayerPrefs.DeleteKey("tier3Ore");
        PlayerPrefs.DeleteKey("tool1level");
        PlayerPrefs.DeleteKey("tool2level");
        PlayerPrefs.DeleteKey("tool3level");
        PlayerPrefs.DeleteKey("recharges");
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
