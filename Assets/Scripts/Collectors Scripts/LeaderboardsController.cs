using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
//using System.Threading.Tasks;

public class LeaderboardsController : MonoBehaviour {

    public static LeaderboardsController instance;

    private const string LEADERBOARDS_SCORE = "CgkIn6aa-LAPEAIQBg";

    void Awake()
    {
        MakeSingleton();
        //Social.ShowLeaderboardUI();
        //Social.ShowAchievementsUI();
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    // Use this for initialization
    void Start()
    {
       PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       .Build();

       // PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        ReportScore(GameController.instance.GetHighscore());
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void MakeSingleton () {
		if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}

    public void ConnectOrDisconnectOnGooglePlayGames()
    {
        if (Social.localUser.authenticated) //already logged in
        {
            Debug.Log("Already logged in");
            PlayGamesPlatform.Instance.SignOut();
        }
        else
        {
            Debug.Log("Not logged in 1");
            
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Authenticated");
                }
                else
                {
                    Debug.Log("Authentication Failed");
                }
            });
        }
    }
    public void OpenLeaderboardsScore()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARDS_SCORE);
        }
     
    }

    void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, LEADERBOARDS_SCORE, (bool success) =>
              {

              });
        }
        else
        {
            Debug.Log("Not logged in 2");
        }
    }



}
