using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {
    public GameObject leaderboardCross;
    public AudioSource clickSound;
    public GameObject highscoreText;
    public GameObject scoreText;
    public AudioSource deathSound;
    private int score;

    // Use this for initialization
    void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();


        highscoreText.GetComponent<Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore").ToString();
        login();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                leaderboardCross.SetActive(false);
            }
        });
    }
    
    public void LeaderboardPress()
    {
        clickSound.Play();
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else {
            login();
        }
    }

    public void SentToLeaderboard(int score)
    {
        if(Social.localUser.authenticated)
        {
            Social.ReportScore(score, "CgkIkvWNjaoWEAIQAQ", (bool success) =>
            {
                if(success)
                {
                }
            });

        }
    }
    
    public void addScore()
    {
        score++;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
    public void endGame()
    {
        deathSound.Play();
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            SentToLeaderboard(score);
            PlayerPrefs.SetInt("highscore", score);
        }
        highscoreText.GetComponent<Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore").ToString();
        PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
        if(PlayerPrefs.GetInt("gamesPlayed") % 5 == 0)
        {
            showAd();
        }
        score = 0;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
    public void showAd()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}
