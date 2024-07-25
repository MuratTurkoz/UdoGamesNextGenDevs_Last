using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffectController : MonoBehaviour
{
    public Image bloodEffectImage;
    public float fadeDuration = 2.0f; // Saydamlığı değiştirme süresi

    public void StartEffect()
    {
        StartCoroutine(FadeInAndOut());
    }

    public void StartEffectJustOne()
    {
        StartCoroutine(FadeInAndOutJustOne());
    }

    public void StopEffect()
    {
        StopCoroutine(FadeInAndOut());
        StopCoroutine(Fade(0f, 1f, fadeDuration));
        StopCoroutine(Fade(1f, 0f, fadeDuration));
        Color color = bloodEffectImage.color;
        color.a = 1f;
        bloodEffectImage.color = color;
    }

    public IEnumerator FadeInAndOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        }
    }

    public IEnumerator FadeInAndOutJustOne()
    {
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

    }



    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = bloodEffectImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            bloodEffectImage.color = color;
            yield return null;
        }

        // Alfa değerini tam olarak hedef değere ayarla
        color.a = endAlpha;
        bloodEffectImage.color = color;
    }
}
