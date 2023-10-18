using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public void LoadMenuScene()
    {
        // Load the specified scene by its name.
        SceneManager.LoadScene(0);
    }
}
