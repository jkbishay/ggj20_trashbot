using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSpawner : MonoBehaviour
{
    public GameObject ItemToSpawn;
    public int itemsToSpawn;
    public float overlapTestBoxSize;
    public LayerMask spawnedObjectLayer;

    public float itemSpreadX;
    public float itemSpreadY;
    public float itemSpreadZ;

    private int spawned;

    // Start is called before the first frame update
    void Start()
    {
        spawned = 0;
        for (int i = 0; i < itemsToSpawn; i++)
        {
            SpreadItems();
        }
        Debug.Log(spawned);
    }

    void RaycastPosition(Vector3 position)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, Vector3.down, out hit, 100f))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            Vector3 overlapTestBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);


            if (numberOfCollidersFound == 0)
            {
                //Debug.Log("spawned robot");
                Spawn(hit.point + new Vector3(0, 0.2f, 0), spawnRotation);
            }
        }
    }

    void SpreadItems()
    {
        Vector3 randPos = new Vector3(Random.Range(-itemSpreadX, itemSpreadX), itemSpreadY, Random.Range(-itemSpreadZ, itemSpreadZ));
        RaycastPosition(randPos);
    }
    
    void Spawn(Vector3 position, Quaternion rotation)
    {
        Instantiate(ItemToSpawn, position, rotation);
        spawned++;
    }
}
