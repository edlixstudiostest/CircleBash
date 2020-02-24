using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    GameObject[] HexagonChilds;
    GameObject activeChild;

    Animator anim;
    AudioSource audio;

    public float speed;
    
    public bool slowDown;
    bool gameStarded;

    public static bool canRepeat;

    [SerializeField]
    AudioClip[] audioClips;

    private void Awake()
    {
        HexagonChilds = GameObject.FindGameObjectsWithTag("Polygon");   // Find gameobjects with the tag polygon
    }

    // Start is called before the first frame update
    void Start()
    {
        canRepeat = true;                       // Player can repeat the game
        audio = GetComponent<AudioSource>();    
        ChoosePolygon();                 // Choose randomly the enemies polygon
        gameStarded = false;   
        slowDown = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, speed);           // Rotates the polygon around his z-axis

        if (GameController.gameController.gameStates != GameController.GameStates.Play)  return;

        if (!gameStarded) 
        {
            StartCoroutine(ShrinkTimer()); 
            gameStarded = true; 
        }

        // Slows down the polygon befor shrink
        if (slowDown)
        {
            if (speed > 0)
            {
                speed -= 0.05f * Multiplikator();
                if (speed < 0)
                {
                    speed = 0;
                    Shrink();
                }    
            }else if (speed <= 0)
            {
                speed += 0.05f * Multiplikator();
                if (speed > 0)
                {
                    speed = 0;
                    Shrink();
                }
            }
        }


    }


    // Changes the backround over the animator timeline
    void ChangeBg()
    {
        ChangeBackround.change.ChooseBackground();
    }

    // Plays blend animation when player get's hit
    void HitBlendMethod()
    {
        HitBlend.hitBlend.Blend();
    }

    // Randomly timer when enemie should shrink
    IEnumerator ShrinkTimer()
    {
            yield return new WaitForSeconds(RandomTimerValue());
            slowDown = true;
    }

    // Starts the shrink animation
    void Shrink()
    {
        if (GameController.gameController.gameStates != GameController.GameStates.Play)  return;
        slowDown = false;
        anim.SetTrigger("GetSmall");
    }

    // Returns a random number between 3 and 4
    float RandomTimerValue()
    {
        return Random.Range(3, 5);
    }


    // Choose randomly a polygon after shrink
    void ChoosePolygon()
    {
        // If points less then 3 the standard polygon appears
        if (Points.points.pointCounter < 3)
        {
            SetPolygonValues(HexagonChilds, true);
        }
        else
        {
            SetPolygonValues(HexagonChilds,false);
        }

        // If gamestate is not play the enemie stops to shrink
        if (GameController.gameController.gameStates != GameController.GameStates.Play) return;
        StartCoroutine(ShrinkTimer());
    }

    // Set the values to choose the right polygon
    void SetPolygonValues(GameObject[] hexagons, bool atStart)
    {
        for (int i = 0; i < hexagons.Length; i++)
        {
            hexagons[i].SetActive(false);
        }

        int r = Random.Range(0, hexagons.Length);
        speed = ChooseDirection();
        InputForPlayerGameObject();

        if (atStart)
        {
            activeChild = hexagons[0];
            hexagons[0].SetActive(true);
        }
        else
        {
            activeChild = hexagons[r];
            hexagons[r].SetActive(true);
        }
        
    }

    // Turns the player in the same direction as the polygon
    void InputForPlayerGameObject()
    {
        PlayerBehaviour p = GameObject.FindGameObjectWithTag("PlayerParent").GetComponent<PlayerBehaviour>();
        if (p != null)
        {
            p.speed = p.Direction(speed);
        }
        else
        {
            Debug.Log("Something went wrong");
        }
    }


    // Enables the colliders over the animator timeline
    void EnableCollider()
    {
        EdgeCollider2D[] coll = activeChild.GetComponentsInChildren<EdgeCollider2D>();

        foreach (var col in coll)
        {
            col.enabled = true;
        }
    }

    // Disables the colliders over the animator timeline
    void DisableCollider()
    {
       
        EdgeCollider2D[] coll = activeChild.GetComponentsInChildren<EdgeCollider2D>();

        foreach (var col in coll)
        {
            col.enabled = false;
        }

        if (GameController.gameController.gameStates == GameController.GameStates.Play)
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                audio.clip = audioClips[1];
                audio.volume = 0.3f;
                audio.Play();
            }
        }

    }

    // Chooese randomly the direction to rotate
    float ChooseDirection()
    {
        float dir = Random.Range(0, 2);

        if (dir >= 1)
        {
            return 1f* Multiplikator();
        }
        else
        {
            return -1f * Multiplikator();
        }
    }

    // Increase points
    void GetPoint()
    {
        Points.points.IncreasePoint();

        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            audio.clip = audioClips[0];
            audio.volume = 1f;
            audio.Play();
        }
    }

    // Multiplicates the speed in the certain direction
    float Multiplikator()
    {
        return Time.fixedDeltaTime * 100;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerBehaviour player = collision.gameObject.GetComponentInParent<PlayerBehaviour>();
            if (player == null)
            {
                Debug.Log("There is no Player!");
            }
            else
            {
                if (player.canKill) {

                    if (Points.points.pointCounter > GameController.gameController.highscorePoints)
                    {
                        GameController.gameController.highscorePoints = Points.points.pointCounter;
                        PlayerPrefs.SetInt("Highscore", Points.points.pointCounter);
                    }

                   slowDown = false;

                    if (canRepeat)
                    {
                        GameObject.FindGameObjectWithTag("Continue").GetComponent<Animator>().SetTrigger("GameOver");
                    }
                    
                    HitBlendMethod();
                    GameController.gameController.gameStates = GameController.GameStates.Dead;
                    GameObject.FindGameObjectWithTag("GameRoom").GetComponent<Animator>().SetTrigger("Dead");
                    collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                }
            }
           
        }
    }



}
