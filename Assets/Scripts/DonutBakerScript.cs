using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutBakerScript : MonoBehaviour
{
    [System.Serializable]
    public class BakeryItem
    {
        public string name;
        public GameObject prefab;
        public float chance;
    }

    public List<BakeryItem> bakeryItems;

    public bool autoStartBaking = true;

    public float bakeInterval = 1.0f;
    public float offset = 0.7f;

    Transform ovenTransform;
    float minPoz, maxPoz;

    void Start()
    {
        ovenTransform = GetComponent<Transform>();

        if (autoStartBaking)
        {
            BakeDonut(true);
        }
    }

    public void BakeDonut(bool state)
    {
        if (state)
            StartCoroutine(Bake());
        else
            StopAllCoroutines();
    }

    IEnumerator Bake()
    {
        while (true)
        {
            if (bakeryItems.Count > 0)
            {
  
                float totalWeight = 0f;
                foreach (var item in bakeryItems)
                {
                    totalWeight += item.chance;
                }

                float randomPick = Random.Range(0f, totalWeight);

                float currentSum = 0f;
                bool spawned = false;

                foreach (BakeryItem item in bakeryItems)
                {
                    currentSum += item.chance;
                    if (randomPick <= currentSum)
                    {
                        SpawnItem(item.prefab);
                        spawned = true;
                        break;
                    }
                }

                if (!spawned)
                {
                    SpawnItem(bakeryItems[bakeryItems.Count - 1].prefab);
                }
            }

            yield return new WaitForSeconds(bakeInterval);
        }
    }

    void SpawnItem(GameObject prefabToSpawn)
    {
        minPoz = ovenTransform.position.x - offset;
        maxPoz = ovenTransform.position.x + offset;
        float randPoz = Random.Range(minPoz, maxPoz);
        Vector2 spawnPoz = new Vector2(randPoz, ovenTransform.position.y);

        Instantiate(prefabToSpawn, spawnPoz, Quaternion.identity, ovenTransform);
    }
}