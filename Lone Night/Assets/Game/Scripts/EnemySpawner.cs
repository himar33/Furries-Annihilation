using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyType
    {
        EASY,
        NORMAL,
        HARD
    }

    [Header("Enemy Prefab")]

    [Header("Easy Enemies")]
    [SerializeField]
    private GameObject[] easyEnemies;

    [Header("Normal Enemies")]
    [SerializeField]
    private GameObject[] normalEnemies;

    [Header("Hard Enemies")]
    [SerializeField]
    private GameObject[] hardEnemies;

    [Header("Minimum Scale Range")]
    [SerializeField, Range(0.2f, 0.4f)]
    private float minX;
    [SerializeField, Range(0.2f, 0.4f)]
    private float minY;
    [SerializeField, Range(0.2f, 0.4f)]
    private float minZ;

    [Header("Maximum Scale Range")]
    [SerializeField, Range(0.2f, 0.4f)]
    private float maxX;
    [SerializeField, Range(0.2f, 0.4f)]
    private float maxY;
    [SerializeField, Range(0.2f, 0.4f)]
    private float maxZ;

    private Vector3 minScale;
    private Vector3 maxScale;

    [Header("Position Range")]
    [SerializeField]
    private Collider spawnCollider;

    public void Spawn(EnemyType type, int n)
    {
        Transform eRoot = GameObject.Find("EnemiesRoot").transform;

        minScale = new Vector3(minX, minY, minZ);
        maxScale = new Vector3(maxX, maxY, maxZ);

        for (int i = 0; i < n; i++)
        {
            GameObject enemy = null;
            switch (type)
            {
                case EnemyType.EASY:
                    enemy = easyEnemies[Random.Range(0, easyEnemies.Length)];
                    break;
                case EnemyType.NORMAL:
                    enemy = normalEnemies[Random.Range(0, normalEnemies.Length)];
                    break;
                case EnemyType.HARD:
                    enemy = hardEnemies[Random.Range(0, hardEnemies.Length)];
                    break;
                default:
                    break;
            }
            enemy.transform.localScale = GetRandomScale(minScale, maxScale);
            Instantiate(enemy, GetRandomPosition(spawnCollider.bounds), enemy.transform.rotation, eRoot);
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
