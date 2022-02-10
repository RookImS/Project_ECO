using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public static void handleStartNew () {
        SceneManager.LoadScene((int)GameManager.sceneIndex.ingame);
    }
    public static void handleStartLoad (int dataFileIndex) {
        SceneManager.LoadScene((int)GameManager.sceneIndex.ingame);
    }
    public static void terminate() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
