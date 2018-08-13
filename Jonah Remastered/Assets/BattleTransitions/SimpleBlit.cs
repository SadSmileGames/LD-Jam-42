using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    public Material TransitionMaterial;

    private void OnEnable()
    {
        GameController.OnNextWave += StartFade;
        GameController.OnNextWaveBegin += StartFadeOut;
        PlayerHealth.OnPlayerDeath += StartFade;
    }

    private void OnDisable()
    {
        GameController.OnNextWave -= StartFade;
        GameController.OnNextWaveBegin -= StartFadeOut;
        PlayerHealth.OnPlayerDeath -= StartFade;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }

    public void StartFade()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float transition = 1f;
        while(transition > 0)
        {
            transition -= Time.deltaTime;
            TransitionMaterial.SetFloat("_Cutoff", transition);

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float transition = 0f;
        while (transition < 1)
        {
            transition += Time.deltaTime;
            TransitionMaterial.SetFloat("_Cutoff", transition);

            yield return null;
        }
    }

    
}
