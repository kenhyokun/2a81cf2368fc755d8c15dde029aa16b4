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

    game refference: 
    https://play.google.com/store/apps/details?id=com.emagssob.big2&hl=en_US

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

    public GameObject card_game_object;
    public GameObject p1_deck;
    public GameObject p2_deck;
    public GameObject p3_deck;
    public GameObject p4_deck;

    void InitDeck(){
	for(int j = 0; j < 4; j++){
	    for(int i = 0; i < 13; i++){
		Card card = new Card(j, i + 1, i);
		card_deck.Add(card);
	    } // i
	} // j
    }

    void RandCard(){ // give 13 random card to each player
	do{
	    int rand_index = Random.Range(0, card_deck.Count);

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
	}
	while(card_deck.Count > 0);
    }

    void InstantiateCard(Player player, GameObject deck, float offset_x = 0.7f){ // instancing card_game_object to each player
	for(int i = 0; i < player.card_on_hand.Count; i++){

	    GameObject card_inst =
		GameObject.Instantiate(card_game_object);

	    card_inst.transform.position =
		new Vector3(deck.transform.position.x + (offset_x * i),
			    deck.transform.position.y,
			    deck.transform.position.z);

	    card_inst.transform.name = player.player_name + " " +
		player.card_on_hand[i].card_name;

	    if(player.player_name == "Player1"){
		card_inst.transform.tag = "player_card";
		card_inst.GetComponent<Card_Game_Object>().is_open = true;
	    }
	    else{
		card_inst.transform.tag = "com_card";
	    }

	    card_inst.GetComponent<Card_Game_Object>().
		GetComponent<SpriteRenderer>().sortingOrder = i; 

	    card_inst.GetComponent<Card_Game_Object>().card_data =
		player.card_on_hand[i];

	    card_inst.transform.parent = deck.transform;
	}
    }
        
    void Start(){

	card_game_object = GameObject.Find("CardGameObject");
	p1_deck = GameObject.Find("Player1Deck");
	p2_deck = GameObject.Find("Player2Deck");
	p3_deck = GameObject.Find("Player3Deck");
	p4_deck = GameObject.Find("Player4Deck");

	InitDeck();

	p1 = new Player("Player1");
	p2 = new Player("COM1");
	p3 = new Player("COM2");
	p4 = new Player("COM3");

	RandCard();
	InstantiateCard(p1, p1_deck);
	InstantiateCard(p2, p2_deck, 0.4f);
	InstantiateCard(p3, p3_deck);
	InstantiateCard(p4, p4_deck, 0.4f);

	// vertical orientation arrange
	p2_deck.transform.Rotate(0.0f, 0.0f, 90.0f);
	p4_deck.transform.Rotate(0.0f, 0.0f, -90.0f);

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
