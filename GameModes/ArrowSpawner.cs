using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
public class ArrowSpawner : MonoBehaviour
{

    float speed;
    float enemySpeed;
    float time;

    bool slowDown;

    public GameObject prefab;
    public Transform spawnPosition;

    [SerializeField]
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        slowDown = false;
        enemySpeed = ChooseDirection(speed);
    }

    // Update is called once per frame
    void Update()
    {

        // Restarts the gamescene for debuging
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Arrow");
        }

        // If gamestate equals play run code
        if (ArrowModeController.arrowModeController.gameStates == ArrowModeController.GameStates.Play)
        {
            transform.RotateAround(Vector3.forward, enemySpeed);

            // Detects the player and start's shooting arrows
            RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 2, layerMask);

            if (ray)
            {
                if (!slowDown) slowDown = true;
            }

            if (slowDown)
            {
                SlowDownArrow();
            }
        }

    
    }

    IEnumerator ShootOnPlayer()
    {
        yield return new WaitForSeconds(.25f);

        // Instantiate Arrow Prefab
        var p = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        p.transform.rotation = transform.rotation;

        yield return new WaitForSeconds(.2f);
        slowDown = false;
        enemySpeed = ChooseDirection(speed);
    
    }

    // Slows down the arrow before shooting
    void SlowDownArrow()
    {
        if (Mathf.Abs(enemySpeed) > 0)
        {
            if (enemySpeed > 0)
            {
                enemySpeed -= .01f;
                
                if (enemySpeed <= 0)
                {
                    enemySpeed = 0;
                    StartCoroutine(ShootOnPlayer());
                    Debug.Log(time);
                }
            }
            else if (enemySpeed <0)
            {
                enemySpeed += .01f;

                if (enemySpeed >= 0)
                {
                    StartCoroutine(ShootOnPlayer());
                    Debug.Log(time);
                    enemySpeed = 0;
                }
            }
            
        }
    }

    // Choosed randomly direction to move
    float ChooseDirection(float spd)
    {
        float r = Random.Range(0f, 2f);

        float returnVal = 0;

        if (r > 1f)
        {
            returnVal =  1 * spd*Time.fixedDeltaTime;
        }
        else if (r <= 1f)
        {
            returnVal =  - 1 * spd * Time.fixedDeltaTime;
        }

        return returnVal;
    }

}
