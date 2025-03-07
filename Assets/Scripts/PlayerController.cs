using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHP = 100;
    public int currentHP;
    public float regenerationRate = 1f;
    public float collisionRadius = 1f;

    private int currentLane = 1;
    public Vector3[] lanes;

    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    public float shakeMagnitude = 0.2f;
    public float shakeDuration = 0.2f;
    private float shakeTime = 0f;

    private float regenerationTimer = 0f;

    public TextMeshProUGUI hpText;

    void Start()
    {
        currentHP = maxHP;
        lanes = new Vector3[3] { new Vector3(-3, -3.17f, 0), new Vector3(0, -3.17f, 0), new Vector3(3, -3.17f, 0) };
        transform.position = lanes[currentLane];

        mainCamera = Camera.main;
        originalCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        MovePlayer();
        RegenerateHealth();
        DetectCollisions();

        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            ShakeCamera();
        }

        UpdateHealthUI();
    }

    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > 0)
            {
                currentLane--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 2)
            {
                currentLane++;
            }
        }

        Vector3 targetPosition = new Vector3(lanes[currentLane].x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void RegenerateHealth()
    {
        if (currentHP < maxHP)
        {
            regenerationTimer += Time.deltaTime;

            if (regenerationTimer >= 1f)
            {
                currentHP += 1;
                regenerationTimer = 0f;
            }

            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    private void DetectCollisions()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (var obstacle in obstacles)
        {
            float distance = Vector3.Distance(transform.position, obstacle.transform.position);

            if (distance < collisionRadius)
            {
                currentHP -= 5;
                shakeTime = shakeDuration;
                Destroy(obstacle);
            }
        }
    }

    private void ShakeCamera()
    {
        if (shakeTime > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            mainCamera.transform.position = originalCameraPosition + shakeOffset;
        }
        else
        {
            mainCamera.transform.position = originalCameraPosition;
        }
    }

    private void UpdateHealthUI()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }
    }
}
