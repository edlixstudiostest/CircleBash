using System.Collections;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{

    GameObject[] itemsPosition;
    [SerializeField]
    GameObject[] prefabItems;

    // Start is called before the first frame update
    void Start()
    {
        // Search for spawnpoints
        itemsPosition = GameObject.FindGameObjectsWithTag("SpawnPoint");
        StartCoroutine(SpawnItem());
    }

    // Spawn item after random time
    IEnumerator SpawnItem()
    {
        StopCoroutine(SpawnItem());
        float r = Random.Range(10, 15);
        yield return new WaitForSeconds(r);
        CreateRandomItemOnRandomLocation();
    }


    void CreateRandomItemOnRandomLocation()
    {
        if (GameController.gameController.gameStates != GameController.GameStates.Play) return;
        int randomPosition = Random.Range(0, itemsPosition.Length);
        int randomPrefab = Random.Range(0, prefabItems.Length);
        SpawnPosition spawnPos = itemsPosition[randomPosition].GetComponent<SpawnPosition>();

        if (spawnPos != null)
        {
            spawnPos.prefabToSpawn = prefabItems[randomPrefab];
            spawnPos.SpawnItem(); // Fehler
           
            StartCoroutine(SpawnItem());
        }
        
    }

}
