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

  shape value from high to low:
  spade = 4
  heart = 3
  club = 2
  diamond = 1

  card value from high to low:
  [2-A-K-Q-J-10-9-8-7-6-5-4-3]
  [13-1]
  
  card index:
  [0-12]
  [3-4-5-6-7-8-9-10-J-Q-K-A-2]
  base on image name in card image asset dir &
  lowest card value to highest value

*/

public class Card
{

    enum Shape{
	SPADE,
	HEART,
	CLUB,
	DIAMOND
    }

    public int card_shape {get;}
    public int card_value {get;}
    public int shape_value {get;}
    public int card_index {get;}
    public string card_shape_str {get;}
    public string card_index_str {get;}
    public string image_source {get;}

    public Card(int _card_shape, int _card_value, int _card_index){
	card_shape = _card_shape;
	card_value = _card_value;
	card_index = _card_index;

    	switch(card_shape){
    	    case (int)Shape.SPADE:
    		shape_value = 4;
		card_shape_str = "spade";
    		break;
    	    case (int)Shape.HEART:
    		shape_value = 3;
		card_shape_str = "heart";
    		break;
    	    case (int)Shape.CLUB:
    		shape_value = 2;
		card_shape_str = "club";
    		break;
    	    case (int)Shape.DIAMOND:
    		shape_value = 1;
		card_shape_str = "diamond";
    		break;
    	}

	card_index_str = card_index.ToString();
	image_source = "images/cards/" + card_shape_str + "/" + card_index_str;

    }

}
