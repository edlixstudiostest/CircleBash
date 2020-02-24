using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToChosenGameMode : MonoBehaviour
{
    Button btn;

    [SerializeField]
    string roomName;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(GoToRoom);
    }


    void GoToRoom()
    {
        SceneManager.LoadScene(roomName);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
