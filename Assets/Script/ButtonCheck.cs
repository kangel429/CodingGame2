using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    public void ClickButton (GameObject gbt) {
        gbt.SetActive(false);	
	}
}
