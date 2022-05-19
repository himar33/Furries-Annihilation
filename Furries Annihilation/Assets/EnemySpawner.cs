using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int nEnemies;

    [Header("Minimum Scale Range")]
    [SerializeField, Range(0.8f, 1.2f)]
    private float minX;
    [SerializeField, Range(0.8f, 1.2f)]
    private float minY;
    [SerializeField, Range(0.8f, 1.2f)]
    private float minZ;

    [Header("Maximum Scale Range")]
    [SerializeField, Range(0.8f, 1.2f)]
    private float maxX;
    [SerializeField, Range(0.8f, 1.2f)]
    private float maxY;
    [SerializeField, Range(0.8f, 1.2f)]
    private float maxZ;

    private Vector3 minScale;
    private Vector3 maxScale;

    [Header("Position Range")]
    [SerializeField]
    private Collider spawnCollider;

    private void Start()
    {
        Transform eRoot = GameObject.Find("EnemiesRoot").transform;

        minScale = new Vector3(minX, minY, minZ);
        maxScale = new Vector3(maxX, maxY, maxZ);
        for (int i = 0; i < nEnemies; i++)
        {
            GameObject e = enemyPrefab;
            e.transform.localScale = GetRandomScale(minScale, maxScale);
            Instantiate(e, GetRandomPosition(spawnCollider.bounds), e.transform.rotation, eRoot);
        }
    }

    private static Vector3 GetRandomPosition(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private static Vector3 GetRandomScale(Vector3 min, Vector3 max)
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z)
        );
    }
}
