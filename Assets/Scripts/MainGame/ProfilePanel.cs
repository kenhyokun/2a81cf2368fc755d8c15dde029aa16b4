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

/*
  NOTE [Kevin]:
  this class/script will handle profile panel in main game scene 
*/

public class ProfilePanel : MonoBehaviour
{
    public int index; // [1-4]
    public Player player;

    Text player_name_text;
    Text card_count_text;
    RawImage avatar_photo;
    
    void Start(){ // set profile panel avatar photo, player name, player card count

	// set player
	switch(index){
	    case 1:
		player = Main.GetMain().p1;
		break;
	    case 2:
		player = Main.GetMain().p2;
		break;
	    case 3:
		player = Main.GetMain().p3;
		break;
	    case 4:
		player = Main.GetMain().p4;
		break;
	}
	
	avatar_photo =
	    GameObject.Find("ProfilePanel" + index.ToString() + "/AvatarPhoto").
	    GetComponent<RawImage>(); 

	player_name_text =
	    GameObject.Find("ProfilePanel" + index.ToString() + "/PlayerName").
	    GetComponent<Text>(); 

	card_count_text =
	    GameObject.Find("ProfilePanel" + index.ToString() + "/PlayerCard").
	    GetComponent<Text>(); 

	player_name_text.text = player.player_name;
	card_count_text.text = player.card_on_hand.Count.ToString();
	avatar_photo.texture = player.win_photo_texture;
    }

    void Update(){ // update profile panel avatar photo & player card count
        
    }
}
