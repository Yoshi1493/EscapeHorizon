using System.Linq;
using UnityEngine;
using static VectorHelper;

public class DebrisSpawner : MonoBehaviour
{
    [SerializeField] PolygonCollider2D spawnBoundaries;
    Vector3 spawnRange;

    const float MinSpawnInterval = 1.0f;
    const float MaxSpawnInterval = 5.0f;
    float spawnInterval = MaxSpawnInterval;
    float timer = 0f;

    const int minSpawnCountPerWave = 3;
    const int maxSpawnCountPerWave = 8;

    int currentWave;
    const int MaxWave = 50;

    [SerializeField] AnimationCurve spawnIntervalInterpolation;

    void Awake()
    {
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
        FindObjectOfType<Player>().GameOverAction += OnGameOver;

        spawnRange.x = Mathf.Max(spawnBoundaries.points.Select(p => p.x).ToArray());
        spawnRange.y = Mathf.Max(spawnBoundaries.points.Select(p => p.y).ToArray());
    }

    void Update()
    {
        if (timer <= 0f)
        {
            int randSpawnAmount = Random.Range(minSpawnCountPerWave, maxSpawnCountPerWave);

            // spawn a random amount of debris
            for (int i = 0; i < randSpawnAmount; i++)
            {
                var debris = DebrisPool.Instance.Get();

                // init. position and scale
                debris.transform.position = GetRandomSpawnPosition(-spawnRange, spawnRange);
                debris.transform.localScale = Vector3.one;

                // re-enable components
                debris.collider.enabled = true;
                debris.enabled = true;
                debris.gameObject.SetActive(true);
            }

            // increment wave
            if (currentWave <= MaxWave)
            {
                currentWave++;

                // reduce spawn interval based on interpolation curve
                float lerpProgress = (float)currentWave / MaxWave;
                spawnInterval = Mathf.Lerp(MinSpawnInterval, MaxSpawnInterval, spawnIntervalInterpolation.Evaluate(lerpProgress));
            }

            // reset timer
            timer = spawnInterval;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void OnGamePaused(bool pauseState)
    {
        enabled = !pauseState;
    }

    void OnGameOver()
    {
        OnGamePaused(true);
    }
}