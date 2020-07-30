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
  TODO [Kevin]:
    - All common Big 2 rules are well implemented.
    https://www.pagat.com/climbing/bigtwo.html
    ++ a standard 52 card pack is used. 
    ++ the cards ranking from high to low: 2-A-K-Q-J-10-9-8-7-6-5-4-3. 
    ++ There is also an ordering of suits - from high to low: spades, hearts, clubs, diamonds

    ++ Pairs:
    A pair of equal ranked cards - twos are highest and threes are lowest. 
    Any higher ranked pair beats one with lower ranks. 
    Between equal ranked pairs, the one containing the highest suit is better
    for example (sp)9-(dia)9 beats (hr)9-(cl)9 because the spade is higher than the heart, 
    but (cl)Q-(dia)Q beats (sp)J-(hr)J because queens beat jacks.

    +++ card value, shape value for check validation 

    - Can choose a character at the beginning. 
    Character avatars can change depending on the game condition during gameplay. 
    For example, when winning the character is smiling, 
    while when losing the character looks sad / angry.

    - As the game is 4 players, the other 3 players would be run by AI.
*/

public class Main : MonoBehaviour
{
    enum TurnState{
	P1,
	P2,
	P3,
	P4
    }

    enum ActState{ // action state -> what we do on one turn...
	ACT_THROW,
	ACT_PASS,
	ACT_END
    }

    TurnState turn_state;
    ActState act_state;
    List<Card> card_deck = new List<Card>();

    Player p1;
    Player p2;
    Player p3;
    Player p4;

    void InitDeck(){
	for(int j = 0; j < 4; j++){
	    for(int i = 0; i < 13; i++){
		Card card = new Card(j, i + 1, i);
		card_deck.Add(card);
		// Debug.Log(card.image_source);
	    } // i
	} // j
    }

    void RandCard(){ // give 13 random card to each player
	// int count = 0;
	do{
	    int rand_index = Random.Range(0, card_deck.Count - 1);

	    if(p1.card_on_hand.Count < 13){
		p1.card_on_hand.Add(card_deck[rand_index]);
		Debug.Log(p1.player_name + " -> " + card_deck[rand_index].image_source);
	    }
	    else if(p2.card_on_hand.Count < 13){
		p2.card_on_hand.Add(card_deck[rand_index]);
		Debug.Log(p2.player_name + " -> " + card_deck[rand_index].image_source);
	    }
	    else if(p3.card_on_hand.Count < 13){
		p3.card_on_hand.Add(card_deck[rand_index]);
		Debug.Log(p3.player_name + " -> " + card_deck[rand_index].image_source);
	    }
	    else if (p4.card_on_hand.Count < 13){
		p4.card_on_hand.Add(card_deck[rand_index]);
		Debug.Log(p4.player_name + " -> " + card_deck[rand_index].image_source);
	    }

	    card_deck.RemoveAt(rand_index);
	    // count++;
	    // Debug.Log("rand remove" + count.ToString());
	}
	while(card_deck.Count > 0);
	// Debug.Log("end rand remove");
    }
        
    void Start(){
	InitDeck();

	p1 = new Player("Player1");
	p2 = new Player("COM1");
	p3 = new Player("COM2");
	p4 = new Player("COM3");

	RandCard();

	turn_state = TurnState.P1;
    }

    void Update(){
	switch(turn_state){
	    case TurnState.P1:
		break;
	    case TurnState.P2:
		break;
	    case TurnState.P3:
		break;
	    case TurnState.P4:
		break;
	}
    }
}
