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

  this class will handle card attribute

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
    public string card_name{get;}
    public int single_card_value {get;}

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

	single_card_value = card_value + shape_value;

	switch(card_index){
	    case 0:
		card_name = card_shape_str + "_" + "3";
		break;
	    case 1:
		card_name = card_shape_str + "_" + "4";
		break;
	    case 2:
		card_name = card_shape_str + "_" + "5";
		break;
	    case 3:
		card_name = card_shape_str + "_" + "6";
		break;
	    case 4:
		card_name = card_shape_str + "_" + "7";
		break;
	    case 5:
		card_name = card_shape_str + "_" + "8";
		break;
	    case 6:
		card_name = card_shape_str + "_" + "9";
		break;
	    case 7:
		card_name = card_shape_str + "_" + "10";
		break;
	    case 8:
		card_name = card_shape_str + "_" + "J";
		break;
	    case 9:
		card_name = card_shape_str + "_" + "Q";
		break;
	    case 10:
		card_name = card_shape_str + "_" + "K";
		break;
	    case 11:
		card_name = card_shape_str + "_" + "A";
		break;
	    case 12:
		card_name = card_shape_str + "_" + "2";
		break;
	}

	card_index_str = card_index.ToString();
	image_source = "images/cards/" + card_shape_str + "/" + card_index_str;

    }

}
