﻿using UnityEngine;

public class EnemyGetSmallState : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("GetSmall");
    }

}
