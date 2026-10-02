using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using System.Collections.Generic;

public class SplineMovement : MonoBehaviour
{
    [Header("Spline")]
    public SplineContainer spline;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Enemy")]
    public GameObject enemyPrefab;
    public float enemyDistance = 0.91f;

    private SplineStop[] stops;

    private int currentStop = -1;
    private float currentT = 0f;
    private float targetT = 0f;

    private bool isMoving = false;

    void Start()
    {
        stops = FindObjectsByType<SplineStop>(
            FindObjectsSortMode.None
        );

        // Sort stops from beginning to end of spline
        System.Array.Sort(stops, (a, b) =>
            a.splinePosition.CompareTo(b.splinePosition)
        );

        // Put player at the beginning of the spline
        currentT = 0f;

        UpdatePlayerPosition();

        Debug.Log("Found " + stops.Length + " spline stops.");
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!isMoving)
            {
                MoveToNextStop();
            }
        }

        if (isMoving)
        {
            MoveAlongSpline();
        }
    }

    void MoveToNextStop()
    {
        currentStop++;

        if (currentStop >= stops.Length)
        {
            Debug.Log("No more stops!");
            return;
        }

        targetT = stops[currentStop].splinePosition;

        isMoving = true;

        Debug.Log("Moving to stop " + currentStop);
    }

    void MoveAlongSpline()
    {
        float direction = Mathf.Sign(targetT - currentT);

        currentT += direction *
                     (moveSpeed / spline.CalculateLength()) *
                     Time.deltaTime;

        // Check if we've reached the stop
        if ((direction > 0 && currentT >= targetT) ||
            (direction < 0 && currentT <= targetT))
        {
            currentT = targetT;

            isMoving = false;

            ArriveAtStop();
        }

        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        Vector3 position = spline.EvaluatePosition(currentT);
        Vector3 direction = spline.EvaluateTangent(currentT);

        transform.position = position;

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }

    void ArriveAtStop()
    {
        Debug.Log("Arrived at fight stop!");

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab has not been assigned!");
            return;
        }

        Vector3 spawnPosition =
            transform.position +
            transform.forward * enemyDistance;

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.LookRotation(-transform.forward)
        );

        Debug.Log("Enemy spawned!");
    }
}
