using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject collectiblePrefab;  // Toplanacak eşyanın prefab'ı
    public int initialSpawnCount = 10;    // Başlangıçta spawn edilecek eşya sayısı
    public float respawnTime = 5f;        // Eşyaların yeniden spawn olma süresi
    public float minSpawnDistance = 5f;   // Eşyaların birbirine en yakın olabileceği mesafe
    public Camera mainCamera;             // Ana kamera referansı
    public List<GameObject> areaObjects;  // Spawn alanını belirleyen GameObject'lerin listesi
    public LayerMask obstacleLayer;       // Engellerin bulunduğu katman

    private List<GameObject> collectibles = new List<GameObject>();

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (areaObjects == null || areaObjects.Count == 0)
        {
            Debug.LogError("Area Objects are not assigned or the list is empty.");
            return;
        }

        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnCollectible();
        }
    }

    void SpawnCollectible()
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = GetRandomPositionWithinAreas();
            spawnPosition.z = 0;
        } while (IsPositionTooClose(spawnPosition) || IsPositionInsideCameraView(spawnPosition) || IsPositionInsideObstacle(spawnPosition));

        GameObject item = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
        item.GetComponent<CollectableObject>().OnCollected += HandleCollectibleCollected;
        collectibles.Add(item);
    }

    Vector3 GetRandomPositionWithinAreas()
    {
        GameObject selectedArea = areaObjects[Random.Range(0, areaObjects.Count)];
        Bounds bounds = selectedArea.GetComponent<Renderer>().bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }

    bool IsPositionTooClose(Vector3 position)
    {
        foreach (GameObject collectible in collectibles)
        {
            if (Vector3.Distance(position, collectible.transform.position) < minSpawnDistance)
            {
                return true;
            }
        }
        return false;
    }

    bool IsPositionInsideCameraView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    bool IsPositionInsideObstacle(Vector3 position)
    {
        float checkRadius = 0.5f; // Kontrol yarıçapını ayarlayın
        return Physics.CheckSphere(position, checkRadius, obstacleLayer);
    }

    void HandleCollectibleCollected(GameObject collectible)
    {
        collectibles.Remove(collectible);
        Destroy(collectible);
        StartCoroutine(RespawnCollectible());
    }

    IEnumerator RespawnCollectible()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnCollectible();
    }
}
