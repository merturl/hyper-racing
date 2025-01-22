using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMap : MonoBehaviour
{
    private readonly Queue<GameObject> _tilePool = new Queue<GameObject>();
    
    public GameObject tilePrefab;
    public float tileLength = 10.0f; 
    public int poolSize = 5;
    public Vector3 spawnPosition; 
    public void ReturnToPool(GameObject tile)
    {
        tile.SetActive(false);
        _tilePool.Enqueue(tile);
    }
    public void SpawnTile()
    {
        if (_tilePool.Count <= 0) return;
        var tile = _tilePool.Dequeue();
        tile.SetActive(true); 
        tile.transform.position = spawnPosition;
    }

    public void SpawnTile(Vector3 position)
    {
        if (_tilePool.Count <= 0) return;
        var tile = _tilePool.Dequeue();
        tile.SetActive(true); 
        tile.transform.position = position;
    }
    
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var tile = Instantiate(tilePrefab, transform);
            tile.transform.position = new Vector3(0,0,i*tileLength);
            tile.SetActive(false);
            _tilePool.Enqueue(tile);
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        SpawnTile(new Vector3(0,0,0));
        SpawnTile(new Vector3(0,0,10));
        SpawnTile(new Vector3(0,0,20));
        SpawnTile(new Vector3(0,0,30));
        SpawnTile(new Vector3(0,0,40));
    }
}