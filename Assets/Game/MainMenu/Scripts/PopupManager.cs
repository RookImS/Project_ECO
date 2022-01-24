using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textTitle;
    public Text textDesc;
    public Button btnConfirm;
    public string curMethod;
    public string curParameter;
    void Start()
    {
        textTitle.GetComponent<Text>().text = "";
        textDesc.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTitle(string title) {
        textTitle.GetComponent<Text>().text = title;
    }

    public void setDesc(string desc) {
        textDesc.GetComponent<Text>().text = desc;
    }
    public void setMethod(string methodKey) {
        curMethod = methodKey;
        curParameter = "";
    }
    public void setParameter(string methodParameter) {
        curParameter = methodParameter;
    }
    public void handleOnClick() {
        switch (curMethod) {
            case "startNew":
                UIManager.handleStartNew();
                break;
            case "startLoad":
                int temp = int.Parse(curParameter);
                UIManager.handleStartLoad(temp);
                break;
            case "terminate":
                UIManager.terminate();
                break;
            default:
                break;
        }
    }
}
