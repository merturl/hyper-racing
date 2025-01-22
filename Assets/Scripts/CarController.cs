using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gasText;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject gameOverUI;
    
    public float maxGas = 100.0f;
    public float moveSpeed = 10f;
    public float gasConsumptionRate = 10f; // 1초당 소모되는 가스량
    
    private Camera _camera;
    public float CurrentGas { get; set; }
    private float _timeElapsed  = 0f;

    void Start()
    {
        _camera = Camera.main;
        CurrentGas = maxGas;
        _timeElapsed = 0f;
    }
    void OnEnable()
    {   
        CurrentGas = maxGas;
        _timeElapsed = 0f;
    }
    void Update()
    {
        HandleGasConsumption();
        HandleCarMovement();
        CheckGameOver();
    }

    private void HandleGasConsumption()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= 1.0f)
        {
            CurrentGas -= gasConsumptionRate;
            _timeElapsed = 0f;
        }

        gasText.text = $"{CurrentGas}";
    }

    private void HandleCarMovement()
    {
        if (!Input.GetMouseButton(0)) return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return;
        var hitPoint = hit.point;
        MoveCar(hitPoint.x < 0 ? -1 : 1);
    }

    private void MoveCar(int horizontal)
    {
        var vector = Time.deltaTime * horizontal * moveSpeed * transform.right;
        var newPosition = transform.position + vector;
        newPosition.x = Mathf.Clamp(newPosition.x, -4f, 4f);
        transform.position = newPosition;
    }

    private void CheckGameOver()
    {
        if (!(CurrentGas <= 0)) return;
        _timeElapsed = 0f;
        gameOverUI.SetActive(true);
        main.SetActive(false);
        mainUI.SetActive(false);
    }
}
