using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static int currentLevelIndex = -1;

    public static void LoadLevel(int index)
    {
        currentLevelIndex = index;
        SceneManager.LoadScene(index);
    }

    public static void LoadLevel(string levelName)
    {
        int index = SceneManager.GetSceneByName(levelName).buildIndex;
        LoadLevel(index);
    }

    public static void LoadNextLevel()
    {
        if (currentLevelIndex < 0)
        {
            currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        }
        LoadLevel(currentLevelIndex + 1);
    }

    public static void RestartLevel()
    {
        if (currentLevelIndex < 0)
        {
            currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        }
        LoadLevel(currentLevelIndex);
    }
}
