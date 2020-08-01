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

/*
  NOTE [Kevin]:
  this class/script handle input when avatar image touched
*/

public class AvatarBox : MonoBehaviour
{
    public int index; // index [0-3] [yugi-joe-seto-ryo]
    GameObject pointer;
    
    void Start(){
	pointer = GameObject.Find("pointer");
    }

    void Update(){}

    void OnMouseDown(){

	// set pointer position to avatar image box
	pointer.transform.position =
	    new Vector3(transform.position.x,
			pointer.transform.position.y,
			pointer.transform.position.z);

	MessageSender.player_avatar_index = index;
    }
}
