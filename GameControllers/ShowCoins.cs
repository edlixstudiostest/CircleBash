using UnityEngine;
using TMPro;

public class ShowCoins : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        text.text = "x " + GameController.gameController.coinPoints;
    }
}
