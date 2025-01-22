using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private LoopMap _loopMap;
    
    public float moveSpeed = 10.0f;
    public float limitPosition = 10.0f;
    
    private void Start()
    {
        _loopMap = gameObject.transform.parent.GetComponent<LoopMap>();
    }
    
    private void OnDisable()
    {
        if (!gameObject.activeSelf) return;
        _loopMap.ReturnToPool(gameObject);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        
        transform.position -= moveSpeed * Time.deltaTime * Vector3.forward;
        if (transform.position.z <= -limitPosition)
        {
            _loopMap.ReturnToPool(gameObject);
            _loopMap.SpawnTile();
        }
    }

}
