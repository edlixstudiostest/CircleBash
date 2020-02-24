using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSound : MonoBehaviour
{

   // Button myButton;

    [SerializeField]
    Sprite[] musicSprites;

    public static DisableSound disableSound;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("MusicOn", 1);
        GetComponent<Image>().sprite = musicSprites[PlayerPrefs.GetInt("MusicOn")];


    }

    public void HandleMusicButton()
    {
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            PlayerPrefs.SetInt("MusicOn", 0);
            GetComponent<Image>().sprite = musicSprites[PlayerPrefs.GetInt("MusicOn")];
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            Debug.Log(PlayerPrefs.GetInt("MusicOn"));
        }
        else
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("MusicOn", 1);
            GetComponent<Image>().sprite = musicSprites[PlayerPrefs.GetInt("MusicOn")];
           
        }
    }
}
