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
  just class/script that I used to set basic parameter in application 
  that share in common between scenes
*/

public class BaseApplication : MonoBehaviour
{
    void Start(){
	Screen.SetResolution(1280, 720, false); 
    }

    void Update(){}
}
