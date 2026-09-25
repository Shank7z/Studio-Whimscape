using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float distanceFromEnemy = 4f;

    private GameObject targetEnemy;
    private List<GameObject> visitedEnemies = new List<GameObject>();

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && targetEnemy == null)
        {
            FindNextEnemy();
        }

        if (targetEnemy != null)
        {
            MoveToEnemy();
        }
    }

    void FindNextEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (visitedEnemies.Contains(enemy))
                continue;

            float distance = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            visitedEnemies.Add(targetEnemy);
            
        }
        else
        {
            Debug.Log("No Enemies");
        }
    }

    void MoveToEnemy()
    {
        Vector3 targetPosition =
            targetEnemy.transform.position -
            targetEnemy.transform.forward * distanceFromEnemy;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        transform.LookAt(targetEnemy.transform);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            targetEnemy = null;
        }
    }
}
