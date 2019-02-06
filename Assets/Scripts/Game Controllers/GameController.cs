using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //reference to this class
    public static GameController instance;

    //string values that never change (used in player prefs)
    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";
    private const string GREEN_BIRD = "Green Bird";
    private const string RED_BIRD = "Red Bird";

    void Awake()
    {
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();
        //PlayerPrefs.DeleteAll();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	
	void MakeSingleton () {
        if (instance != null) //if there is already a GameController
        {
            //destroy the game object holding this script
            Destroy(gameObject);
        }
        else //if we don't have an instance
        {
            //make this class the instance
            instance = this;
            //don't destroy the game object holding this class
            DontDestroyOnLoad(gameObject);
        }
	}

    void IsTheGameStartedForTheFirstTime()
    {
        //first time game starts, will be true
        //"PlayerPrefs" allows us to store data in key-value pair
        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);      //high-score initially zero
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);   // 0-bluebird 1-greenbird 2-redbird
            PlayerPrefs.SetInt(GREEN_BIRD, 0);      // 0-locked 1-unlocked
            PlayerPrefs.SetInt(RED_BIRD, 0);        // 0-locked 1-unlocked
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);

        }
    }

    public void SetHighscore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedBird(int selectedBird)
    {
        PlayerPrefs.SetInt(SELECTED_BIRD, selectedBird);
    }

    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(GREEN_BIRD, 1); 
    }

    public int IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_BIRD);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD, 1);
    }

    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD);
    }

}
