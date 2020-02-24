using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChooseGameMode : MonoBehaviour
{

    Button myButton;
    string goToRoom;
    Animator sceneAnim;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(GoToGameMode);
        sceneAnim = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<Animator>();
    }


    void GoToGameMode()
    {
        anim.SetTrigger("StartAnimation");
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
          //  audio.Play();
        }
        switch (goToRoom)
        {
            case "ArrowMode":   RoomTransition("Arrow");
                break;
            case "ChangeMode":
                RoomTransition("Change");
                break;
        }
    }

    void RoomTransition(string sceneName)
    {

        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        sceneAnim.SetTrigger("SceneLoad");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        goToRoom = collision.gameObject.name;
    }
}
