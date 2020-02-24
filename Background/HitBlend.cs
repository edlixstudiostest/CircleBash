using UnityEngine;

public class HitBlend : MonoBehaviour
{

    public static HitBlend hitBlend;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // Create Singelton
        if (hitBlend == null)
        {
            hitBlend = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Starts hit animation
    public void Blend()
    {
        anim.SetTrigger("Hit");
    }
}
