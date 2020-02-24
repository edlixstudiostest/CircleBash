using System.Collections;
using UnityEngine;

public class VFXItemEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
       yield return new WaitForSeconds(1f);
       Destroy(gameObject);
    }

}
