using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGas : MonoBehaviour
{
    private readonly Queue<GameObject> _gasPool = new Queue<GameObject>();
    
    public GameObject gasPrefab;
    public List<float> randomPositions = new List<float>();
    
    public int poolSize = 5;
    public float timeElapsed  = 0f;
    public float spawnTime = 3.0f;
    
    
    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var gas = Instantiate(gasPrefab, transform);
            gas.SetActive(false);
            _gasPool.Enqueue(gas);
        }
    }
    
    private void ActivateRandomGas()
    {
        if (_gasPool.Count <= 0) return;
        var gas = _gasPool.Dequeue();
        gas.SetActive(true); 
        var randomIndex = Random.Range(0, randomPositions.Count);
        gas.transform.position = new Vector3(randomPositions[randomIndex], 0, transform.position.z);
    }

    public void ReturnToPool(GameObject gas)
    {
        gas.SetActive(false);
        _gasPool.Enqueue(gas);
    }


    // Update is called once per frame
    void Update()
    {
        timeElapsed+= Time.deltaTime;
        if (!(timeElapsed >= spawnTime)) return;
        ActivateRandomGas();
        timeElapsed  = 0;
    }
}
