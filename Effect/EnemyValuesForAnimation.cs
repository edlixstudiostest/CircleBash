using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesForAnimation : MonoBehaviour
{
    void StartExplosion()
    {
      ShakeEffect.shakeEffect.StartAnimShake();
      VXEffect.vxEffect.StartExplosion();
    }

    void StopExplosion()
    {
        VXEffect.vxEffect.StopExplosion();
    }

}
