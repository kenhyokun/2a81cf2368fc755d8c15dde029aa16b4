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
  this class/script will handle card as game object
*/

public class Card_Game_Object : MonoBehaviour
{
    public SpriteRenderer sprite_renderer {set; get;}

    public bool is_open; // if false, card game object sprite image = card_back
    public bool is_selected;
    public Card card_data {set; get;}
    public int index_on_hand {set; get;}
    public int index_on_selected_list;
    Vector3 start_position;
    public Player card_owner {set; get;}

    void Start(){
        sprite_renderer = GetComponent<SpriteRenderer>();
	start_position = transform.position;
    }

    void Update(){

	// card open state
	if(is_open){
	    if(card_data != null){
		sprite_renderer.sprite = Resources.Load<Sprite>(card_data.image_source);
	    }
	}
	else{
	    sprite_renderer.sprite = Resources.Load<Sprite>("images/cards/card_back");
	}

    }

    public void DestroyCard(){
	//need to reset index...
	card_owner.card_on_hand.RemoveAt(index_on_hand);
	Destroy(gameObject);
    }

    void OnMouseDown(){
	if(transform.tag == "player_card"){ // only player card
	    if(!is_selected){

		transform.position =
		    new Vector3(transform.position.x,
				transform.position.y + 0.3f,
				transform.position.z);

		Main.GetMain().AddCardToSelectionList(gameObject);

		is_selected = true;
	    }
	    else{
		Main.GetMain().RemoveCardFromSelectionList(index_on_selected_list);
		transform.position = start_position;
		is_selected = false;
	    }
	}
    }
}
