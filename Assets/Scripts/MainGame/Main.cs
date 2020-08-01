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

    public Player p1 {set; get;}
    public Player p2 {set; get;}
    public Player p3 {set; get;}
    public Player p4 {set; get;}

    public GameObject card_game_object;
    public GameObject p1_deck;
    public GameObject p2_deck;
    public GameObject p3_deck;
    public GameObject p4_deck;

    public List<GameObject> player_selected_card_list =
	new List<GameObject>(); // list that contain player selected card (game object). select or deselect card will affect this list

    string photo_str;

    public static Main GetMain(){ // static function to get main component from main game object, so we dont need create main instance on other script/class when we need it
	return GameObject.Find("Main").GetComponent<Main>();
    }

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
	    }
	    else if(p2.card_on_hand.Count < 13){
		p2.card_on_hand.Add(card_deck[rand_index]);
	    }
	    else if(p3.card_on_hand.Count < 13){
		p3.card_on_hand.Add(card_deck[rand_index]);
	    }
	    else if (p4.card_on_hand.Count < 13){
		p4.card_on_hand.Add(card_deck[rand_index]);
	    }

	    card_deck.RemoveAt(rand_index);
	}
	while(card_deck.Count > 0);
    }

    void SortCardByValue(Player player){ // just... bubble sort...
	for(int i = 0; i < player.card_on_hand.Count; i++){
	    Card temp_card = player.card_on_hand[i];
	    int index = i;
	    int max_value = temp_card.card_value;

	    for(int j = i + 1; j < player.card_on_hand.Count; j++){
		if(player.card_on_hand[j].card_value > max_value){
		    index = j;
		    max_value = player.card_on_hand[j].card_value;
		} // if max value
	    } // j

	    player.card_on_hand[i] = player.card_on_hand[index];
	    player.card_on_hand[index] = temp_card;

	} // i
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
		player.card_on_hand[i].card_name + " " +
		player.card_on_hand[i].card_value;

	    if(player.player_name == "Player1"){
		card_inst.transform.tag = "player_card";
		card_inst.GetComponent<Card_Game_Object>().is_open = true;
	    }
	    else{
		card_inst.transform.tag = "com_card";
	    }

	    card_inst.GetComponent<Card_Game_Object>().card_owner = player;
	    card_inst.GetComponent<Card_Game_Object>().index_on_hand = i;

	    card_inst.GetComponent<Card_Game_Object>().
		GetComponent<SpriteRenderer>().sortingOrder = i; 

	    card_inst.GetComponent<Card_Game_Object>().card_data =
		player.card_on_hand[i];

	    card_inst.transform.parent = deck.transform;
	}
    }

    // int card_check_count;
    int start_index;
    int last_index;

    void CheckCardOnHand(Player player,
			 int curr_count = 0,
			 int last_value = 0){ // recursive function to get card set and its index

	int curr_value = 0; 

	if(curr_count < player.card_on_hand.Count){

	    curr_value = player.card_on_hand[curr_count].card_value;

	    if(curr_count == 0){

		Debug.Log(curr_count);

		start_index = curr_count;
		last_index = curr_count;
		CheckCardOnHand(player, curr_count + 1, curr_value); 
	    }
	    else{

		if(last_value == curr_value){

		    Debug.Log(curr_count);

		    last_index = curr_count;
		    CheckCardOnHand(player, curr_count + 1, curr_value); 
		}
		else{ // cut here...

		    int card_set = last_index - start_index + 1;
		    player.SetCardSet(card_set, start_index, last_index);

		    Debug.Log(curr_count);

		    Debug.Log(start_index + " " + last_index + " -> " + card_set);

		    start_index = curr_count;
		    last_index = curr_count;

		    CheckCardOnHand(player, curr_count + 1, curr_value); 

		}

	    }

	    // Debug.Log(curr_count);
	}
	else{

	    int card_set = last_index - start_index + 1;
	    player.SetCardSet(card_set, start_index, last_index);

	    Debug.Log(curr_count);

	    Debug.Log(start_index + " " + last_index + " -> " + card_set);

	    start_index = 0;
	    last_index = 0;

	    Debug.Log("total card set " + player.GetTotalCardSet());
	    Debug.Log("single card set " + player.single_set_list.Count);
	    Debug.Log("pair card set " + player.pair_set_list.Count);
	    Debug.Log("triple card set " + player.triple_set_list.Count);
	}

    }
        
    // this function we call at Card_Game_Object script/class
    public void AddCardToSelectionList(GameObject card_game_object){
	card_game_object.GetComponent<Card_Game_Object>().index_on_selected_list =
	    player_selected_card_list.Count;
	
	player_selected_card_list.Add(card_game_object);
    }
    public void RemoveCardFromSelectionList(int index){
	player_selected_card_list.RemoveAt(index);

	//reset index after removing card from list
	for(int i = 0; i < player_selected_card_list.Count; i++){

	    player_selected_card_list[i].
		GetComponent<Card_Game_Object>().
		index_on_selected_list = i;
	}
    }
    // this function we call at Card_Game_Object script/class
    
    bool is_on_set; 
    void IsOnSet(int max_count,
		 int curr_count = 0,
		 int last_value = 0){ // recursive function to check card set/suits pair, triple/three of kind etc

	int curr_value =
	    player_selected_card_list[curr_count].
	    GetComponent<Card_Game_Object>().card_data.card_value;

	if(curr_count < max_count - 1){
	    if(curr_count == 0){

		Debug.Log(curr_count + ", " + (max_count - 1).ToString() + " -> " + curr_value);

		IsOnSet(max_count, curr_count + 1, curr_value);

	    }
	    else{

		Debug.Log(curr_count + ", " + (max_count - 1).ToString() + " -> " + curr_value);

		if(last_value == curr_value){
		    IsOnSet(max_count, curr_count + 1, curr_value);
		}

	    }
	}  
	else{

	    Debug.Log(curr_count + ", " + max_count + " -> " + last_value + ", " + curr_value);
	     if(last_value == curr_value){
	     	Debug.Log("on set...");
		is_on_set = true;
	     }
	     else{
	     	Debug.Log("not on set...");
		is_on_set = false;
	     }
	}

    }


    // event handler on mouse click
    public void SubmitCard(){

	int selected_count = player_selected_card_list.Count;
	if(selected_count > 0){

	    if(selected_count == 1){
		Debug.Log("single");
	    }
	    else if(selected_count == 2){
		IsOnSet(2);
		if(is_on_set){
		    Debug.Log("pair");
		}
		else{
		    Debug.Log("just nothing...");
		}
	    }
	    else if(selected_count == 3){
		IsOnSet(3);
		if(is_on_set){
		    Debug.Log("triples");
		}
		else{
		    Debug.Log("just nothing...");
		}
	    }


	} // selected count > 0
	// player_selected_card_list[0].GetComponent<Card_Game_Object>().DestroyCard();
	// player_selected_card_list.RemoveAt(0);
    }
    // event handler on mouse click
    
    void Awake(){
	switch(MessageSender.player_avatar_index){
	    case 0:
		photo_str = "yugi";
		break;
	    case 1:
		photo_str = "joe";
		break;
	    case 2:
		photo_str = "seto";
		break;
	    case 3:
		photo_str = "ryo";
		break;
	}

	p1 = new Player("Player1", photo_str);
	p2 = new Player("COM1", "joe");
	p3 = new Player("COM2", "seto");
	p4 = new Player("COM3", "ryo");

    }
    
    void Start(){

	card_game_object = GameObject.Find("CardGameObject");
	p1_deck = GameObject.Find("Player1Deck");
	p2_deck = GameObject.Find("Player2Deck");
	p3_deck = GameObject.Find("Player3Deck");
	p4_deck = GameObject.Find("Player4Deck");

	InitDeck();

	RandCard();

	SortCardByValue(p1);
	SortCardByValue(p2);
	SortCardByValue(p3);
	SortCardByValue(p4);

	CheckCardOnHand(p1);

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
	Debug.Log("avatar index:" + MessageSender.player_avatar_index + photo_str);
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
