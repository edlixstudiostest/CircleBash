using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class ArrowModeController : MonoBehaviour
{

    public static ArrowModeController arrowModeController;
    public enum GameStates { Wait, Play, Dead};
    public GameStates gameStates;



#if UNITY_ANDROID
    private string gameId = "3449448";
#elif UNITY_IOS
    private string gameId = "3449449";
#endif

    public string placementId = "Banner";


    private void Awake()
    {
        // Set the application framerate locked to 60 fps
        Application.targetFrameRate = 60;

        // Singelton
        if (arrowModeController == null)
        {
            arrowModeController = this;

        }
        else
        {
            Destroy(gameObject);
        }
        
        // Initialize Advertisement
        Advertisement.Initialize(gameId, false);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        
        // GameState set to wait
        gameStates = GameStates.Wait;

    }
    // Start is called before the first frame update
    void Start()
    {
        // Starts to load advertisement banner
        StartCoroutine(ShowBannerWhenReady());
    }

    // Update is called once per frame
    void Update()
    {
        switch (arrowModeController.gameStates)
        {
            case GameStates.Wait:

                // Keyboard controll for debuging
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject.FindGameObjectWithTag("Info").GetComponent<Animator>().SetTrigger("Start");
                    arrowModeController.gameStates = GameStates.Play;
                    
                }

                // Touch controll
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        GameObject.FindGameObjectWithTag("Info").GetComponent<Animator>().SetTrigger("Start");
                        arrowModeController.gameStates = GameStates.Play;

                    }

                }

                break;
            case GameStates.Play:

                // Show advertisement banner
                if (Advertisement.IsReady(placementId))
                {
                    Advertisement.Banner.Show(placementId);
                }

                break;
        }


    }

    public void StartGame()
    {

        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            GetComponent<AudioSource>().Play();
        }
        GameObject.FindGameObjectWithTag("GameRoom").GetComponent<Animator>().SetTrigger("StartNormalMode");

        arrowModeController.gameStates = GameStates.Play;

    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Animation on scenechange
    public void GoBackToMainMenue()
    {
        StartCoroutine(LoadLevel("MainScene"));
    }

    // Animation on scenechange
    public void StartGameAgain()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevel(string activeScene)
    {
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<Animator>().SetTrigger("SceneLoad");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(activeScene);
    }

}
