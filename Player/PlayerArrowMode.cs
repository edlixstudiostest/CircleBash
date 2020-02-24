using UnityEngine;
using TMPro;

public class PlayerArrowMode : MonoBehaviour
{
    GameObject center;
    
    Color col;
    bool canKill = true;

    [SerializeField]
    LayerMask layerMask;

    public float dis;
    public float speed;

    Rigidbody2D rb;

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        //   audio = GetComponent<AudioSource>();
        rb = GetComponentInChildren<Rigidbody2D>();
        col = GetComponentInChildren<SpriteRenderer>().color;
        center = FindCenterObject();
    }

    // Update is called once per frame
    void Update()
    {
         RaycastHit2D rayRight = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - .2f, transform.position.z), transform.TransformDirection(Vector3.right), dis, layerMask);
         RaycastHit2D rayLeft = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - .2f, transform.position.z), transform.TransformDirection(Vector3.left), dis, layerMask);

         DetectCollisionWithArrow(rayLeft);
         DetectCollisionWithArrow(rayRight);

        // Keyboard controll for debuging
        if (Input.GetKey(KeyCode.Space))
        {
            transform.RotateAround(center.transform.position, Vector3.forward, Direction());
        }

        // Touch controll
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                transform.RotateAround(center.transform.position, Vector3.forward, Direction());
            }
        }
    }



    #region Functions

    // Behaviour on collision with Arrow
    void DetectCollisionWithArrow(RaycastHit2D raycast)
    {
        Animator arrow = raycast.transform.gameObject.GetComponent<ComponentFromParent>().anim;
        ComponentFromParent cP = raycast.transform.gameObject.GetComponent<ComponentFromParent>();
        if (arrow != null)
        {
            speed *= -1;
            if (cP.canHit)
            {
                cP.canHit = false;
                raycast.transform.gameObject.GetComponent<ComponentFromParent>().animState++;
                arrow.SetInteger("Hit", raycast.transform.gameObject.GetComponentInParent<ComponentFromParent>().animState);
                if (raycast.transform.gameObject.GetComponent<ComponentFromParent>().animState > 3)
                {
                    Destroy(raycast.transform.gameObject);
                }
            }
        }
    }

    // Set Center Gameobject
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



   // Set Random Value
   public float Direction()
   {
       return speed * Time.fixedDeltaTime * 100;
   }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Arrow")
        {
            if (canKill)
            {
                canKill = false;
                GetComponentInChildren<CircleCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                GameObject.FindGameObjectWithTag("Continue").GetComponent<Animator>().SetTrigger("GameOver");
                //HitBlendMethod();
                // GameController.gameController.gameStates = GameController.GameStates.Dead;
                GameObject.FindGameObjectWithTag("GameRoom").GetComponent<Animator>().SetTrigger("Dead");
              
            }
        }
    }
    #endregion
}
