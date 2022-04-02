using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class DebrisSpawner : MonoBehaviour
{
    const float MinSpawnInterval = 1.0f;
    const float MaxSpawnInterval = 10.0f;
    float spawnInterval = MaxSpawnInterval;

    const int minSpawnCountPerWave = 1;
    const int maxSpawnCountPerWave = 5;

    int currentWave;
    const int maxWave = 100;

    [SerializeField] AnimationCurve spawnIntervalInterpolation;

    IEnumerator Start()
    {
        while (enabled)
        {
            int randSpawnAmount = Random.Range(minSpawnCountPerWave, maxSpawnCountPerWave);

            // spawn a random amount of debris
            for (int i = 0; i < randSpawnAmount; i++)
            {
                var debris = DebrisPool.Instance.Get();

                Vector3 randPos = 10f * Random.insideUnitCircle;
                float randRot = Random.Range(0f, 360f);
                debris.transform.SetPositionAndRotation(randPos, Quaternion.Euler(randRot * Vector3.forward));

                debris.enabled = true;
                debris.gameObject.SetActive(true);
            }

            // increment wave
            if (currentWave <= maxWave)
            {
                currentWave++;
            }

            // reduce spawn interval based on interpolation curve
            float lerpProgress = (float)currentWave / maxWave;
            spawnInterval = Mathf.Lerp(MinSpawnInterval, MaxSpawnInterval, spawnIntervalInterpolation.Evaluate(lerpProgress));

            yield return WaitForSeconds(spawnInterval);
        }
    }

}