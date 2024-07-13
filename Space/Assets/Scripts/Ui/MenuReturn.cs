using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    public void ChangeLevelAsync(string sceneName)
    {
        StartCoroutine(Cinematic(sceneName));
        FadeToBlack(0.5f);
    }
    public void FadeToBlack(float duration)
    {
        blackScreen.DOFade(1, duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        print(duration);
    }

    private IEnumerator Cinematic(string sceneName)
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Puedes mostrar una barra de carga aquí si lo deseas
            yield return null;
        }
    }
}
