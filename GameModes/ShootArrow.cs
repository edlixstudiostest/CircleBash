using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public bool stopMoving;

    Rigidbody2D rb;

    [SerializeField]
    Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stopMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMoving)  return;
            transform.localPosition += Vector3.right * .3f;
    }


    // Disables the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center")
        {
            GetComponentInChildren<CircleCollider2D>().enabled = false;
        }

    }



}
