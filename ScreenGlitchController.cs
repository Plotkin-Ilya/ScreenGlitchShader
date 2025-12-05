using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class ScreenGlitchController : MonoBehaviour
{
    [SerializeField] private Material glitchMaterial;

    public static ScreenGlitchController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (glitchMaterial != null)
            {
                glitchMaterial.SetFloat("_NoiseAmount", 0f);
            }
        }
    }

    [Button]
    public static void GlitchWhenOnTV()
    {
        if (Instance != null)
        {
            Instance.StartCoroutine(Instance.GlitchRoutine(50f, 0.5f));
        }
    }

    public IEnumerator GlitchRoutine(float maxNoise, float duration)
    {
        float timer = 0f;
        float startValue = glitchMaterial.GetFloat("_NoiseAmount");


        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = timer / duration;

            float currentNoise = Mathf.Lerp(startValue, maxNoise, progress);

            glitchMaterial.SetFloat("_NoiseAmount", currentNoise);

            yield return null;
        }

        glitchMaterial.SetFloat("_NoiseAmount", maxNoise);


        timer = 0f;
        startValue = maxNoise;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            float currentNoise = Mathf.Lerp(startValue, 0f, progress);

            glitchMaterial.SetFloat("_NoiseAmount", currentNoise);

            yield return null;
        }

        glitchMaterial.SetFloat("_NoiseAmount", 0f);
    }
}