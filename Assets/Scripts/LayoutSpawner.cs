using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutSpawner : MonoBehaviour
{
    public GameObject[] TrashToSpawn;
    public GameObject[] Hazards;
    public int numTrash;
    public int numHazards;
    public float overlapTestBoxSize;
    public LayerMask spawnedObjectLayer;

    public float itemSpreadX;
    public float itemSpreadY;
    public float itemSpreadZ;

    private int spawnedTrash;

    // Start is called before the first frame update
    void Start()
    {
        int hazardIndex;
        for (int i = 0; i < numHazards; i++)
        {
            hazardIndex = Random.Range(0, Hazards.Length);
            SpreadItems(Hazards[hazardIndex]);
        }

        int trashIndex;
        spawnedTrash = 0;
        for (int i = 0; i < numTrash; i++)
        {
            trashIndex = Random.Range(0, TrashToSpawn.Length);
            SpreadItems(TrashToSpawn[trashIndex]);
        }
        Debug.Log(spawnedTrash);
    }

    void RaycastPosition(Vector3 position, GameObject Item)
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
                Spawn(Item, hit.point + new Vector3(0, 0.2f, 0), spawnRotation);
            }
        }
    }

    void SpreadItems(GameObject Item)
    {
        Vector3 randPos = new Vector3(Random.Range(-itemSpreadX, itemSpreadX), itemSpreadY, Random.Range(-itemSpreadZ, itemSpreadZ));
        RaycastPosition(randPos, Item);
    }
    
    void Spawn(GameObject Item, Vector3 position, Quaternion rotation)
    {
        if (Item == TrashToSpawn[2])
        {
            position += new Vector3(0, 0.2f, 0);
        }
        else if (Item == TrashToSpawn[0] || Item == TrashToSpawn[4])
        {
            position += new Vector3(0, 0.1f, 0);
        }
        Instantiate(Item, position, Item.transform.rotation);
        spawnedTrash++;
    }
}
