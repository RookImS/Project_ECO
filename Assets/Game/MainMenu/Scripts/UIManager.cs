using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public void LoadGame(int dataFileIndex)
    {
        SceneManager.LoadScene((int)GameManager.sceneIndex.ingame);

        // ������ �ҷ��ͼ� ó���ϱ�
    }
}
