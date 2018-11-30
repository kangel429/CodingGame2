using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartManager : MonoBehaviour {
    public TMP_InputField nickName;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	public void SaveNickName () {
        if(nickName.text != ""){
            Debug.Log("111"+nickName.text);
            PlayerPrefs.SetString("NickName", nickName.text);
        }else{

        }
    }
}
