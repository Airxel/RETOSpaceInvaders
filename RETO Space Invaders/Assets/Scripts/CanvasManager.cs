using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu, gameOverMenu, victoryMenu, menuBeams, upBanner;

    private void Start()
    {
        LeanTween.moveX(menuBeams.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeInSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), 0f, 1.25f).setEase(LeanTweenType.easeInSine);
    }

    public void PlayIsClicked()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene("Player Selection");
    }
}
