using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;

    private const string SDK_KEY = "833lZCpTOf2QAhTISWIW5RrTsJBRfol_4IprU5Xdki2eHfaXDmZUG7dwgV8yPhoUJr-6ugVLBqbi57qcXP4IGo";

    public const int DEATHS_TILL_AD = 3;

    public int deaths;

    void Awake()
    {
        MakeSingleton();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        deaths = 0;
        //AppLovin.SetSdkKey(SDK_KEY);
        AppLovin.InitializeSdk();
        AppLovin.SetUnityAdListener(this.gameObject.name);
        StartCoroutine(CallAds());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        //int random = Random.Range(0, 10);
        //if (random > 5)
        //{
        //    ShowInterstitial();
        //}
        //else
        //{
        //    ShowVideo();
        //}
           
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

  

    IEnumerator CallAds()
    {
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(3f));
        LoadInterstitial();
        //LoadVideo();
        //AppLovin.ShowAd(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_TOP);
    }

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadInterstitial()
    {
        AppLovin.PreloadInterstitial();
    }

    public void ShowInterstitial()
    {
        if (AppLovin.HasPreloadedInterstitial())
        {
            AppLovin.ShowInterstitial();
        }
        else
        {
            LoadInterstitial();
        }
    }

    public void LoadVideo()
    {
        AppLovin.LoadRewardedInterstitial();
    }

    public void ShowVideo()
    {
        AppLovin.ShowRewardedInterstitial();
    }


    void onAppLovinEventReceived(string ev)
    {
        if (ev.Contains("DISPLAYEDINTER"))
        {
            // An ad was shown.  Pause the game.
        }
        else if (ev.Contains("HIDDENINTER"))
        {
            // Ad ad was closed.  Resume the game.
            // If you're using PreloadInterstitial/HasPreloadedInterstitial, make a preload call here.
            AppLovin.PreloadInterstitial();
        }
        else if (ev.Contains("LOADEDINTER"))
        {
            // An interstitial ad was successfully loaded.

        }
        else if (string.Equals(ev, "LOADINTERFAILED"))
        {
            // An interstitial ad failed to load.
            LoadInterstitial();
        }
        //if (ev.Contains("REWARDAPPROVEDINFO"))
        //{

        //    // The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
        //    string delimeter = "|";

        //    // Split the string based on the delimeter
        //    string[] split = ev.Split(delimeter);

        //    // Pull out the currency amount
        //    double amount = double.Parse(split[1]);

        //    // Pull out the currency name
        //    string currencyName = split[2];

        //    // Do something with the values from above.  For example, grant the coins to the user.
        //    //updateBalance(amount, currencyName);
        //}
        else if (ev.Contains("LOADEDREWARDED"))
        {
            // A rewarded video was successfully loaded.
        }
        else if (ev.Contains("LOADREWARDEDFAILED"))
        {
            // A rewarded video failed to load.
            //LoadVideo();
        }
        else if (ev.Contains("HIDDENREWARDED"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            //AppLovin.LoadRewardedInterstitial();
            //LoadVideo();
        }
    }
}
