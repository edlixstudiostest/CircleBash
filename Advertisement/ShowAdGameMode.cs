using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


[RequireComponent(typeof(Button))]
public class ShowAdGameMode : MonoBehaviour, IUnityAdsListener
{
    Animator continuePanel;

#if UNITY_ANDROID
    private string gameId = "3449448";
#elif UNITY_IOS
    private string gameId = "3449449";
#endif
    Button myButton;
    public string myPlacementId = "rewardedVideo";

    bool doublePoint;

    void Start()
    {
        doublePoint = false;
        continuePanel = GameObject.FindGameObjectWithTag("Continue").GetComponent<Animator>();
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        if (!doublePoint)
        {
            doublePoint = true;
            Points.points.pointCounter *= 2;
            int myPoints = Points.points.pointCounter;
            int newCoinValue = PlayerPrefs.GetInt("Coin", 0) + myPoints;
            PlayerPrefs.SetInt("Coin", newCoinValue);
        }

        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (continuePanel == null)
            {
                continuePanel = GameObject.FindGameObjectWithTag("Continue").GetComponent<Animator>();
            }
            else
            {


                continuePanel.SetTrigger("End");
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        }
        else if (showResult == ShowResult.Skipped)
        {
            continuePanel.SetTrigger("End");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
