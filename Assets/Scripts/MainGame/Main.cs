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
using SimpleJSON;

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

    enum SetState{ // card set state
	SINGLE,
	PAIR,
	TRIPLE
    }

    int turn_state;
    ActState act_state;
    List<Card> card_deck = new List<Card>();

    public Player p1 {set; get;}
    public Player p2 {set; get;}
    public Player p3 {set; get;}
    public Player p4 {set; get;}
    public Player player_turn {set; get;}

    public GameObject card_game_object;
    public GameObject p1_deck;
    public GameObject p2_deck;
    public GameObject p3_deck;
    public GameObject p4_deck;

    public List<GameObject> player_selected_card_list =
	new List<GameObject>(); // list that contain player selected card (game object). select or deselect card will affect this list

    string photo_str;
    GameObject point; // submited card position
    SetPanel set_panel;

    Player last_player_thrower; // last player who submit/throw card into tableeee....
    int card_value_on_table; // value of submited card on table
    bool is_first_run = true;
    int set_state;
    int set_state_on_table;

    public static Main GetMain(){ // static function to get main component from main game object, so we dont need create main instance on other script/class when we need it
	return GameObject.Find("Main").GetComponent<Main>();
    }

    public static Debugger GetDebugger(){ // static function to get debugger component from debugger game object, so we dont need create debugger instance on other script/class when we need it
	return GameObject.Find("Debugger").GetComponent<Debugger>();
    }

    void InitDeck(){ // init 52 card

	TextAsset card_json = Resources.Load("JSON/cards") as TextAsset;
	var card_parse = JSON.Parse(card_json.text);
	
	for(int i = 0; i < 52; i++){

	    Card card =
		new Card(
			 card_parse["cards"][i]["card_shape"],
			 card_parse["cards"][i]["shape_value"],
			 card_parse["cards"][i]["shape_name"],
			 card_parse["cards"][i]["card_value"],
			 card_parse["cards"][i]["card_name"],
			 card_parse["cards"][i]["card_index"]
			 );

	    card_deck.Add(card);
	}

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

	    if(player.player_name == "Player"){
		card_inst.transform.tag = "player_card";
		card_inst.GetComponent<Card_Game_Object>().is_open = true;
	    }
	    else{
		card_inst.transform.tag = "com_card";
	    }

	    card_inst.GetComponent<Card_Game_Object>().card_owner = player;
	    card_inst.GetComponent<Card_Game_Object>().index_on_hand = i;

	    if(offset_x < 0.7f){
		card_inst.GetComponent<Card_Game_Object>().arrange_orientation = 1;
	    }

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

		start_index = curr_count;
		last_index = curr_count;
		CheckCardOnHand(player, curr_count + 1, curr_value); 
	    }
	    else{

		if(last_value == curr_value){

		    last_index = curr_count;
		    CheckCardOnHand(player, curr_count + 1, curr_value); 
		}
		else{ // cut here...

		    int card_set = last_index - start_index + 1;
		    player.SetCardSet(card_set, start_index, last_index);

		    start_index = curr_count;
		    last_index = curr_count;

		    CheckCardOnHand(player, curr_count + 1, curr_value); 

		}

	    }

	}
	else{

	    int card_set = last_index - start_index + 1;
	    player.SetCardSet(card_set, start_index, last_index);


	    start_index = 0;
	    last_index = 0;

	    // Debug.Log("total card set " + player.GetTotalCardSet());
	    // Debug.Log("single card set " + player.single_set_list.Count);
	    // Debug.Log("pair card set " + player.pair_set_list.Count);
	    // Debug.Log("triple card set " + player.triple_set_list.Count);
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

		IsOnSet(max_count, curr_count + 1, curr_value);

	    }
	    else{

		if(last_value == curr_value){
		    IsOnSet(max_count, curr_count + 1, curr_value);
		}

	    }
	}  
	else{

	     if(last_value == curr_value){
		is_on_set = true;
	     }
	     else{
		is_on_set = false;
	     }
	}

    }


    void NextTurn(){
	if(turn_state < 3){
	    turn_state++;
	}
	else{
	    turn_state = 0;
	}
    }

    void RemoveCardOnHand(Player player, int selected_count){ // removing player card on hand

	GameObject deck = null;

	if(player.player_tag == "p1"){
	    deck = p1_deck;
	}
	else if(player.player_tag == "p2"){
	    deck = p2_deck;
	}
	else if(player.player_tag == "p3"){
	    deck = p3_deck;
	}
	else if(player.player_tag == "p4"){
	    deck = p4_deck;
	}

	// removing player card on hand
	for(int i = 0; i < selected_count; i++){
	    player_selected_card_list[i].transform.parent = point.transform; // set parent to point. when we dont need this card object, we just destroy all point child 
	    Card_Game_Object temp_card = player_selected_card_list[i].GetComponent<Card_Game_Object>();
	    // temp_card.card_owner.card_on_hand.RemoveAt(temp_card.index_on_hand); // removing player card on hand
	    player.card_on_hand.RemoveAt(temp_card.index_on_hand); // removing player card on hand

	    // reset card game object index on hand
	    for(int j = 0; j < deck.transform.childCount; j++){
		deck.transform.GetChild(j).GetComponent<Card_Game_Object>().index_on_hand = j;
	    }

	}
    }

    // event handler on mouse click
    public void SubmitCard(){

	int selected_count = player_selected_card_list.Count;
	int curr_card_value = 0;

	if(selected_count > 0){

	    // - check card set here...
	    // - set curr card value here...
	    if(selected_count == 1){
		Debug.Log("single");

		curr_card_value =
		    player_selected_card_list[0].GetComponent<Card_Game_Object>().card_data.card_value +
		    player_selected_card_list[0].GetComponent<Card_Game_Object>().card_data.shape_value;

		set_state = (int)SetState.SINGLE;
		set_panel.Show();
		is_on_set = true;
	    }
	    else if(selected_count == 2){
		IsOnSet(2);
		if(is_on_set){
		    Debug.Log("pair");

		    curr_card_value =
			player_selected_card_list[0].GetComponent<Card_Game_Object>().card_data.card_value;

		    set_state = (int)SetState.PAIR;
		    set_panel.Show(2);
		}
		else{
		    Debug.Log("just nothing...");
		}
	    }
	    else if(selected_count == 3){
		IsOnSet(3);
		if(is_on_set){
		    Debug.Log("triples");

		    curr_card_value =
			player_selected_card_list[0].GetComponent<Card_Game_Object>().card_data.card_value;

		    set_state = (int)SetState.TRIPLE;
		    set_panel.Show(3);
		}
		else{
		    Debug.Log("just nothing...");
		}
	    }
	    else{
		is_on_set = false;
	    }
 

	    /*
	      NOTE [Kevin]:

	      - check if card on set (single, pair, triple, etc)
	      - check if this turn is first run (first card throw/submit)
	      - if first run/throw/submit, set card value on table
	      - run next turn until we find player with higher card set value
	      
	      - player can't submit card if card value < card value on table

	    */


	    if(is_on_set &&
	       curr_card_value > card_value_on_table // check if player card value > card value on table
	       ){ 
	    
		// check first run here...
		if(is_first_run){
		    card_value_on_table = curr_card_value;
		    is_first_run = false;
		} // is first run
		
		if(selected_count == 1){
		    player_selected_card_list[0].
			GetComponent<Card_Game_Object>().is_submited = true;

		    player_selected_card_list[0].transform.position =
			point.transform.position;
		}
		else{
		    for(int i = 0; i < selected_count; i++){

			player_selected_card_list[i].GetComponent<SpriteRenderer>().sortingOrder = i;

			player_selected_card_list[i].
			    GetComponent<Card_Game_Object>().is_submited = true;
		    
			if(i > 0){

			    player_selected_card_list[i].transform.position =
				new Vector3(player_selected_card_list[i - 1].transform.position.x + 1.0f,
					    player_selected_card_list[i - 1].transform.position.y,
					    player_selected_card_list[i - 1].transform.position.z
					    );

			}
			else{
			    player_selected_card_list[i].transform.position =
				// point.transform.position;
				new Vector3(point.transform.position.x - ( (1.18f * selected_count) / 2 ),
					    point.transform.position.y,
					    point.transform.position.z);
			}

		    }
		} // submited card position

		RemoveCardOnHand(player_turn, selected_count);
	    
		// // remove testing
		// for(int i = 0; i < point.transform.childCount; i++){
		// 	Destroy(point.transform.GetChild(i).gameObject);
		// }
		// player_selected_card_list.Clear();
		// // remove testing

		player_selected_card_list.Clear();
		NextTurn();

	    } // is on set

	} // selected count > 0



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

	p1 = new Player("p1", "Player", photo_str);
	p2 = new Player("p2", "COM1", "joe");
	p3 = new Player("p3", "COM2", "seto");
	p4 = new Player("p4", "COM3", "ryo");

    }
    
    void Start(){

	set_panel = GameObject.Find("Canvas/GUI Panel/SetPanel").GetComponent<SetPanel>();

	card_game_object = GameObject.Find("CardGameObject");
	p1_deck = GameObject.Find("Player1Deck");
	p2_deck = GameObject.Find("Player2Deck");
	p3_deck = GameObject.Find("Player3Deck");
	p4_deck = GameObject.Find("Player4Deck");
	point = GameObject.Find("Point");

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

	// init first player turn
	turn_state = (int)TurnState.P1;
	player_turn = p1;
    }

    void Update(){

	Debug.Log("turn state:" + turn_state);

	switch(turn_state){
	    case (int)TurnState.P1:
		player_turn = p1;
		break;
	    case (int)TurnState.P2:
		player_turn = p2;
		break;
	    case (int)TurnState.P3:
		player_turn = p3;
		break;
	    case (int)TurnState.P4:
		player_turn = p4;
		break;
	}
    }
}
