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

public class Player
{
    public List<Card> card_on_hand = new List<Card>();
    public string player_name {set; get;}

    public Player(string _player_name){
	player_name = _player_name;
    }
}
