using UnityEngine;
using UnityEngine.UI;

public class UnlockFigure : MonoBehaviour
{
    [SerializeField]
    int costToBuy;

    [SerializeField]
    int myUnlockSprite;

    Button myButton;

    [SerializeField]
    AudioClip audioClip;

    AudioSource audioPlayer;

    Animator anim;

    Image img;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(UnlockNewFigure);

        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            audioPlayer.clip = audioClip;

            // Sets sprite to the last chosen one
            img.sprite = GameController.gameController.playerSprites[myUnlockSprite];
        }

    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            if (PlayerPrefs.GetInt("ActualPlayer") == myUnlockSprite)
            {
                Debug.Log("Ich wurde ausgewählt" + gameObject.name);
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
            }
        }
    }

    void UnlockNewFigure()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 0)
        {
            Debug.Log("Item ist noch verschlossen");
            if (GameController.gameController.coinPoints >= costToBuy)
            {
                if (PlayerPrefs.GetInt("MusicOn") == 1)
                {
                    audioPlayer.Play();
                }
                GameController.gameController.coinPoints -= costToBuy;
                PlayerPrefs.SetInt("Coin", GameController.gameController.coinPoints);
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite = GameController.gameController.playerSprites[myUnlockSprite];
                PlayerPrefs.SetInt("ActualPlayer", myUnlockSprite);
                PlayerPrefs.SetInt(gameObject.name, 1);
                anim.SetTrigger("Unlock");
                
            }
            else
            {
                audioPlayer.clip = audioClip;
                if (PlayerPrefs.GetInt("MusicOn") == 1)
                {
                    audioPlayer.Play();
                }
            }
        }
        else
        {
            audioPlayer.clip = audioClip;
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                audioPlayer.Play();
            }
            //Set Sprite to Player
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite = GameController.gameController.playerSprites[myUnlockSprite];
            PlayerPrefs.SetInt("ActualPlayer", myUnlockSprite);
            PlayerPrefs.SetInt(gameObject.name, 1);
          

        }
    }

    void Unlock()
    {
        GetComponent<Image>().sprite = GameController.gameController.playerSprites[myUnlockSprite];
    }

}
