using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaybuttonChange : MonoBehaviour {
    public Sprite playBuSprite;
    public Sprite stopBuSprite;

    public Image img; 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    public void ChangeButtonImg () {

        img.sprite = stopBuSprite;


    }
    public void ChangeButtonImg1()
    {

        img.sprite = playBuSprite;


    }
}
