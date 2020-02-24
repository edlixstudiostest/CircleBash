using UnityEngine;
using UnityEngine.VFX;

public class VXEffect : MonoBehaviour
{
    VisualEffect effect;        // New gpu rendered particle effects
    ParticleSystem part;        // Old cpu rendered particle effects

    public static VXEffect vxEffect;

    private void Awake()
    {
        // Singelton of vxEffect
        if(vxEffect == null)
        {
            vxEffect = this;
        }
        else
        {
            Destroy(gameObject);
        }


        //   effect = GetComponent<VisualEffect>();
        //   effect.Stop();


        part = GetComponent<ParticleSystem>();
    }

    // Starts particle effect
    public void StartExplosion()
    {
        //  effect.Play();
        part.Play();
    }

    // Stops particle effect
    public void StopExplosion()
    {
      //  effect.Stop();

    }
}
