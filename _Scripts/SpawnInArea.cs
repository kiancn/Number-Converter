using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnInArea : MonoBehaviour
{
    [SerializeField] private int maxTokens = 200;
    [SerializeField] private GameObject prefab; // object the will be instance
    [SerializeField] private Transform centerTransform;
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private Vector3 positionVariance;
    [SerializeField] float timeBetweenReveals = 0.3f;

    [SerializeField] private List<GameObject> pool; // signifies reservoir of instances of prefab  

    private void Awake()
    {
        if (centerTransform != null) { centerPosition = centerTransform.position; }

        if (prefab == null)
        {
            Debug.Log("You forgot to set the prefab, SpawnInArea object shutting down.");
            Destroy(this, 0.5f);
            gameObject.SetActive(false);
            return;
        }

        pool = new List<GameObject>();

        StartCoroutine(InstancePrefabsToPool(maxTokens));
    }

    private IEnumerator InstancePrefabsToPool(int numberOfPrefabs)
    {
        var pauseInterval = 10;
        int count = 0;

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject newToken = Instantiate(prefab, transform);

            newToken.SetActive(false);
            pool.Add(newToken);

            if (++count % pauseInterval == 0)
            {
                yield return new WaitForFixedUpdate();
            }
        }


        Debug.Log("Instanced " + numberOfPrefabs + " tokens.");
    }

    public void SpawnTokens(string numberString)
    {
        try
        {
            int number = int.Parse(numberString);

            StopAllCoroutines();
            pool.ForEach(o => o.SetActive(false));

            if (number < pool.Count)
            {
                StartCoroutine(RevealTokens(number));
            }
            else
            {
                StartCoroutine(InstancePrefabsToPool(maxTokens));
            }
        }
        catch (FormatException e) { }
    }

    private IEnumerator RevealTokens(int numberToReveal)
    {
        for (int i = 0; i < numberToReveal; i++)
        {
            float x = Random.Range(centerPosition.x - positionVariance.x, centerPosition.x + positionVariance.x);
            float y = Random.Range(centerPosition.y - positionVariance.y, centerPosition.y + positionVariance.y);
            float z = Random.Range(centerPosition.z - positionVariance.z, centerPosition.z + positionVariance.z);

            pool[i].transform.position = new Vector3(x, y, z);
            pool[i].SetActive(true);

            yield return new WaitForSecondsRealtime(timeBetweenReveals);
        }
    }
}