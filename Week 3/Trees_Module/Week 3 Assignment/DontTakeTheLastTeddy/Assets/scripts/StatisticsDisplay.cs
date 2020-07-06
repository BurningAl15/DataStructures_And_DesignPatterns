using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the statistics
/// </summary>
public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField]
    Text easyEasyPlayer1Wins;
    [SerializeField]
    Text easyEasyPlayer2Wins;

    [SerializeField]
    Text mediumMediumPlayer1Wins;
    [SerializeField]
    Text mediumMediumPlayer2Wins;

    [SerializeField]
    Text hardHardPlayer1Wins;
    [SerializeField]
    Text hardHardPlayer2Wins;

    [SerializeField]
    Text easyMediumPlayer1Wins;
    [SerializeField]
    Text easyMediumPlayer2Wins;

    [SerializeField]
    Text easyHardPlayer1Wins;
    [SerializeField]
    Text easyHardPlayer2Wins;

    [SerializeField]
    Text mediumHardPlayer1Wins;
    [SerializeField]
    Text mediumHardPlayer2Wins;

    /// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
    {
	    easyEasyPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezez + "1");
	    easyEasyPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezez + "2");
	    
	    mediumMediumPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.mdmd + "1");
	    mediumMediumPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.mdmd + "2");
	    
	    hardHardPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.hdhd + "1");
	    hardHardPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.hdhd + "2");

	    easyMediumPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezmd + "1");
	    easyMediumPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezmd + "2");

	    easyHardPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezhd + "1");
	    easyHardPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.ezhd + "2");
	    
	    mediumHardPlayer1Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.mdhd + "1");
	    mediumHardPlayer2Wins.text =""+ PlayerPrefs.GetInt(PlayerPrefsInitials.mdhd + "2");
    }
}
