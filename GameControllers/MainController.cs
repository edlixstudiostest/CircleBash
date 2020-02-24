using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public static MainController mainController;

    private void Awake()
    {
        // Get the actual sprite of player
        PlayerPrefs.GetInt("ActualPlayer", 0);
        InitializeFigures();
    }

    void InitializeFigures()
    {
        // Initialize all available sprites
        GameObject[] FigureToBuy = GameObject.FindGameObjectsWithTag("BuyPlayer");

        // Get value of all sprites 
        for (int i = 0; i < FigureToBuy.Length; i++)
        {
            // On default set all to locked
            PlayerPrefs.GetInt(FigureToBuy[i].name, 0);
        }

        // Normal sprite is from beginning available
        if (PlayerPrefs.GetInt("Normal") == 0)
        PlayerPrefs.SetInt("Normal", 1);
    }


    // Show animation to open shop
    public void OpenShop()
    {
        GameObject.FindGameObjectWithTag("Main").GetComponent<Animator>().SetTrigger("OpenShop");
    }

    // Came back from shop
    public void BackFromShop()
    {
        GameObject.FindGameObjectWithTag("Main").GetComponent<Animator>().SetTrigger("EndShop");
    }





}
