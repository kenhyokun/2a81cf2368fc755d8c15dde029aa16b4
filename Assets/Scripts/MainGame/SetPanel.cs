/*
  Kevin Haryo Kuncoro
  kevinhyokun91@gmail.com
  ------------------------------
  Programming Challenge - Touchten
  2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : MonoBehaviour
{
    Text set_text;

    public void Show(int set = 1){
	
	switch(set){
	    case 1:
		set_text.text = "Single";
		break;
	    case 2:
		set_text.text = "Pair";
		break;
	    case 3:
		set_text.text = "Triple";
		break;
	}

	gameObject.SetActive(true);
    }

    public void Hide(){
	gameObject.SetActive(false);
    }

    void Start(){
	set_text = transform.GetChild(0).GetComponent<Text>();
	Hide();     
    }

    void Update(){}
}
