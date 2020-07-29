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
    void Start()
    {
	Screen.SetResolution(1280, 720, false); 
    }

    void Update(){}
}
