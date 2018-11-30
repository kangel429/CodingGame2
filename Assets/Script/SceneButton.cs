using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour {

    public GameStartManager gameStartManager;
    public GameObject nickNameAlarm;


    // Use this for initialization
    void Start () {
      
    }
	
	public void NextStage(string nextScene) {
      
        SceneManager.LoadScene(nextScene);

    }
    public void NextManinStage(string nextScene){

        if(gameStartManager.nickName.text != ""){
            SceneManager.LoadScene(nextScene);
            nickNameAlarm.SetActive(false);
        }
        else{
            nickNameAlarm.SetActive(true);
        }


    }
}
