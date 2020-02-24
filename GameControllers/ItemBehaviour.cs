using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;

    private void Awake()
    {
       if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            GetComponent<AudioSource>().Play();
        }
        
        // Instantiate particle effect
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
