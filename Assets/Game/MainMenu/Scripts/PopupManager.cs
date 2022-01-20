using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textTitle;
    public Text textDesc;
    public Button btnConfirm;
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
    
    public void handleOnClick(UnityEngine.Events.UnityAction onClick) {
        btnConfirm.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
