using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    void Start()
    {
        //Create dictionary of high scores by gamertag
        Dictionary<string,int> highScores=new Dictionary<string, int>();
        highScores.Add("Joe",100);
        highScores.Add("Bob",9001);
        highScores.Add("JoeBob",1);
        
        //Prompt for and get gamertag from user
        print("Enter gamertag: ");
        string gamertag = "Joe";
        
        //Print high score or error message for gamertag
        if (highScores.ContainsKey(gamertag))
        {
            print("High score for "+gamertag+
                              " is "+highScores[gamertag]);   
        }
        else
        {
            print("No high score for "+gamertag);
        }
    }
}
