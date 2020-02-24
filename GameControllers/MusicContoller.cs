using UnityEngine;

public class MusicContoller : MonoBehaviour
{

    AudioSource audio;
    static MusicContoller mController;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {

            audio.Play();
        }
        else
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }

        // Singelton
        if (mController != null)
        {
            Destroy(gameObject);

        }
        else
        {
            mController = this;
            DontDestroyOnLoad(gameObject);
        }


    }

}
