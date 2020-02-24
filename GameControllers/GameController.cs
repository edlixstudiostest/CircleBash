using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public enum GameStates { MainMenue, Play,Dead,GameEnd};
    public GameStates gameStates;

    public bool refreshedPoints;

    public int highscorePoints;
    public int coinPoints;


    TMP_Text highscoreText;
    TMP_Text coinText;

    public Sprite[] playerSprites;

    AudioSource audio;

    [SerializeField]
    bool setCoinsTo1000;

#if UNITY_ANDROID
    private string gameId = "3449448";
#elif UNITY_IOS
    private string gameId = "3449449";
#endif

    public string placementId = "Banner";


    private void Awake()
    {
        // Set framerate locked to 60 fps
        Application.targetFrameRate = 60;

        // Singelton
        if (gameController == null)
        {
            gameController = this;

        }
        else
        {
            Destroy(gameController);
        }


        // Load Advertisement
        Advertisement.Initialize(gameId,false);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        // Search coinPoints and highscorePoints in the memory, if they are null set value to 0
        coinPoints = PlayerPrefs.GetInt("Coin", 0);
        highscorePoints = PlayerPrefs.GetInt("Highscore", 0);

        // Set gameState to mainmenue
        gameStates = GameStates.MainMenue;

        // Get label component to represent highscore and coins
        highscoreText = GameObject.FindGameObjectWithTag("Highscore").GetComponent<TMP_Text>();
        coinText = GameObject.FindGameObjectWithTag("Coins").GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowBannerWhenReady());

        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
 
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (gameController.gameStates)
        {
            case GameStates.MainMenue:

                refreshedPoints = false;

                // Quits the game on back button (only available Android)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }

                // Starts the game (Debug)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.FindGameObjectWithTag("GameRoom").GetComponent<Animator>().SetTrigger("StartNormalMode");
                    gameController.gameStates = GameStates.Play;
                }

                break;

            case GameStates.Play:

                // Shows advertisement when banner is loaded
                if (Advertisement.IsReady(placementId))
                {
                    Advertisement.Banner.Show(placementId);
                }
                break;
            case GameStates.Dead:

                // On gameover show coins and highscore
                coinText.text = coinPoints.ToString();
                highscoreText.text = highscorePoints.ToString();
                break;
        }
    }

    // Animation for scenechange
    public void GoToGameModeMenue()
    {
        StartCoroutine(LoadingGameModes());
    }


    IEnumerator LoadingGameModes()
    {
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<Animator>().SetTrigger("SceneLoad");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameModes");
    }


    // Starting the game
    public void StartGame()
    {
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            audio.Play();
        }
        GameObject.FindGameObjectWithTag("GameRoom").GetComponent<Animator>().SetTrigger("StartNormalMode");
       
        gameController.gameStates = GameStates.Play;
        
    }


    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Restart game
    public void StartGameAgain()
    {
        PlayerPrefs.SetInt("Highscore", highscorePoints);
        PlayerPrefs.SetInt("Coin", coinPoints);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<Animator>().SetTrigger("SceneLoad");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coin", coinPoints);
        PlayerPrefs.SetInt("Highscore", highscorePoints);
    }
}
