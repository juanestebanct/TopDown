using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons, selectPlayer;
    [SerializeField] private CanvasGroup blackScreen;

    private void Start()
    {
        blackScreen.alpha = 0;
        OpenMainbuttons();
    }
    public void CloseMenus()
    {
        mainButtons.SetActive(false);
        selectPlayer.SetActive(false);
    }
    public void OpenSelect()
    {
        CloseMenus();
        selectPlayer.SetActive(true);
    }
    public void OpenMainbuttons()
    {
        CloseMenus();
        mainButtons.SetActive(true);
    }
    public void ChangeLevelAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
        FadeToBlack(0.5f);
    }
    public void FadeToBlack(float duration)
    {
        blackScreen.DOFade(1, duration).SetEase(Ease.InOutQuad);
        print(duration);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Puedes mostrar una barra de carga aquí si lo deseas
            yield return null;
        }
    }
}
