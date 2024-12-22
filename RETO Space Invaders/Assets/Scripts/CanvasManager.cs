using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu, gameOverMenu, victoryMenu, menuBeams, upBanner, selectionFastShipCollection, selectionBalancedShipCollection, selectionSlowShipCollection;

    public GameObject playerSelection, playerSelectionUI, fastShipCollection, balancedShipCollection, slowShipCollection, mainShipCollection;

    //Singleton
    public static CanvasManager instance;

    private void Awake()
    {
        if (CanvasManager.instance == null)
        {
            CanvasManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        LeanTween.moveX(menuBeams.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeInSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), 0f, 1.25f).setEase(LeanTweenType.easeInSine);
    }

    public void PlayIsClicked()
    {
        mainMenu.SetActive(false);
        menuBeams.SetActive(false);

        playerSelection.SetActive(true);
        playerSelectionUI.SetActive(true);

        LeanTween.moveY(selectionFastShipCollection, 55f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionBalancedShipCollection, 55f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionSlowShipCollection, 55f, 1.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }
}
