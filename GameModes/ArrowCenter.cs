using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowHit")
        {
            ShootArrow arrow = collision.transform.GetComponent<ShootArrow>();

            if (arrow != null)
            {
                arrow.stopMoving = true;
            }
            else
            {
                Debug.Log("Error on shoot - no object found");
            }
        }
    }
}
