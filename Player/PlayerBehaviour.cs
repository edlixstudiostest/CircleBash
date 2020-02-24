using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{

    GameObject center;

    public float speed;

    bool unhittable = false;
    public bool canKill = true;

    Color col;
    
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        col = GetComponentInChildren<SpriteRenderer>().color;
        center = FindCenterObject();
        GetComponentInChildren<SpriteRenderer>().sprite = GameController.gameController.playerSprites[PlayerPrefs.GetInt("ActualPlayer")];
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameController.gameController.gameStates)
        {
            #region Play State
            case GameController.GameStates.Play:

                // Keyboard controll for debuging
                    if (Input.GetKey(KeyCode.Space))
                    {
                        transform.RotateAround(center.transform.position, Vector3.forward, speed);
                   
                }
                CantDie(unhittable);

                // Touch controll
                if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                            {
  
                            transform.RotateAround(center.transform.position, Vector3.forward, speed);
                 
                            }
                         CantDie(unhittable);
                    }
                
                break;
            #endregion
        }

    }



    #region Functions

    // Star powerup - player cannot die
    void CantDie(bool isActive)
    {
        if (isActive)
        {
            canKill = false;
            Color newColor = GetComponentInChildren<SpriteRenderer>().color;
            newColor.r = Random.Range(0f, 1f);
            newColor.g = Random.Range(0f, 1f);
            newColor.b = Random.Range(0f, 1f);
            GetComponentInChildren<SpriteRenderer>().color = newColor;
            
        }
        else
        {
            canKill = true;
        }
    }

    // Set center gameobject
    GameObject FindCenterObject()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Center");

        if (g != null)
        {
            return g;
        }
        else
        {
            Debug.Log("No Object there!");
            return null;
        }
    }

    // Set's the player speed
    float Multiplikator()
    {
        float enemySpeed = Mathf.Sign(GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyBehaviour>().speed);
        return Time.fixedDeltaTime * 100 * enemySpeed;
    }

    // Set Random Value
    public float Direction(float EnemyDir)
    {
        return 1.8f * Time.fixedDeltaTime * 100 * Mathf.Sign(EnemyDir);
    }
  
    // Timer starts when player becomes unhittable
    IEnumerator Unhittable()
    {
        unhittable = true;
        yield return new WaitForSeconds(5f);
        unhittable = false;
        GetComponentInChildren<SpriteRenderer>().color = col;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Powerups and traps behaviour
        if (collision.gameObject.tag == "Item")
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                audio.Play();
            }
            switch (collision.gameObject.name)
            {
                case "TurnDirection":
                    speed *= -1;
                    Destroy(collision.gameObject);
                    break;
                case "MoveCamera":Destroy(collision.gameObject);
                    Camera.main.GetComponent<Animator>().SetTrigger("Reverse");
                    break;
                case "PointsTimesTwo":
                    Destroy(collision.gameObject);
                    Points.points.doublePoints = true;
                    break;
                case "Unhittable":
                    Destroy(collision.gameObject);
                    StartCoroutine(Unhittable());
                    break;
                case "SlowMotion":
                    GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyBehaviour>().slowDown = true;
                    Destroy(collision.gameObject);
                    break;
                    default:
                    Debug.Log("Item was there");
                    break;
            }
         
        }
    }
    #endregion
}
