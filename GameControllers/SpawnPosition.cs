using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    GameObject item;
    public GameObject prefabToSpawn;

    public void SpawnItem()
    {
        if (item == null)
        {
            item = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            item.name = prefabToSpawn.name;
            item.transform.parent = gameObject.transform;
        }
    }


}
