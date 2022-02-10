using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public void LoadGame(int dataFileIndex)
    {
        SceneManager.LoadScene((int)GameManager.sceneIndex.ingame);

        // 데이터 불러와서 처리하기
    }
}
