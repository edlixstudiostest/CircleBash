using System.Collections;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    TMP_Text pointText;

    public static Points points;
    public bool doublePoints;
    bool doublePointsActive = false;

    public int pointCounter;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        doublePoints = false;
        pointText = GetComponent<TMP_Text>();
     
        // Singleton
        if (points == null)
        {
            points = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    #region GameModes

    // Increase points in arrowmode
    public void IncreasePointsArrowMode()
    {
        anim.SetTrigger("PointUp");
        pointCounter++;
        pointText.text = pointCounter.ToString();
    }

    #endregion


    // Increase points in normal mode
    public void IncreasePoint()
    {
        if (GameController.gameController.gameStates == GameController.GameStates.Play)
        {
            anim.SetTrigger("PointUp");
            if (doublePoints)
            {
                pointCounter += 2;
                GameController.gameController.coinPoints += 2;
                pointText.text = pointCounter.ToString();
                if (doublePointsActive)
                    return;
                doublePointsActive = true;
                StartCoroutine(TurnBackToSinglePoint());
            }
            else
            {
                GameController.gameController.coinPoints++;
                pointCounter++;
                pointText.text = pointCounter.ToString();
            }

        }

    }


    IEnumerator TurnBackToSinglePoint()
    {
        yield return new WaitForSeconds(5f);
        doublePointsActive = false;
        doublePoints = false;
    }

}
