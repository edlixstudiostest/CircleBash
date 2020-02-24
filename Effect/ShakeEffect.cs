using System.Collections;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public static ShakeEffect shakeEffect;

    Vector3 camPosition;

    bool shake = false;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if (shakeEffect == null)
        {
            shakeEffect = this;
        }
        else
        {
            Destroy(gameObject);
        }

        camPosition = transform.position;
    }


    private void Update()
    {
        if (shake) ShakeCam();
    }

    // Camera shake effect
    void ShakeCam()
    {
        float intensity = .4f;

        Vector3 cShake = transform.position;
        cShake.x = Random.Range(-intensity, intensity);
        cShake.y = Random.Range(-intensity, intensity);
        transform.position = cShake;

    }

    // Countdown for camera shake effect
    IEnumerator StartShake()
    {
        shake = true;
        yield return new WaitForSeconds(.3f);
        shake = false;
        transform.position = camPosition;
    }

    // Stars the camera shake effect
    public void StartAnimShake()
    {
        StartCoroutine(StartShake());
    }


}
