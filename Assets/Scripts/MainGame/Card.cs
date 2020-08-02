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

    // load card attribute from json file in main class/script
    public Card(int _card_shape,
		int _shape_value,
		string _shape_name,
		int _card_value,
		string _card_name,
		int _card_index){

	card_shape = _card_shape;
	shape_value = _shape_value;
	card_shape_str = _shape_name;
	card_value = _card_value;
	card_name = _card_name;
	card_index = _card_index;
	card_index_str = card_index.ToString();
	image_source = "images/cards/" + card_shape_str + "/" + card_index_str;
    }

}
