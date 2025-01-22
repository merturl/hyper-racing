using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private RandomGas _randomGas;
    
    public float gas = 30.0f;
    public float moveSpeed = 10.0f;
    public float limitPosition = 20.0f;
    

    private void OnDisable()
    {
        if (!gameObject.activeSelf) return;
        _randomGas.ReturnToPool(gameObject);
    }

    private void Start()
    {
        _randomGas = gameObject.transform.parent.GetComponent<RandomGas>();
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        transform.position -= moveSpeed * Time.deltaTime * Vector3.forward;
        if (transform.position.z <= -limitPosition)
        {
            _randomGas.ReturnToPool(gameObject);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CarController>().CurrentGas += gas;
        _randomGas.ReturnToPool(gameObject);
    }
}
