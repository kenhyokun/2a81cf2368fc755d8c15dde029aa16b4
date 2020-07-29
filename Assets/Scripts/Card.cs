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
  diamond = 2
  club = 1
  
  card index:
  [0-12]
  [ace, 2-10, J, Q, K] 
  base on image name in card image asset dir

*/

public class Card : MonoBehaviour
{

    enum Shape{
	SPADE,
	HEART,
	DIAMOND,
	CLUB
    }

    public int card_shape;
    public int card_value;
    public int shape_value;
    int card_index;

    void Start()
    {
	switch(card_shape){
	    case (int)Shape.SPADE:
		shape_value = 4;
		break;
	    case (int)Shape.HEART:
		shape_value = 3;
		break;
	    case (int)Shape.DIAMOND:
		shape_value = 2;
		break;
	    case (int)Shape.CLUB:
		shape_value = 1;
		break;
	}

    }

    void Update()
    {
        
    }
}
