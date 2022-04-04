using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FPSDisplay : MonoBehaviour
{
    TextMeshProUGUI fpsText;

    const float RefreshRate = 0.5f;
    float refreshTimer;

    void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        refreshTimer -= Time.deltaTime;

        if (refreshTimer <= 0)
        {
            fpsText.text = $"{1f / Time.deltaTime:f1} fps";
            refreshTimer = RefreshRate;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fpsText.enabled = !fpsText.enabled;
        }
    }
}