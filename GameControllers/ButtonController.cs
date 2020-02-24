using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public enum ButtonBehaviour {   GoToShop, RestartMode, ComeFromShop, GoToMusicControll, ComeFromMusicConttroll,
                                    BackFromContinue, RestartGame, BackToHome, BackFromContinueGameMode, GameModeMenue, Home };

    public ButtonBehaviour buttonBehaviour;

    Button myButton;
    Animator anim;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(StartAnimation);
    }

    // Animation on scenechange
    IEnumerator GoBack()
    {
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<Animator>().SetTrigger("SceneLoad");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }


    public void GoBackToMainMenue()
    {
        StartCoroutine(GoBack());
    }


    void StartAnimation()
    {
        anim.SetTrigger("StartAnimation");
    }
    

    void StartAction()
    {

        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            audio.Play();
        }

    
       switch (buttonBehaviour)
        {
            case ButtonBehaviour.GoToShop:
                GameObject.FindGameObjectWithTag("Main").GetComponent<Animator>().SetTrigger("OpenShop");
                break;
            case ButtonBehaviour.ComeFromShop:
                GameObject.FindGameObjectWithTag("Main").GetComponent<Animator>().SetTrigger("EndShop");
                break;
            case ButtonBehaviour.GoToMusicControll:
                GetComponent<DisableSound>().HandleMusicButton();
                break;
            case ButtonBehaviour.BackFromContinue:
                GameObject.FindGameObjectWithTag("Continue").GetComponent<BackFromContinue>().EndContinue();
                break;
            case ButtonBehaviour.RestartGame:
                GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().StartGameAgain();
                break;
            case ButtonBehaviour.BackToHome:
                ArrowModeController.arrowModeController.GoBackToMainMenue();
             
                break;
            case ButtonBehaviour.RestartMode:
                Debug.Log("Eigentlich müsste es klappen");
                ArrowModeController.arrowModeController.StartGameAgain();
                break;
            case ButtonBehaviour.BackFromContinueGameMode:
                GameObject.FindGameObjectWithTag("Continue").GetComponent<BackFromContinue>().EndContinue();
                int myPoints = Points.points.pointCounter;
                int newCoinValue = PlayerPrefs.GetInt("Coin", 0) + myPoints;
                PlayerPrefs.SetInt("Coin", newCoinValue);
                break;
            case ButtonBehaviour.GameModeMenue:
                GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().GoToGameModeMenue();
                break;
            case ButtonBehaviour.Home:
                GoBackToMainMenue();
                break;

        }
    }

    
}
