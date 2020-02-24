using UnityEngine;

public class ChangeBackround : MonoBehaviour
{

    public static ChangeBackround change;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // Create Singelton
        if (change == null)
        {
            change = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ChooseBackground(); 
    }


    // Choose randomly a background
    public void ChooseBackground()
    {
        
        int r = Random.Range(1, 4);
        anim.SetInteger("ChangeColor",r);
       
    }


 

}
