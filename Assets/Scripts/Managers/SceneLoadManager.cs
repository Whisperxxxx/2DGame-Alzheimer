using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    public void LoadLevel3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
    }

    public void LoadTruth()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Truth");
    }

}
