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

    /*
      NOTE [Kevin]: this thingies contain player card set. 
      what set player have and which index from card on hand 
      for each set
    */
    public List<int> single_set_list = new List<int>();
    public List<List<int>> pair_set_list = new List<List<int>>();
    public List<List<int>> triple_set_list = new List<List<int>>();
    public List<List<int>> five_set_list = new List<List<int>>();

    // avatar photo
    public Texture game_photo_texture; 
    public Texture win_photo_texture;
    public Texture lose_photo_texture;

    public Player(string _player_name, string photo_str){
	player_name = _player_name;
	SetPhotoTexture(photo_str);
    }

    public void SetPhotoTexture(string photo_str){
	game_photo_texture = Resources.Load<Texture2D>("images/avatars/photo_" + photo_str + "_game");
	win_photo_texture = Resources.Load<Texture2D>("images/avatars/photo_" + photo_str + "_win");
	lose_photo_texture = Resources.Load<Texture2D>("images/avatars/photo_" + photo_str + "_lose");
    }

    public void SetCardSet(int card_set, int start_index, int last_index){
	switch(card_set){

	    case 1: // single
		single_set_list.Add(last_index);
		break;

	    case 2: // pair
		List<int> pair = new List<int>();
		pair.Add(start_index);
		pair.Add(last_index);

		pair_set_list.Add(pair);
		break;

	    case 3: // triple
		List<int> triple = new List<int>();
		triple.Add(start_index);
		triple.Add(last_index - 1);
		triple.Add(last_index);

		triple_set_list.Add(triple);
		break;

	    case 4:
		break;
	}
    }

    public int GetTotalCardSet(){
	return single_set_list.Count +
	    pair_set_list.Count +
	    triple_set_list.Count +
	    five_set_list.Count;
    }

}
