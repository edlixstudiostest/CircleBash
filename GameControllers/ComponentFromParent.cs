using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentFromParent : MonoBehaviour
{

    public Animator anim;
    public int animState;

    public bool canHit;

    // Start is called before the first frame update
    void Start()
    {
        canHit = true;
        animState = 0;
        anim = GetComponent<Animator>();
        Debug.Log("Animator ist daaaaa");
    }

    void PointUp()
    {
        Points.points.IncreasePointsArrowMode();
    }

    void DeadAction()
    {

        Destroy(transform.parent.gameObject);
    }



}
