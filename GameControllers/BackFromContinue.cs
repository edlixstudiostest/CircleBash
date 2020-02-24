using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFromContinue : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EndContinue()
    {
        anim.SetTrigger("End");
    }
}
