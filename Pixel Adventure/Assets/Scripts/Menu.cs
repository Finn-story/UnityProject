using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GameStart()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene"); // according to your project structure, replace "GameScene" with the actual name of your game scene like 0 or 1.
    }
    public void GameExit()
    {
        // Exit the application
        Application.Quit();
        
        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
