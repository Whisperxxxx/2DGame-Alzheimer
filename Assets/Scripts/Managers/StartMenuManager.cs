using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    
    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1_1");
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenAbout()
    {
        aboutPanel.SetActive(true);
    }
    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }
    public void QuitGame()
    {
        // Quit the game
        #if UNITY_EDITOR  
            // 如果在Unity编辑器中运行，则停止播放  
            UnityEditor.EditorApplication.isPlaying = false;  
        #else  
            // 如果在构建后的应用程序中运行，则退出应用程序
            // Application.Quit();  
        #endif  
    }
}
