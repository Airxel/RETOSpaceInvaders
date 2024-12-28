using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu, gameOverMenu, victoryMenu, menuBeams, selectionFastShipCollection, selectionBalancedShipCollection, selectionSlowShipCollection;

    [SerializeField]
    public TextMeshProUGUI scoreNumber, highScoreNumber, lifesNumber;

    public GameObject playerSelection, playerSelectionUI, informationUI, fastShipCollection, balancedShipCollection, slowShipCollection, mainShipCollection, enemiesSpawner, sheltersCollection;

    [SerializeField]
    public int lifes = 3;

    private float score = 0f;
    private float highScore = 0f;

    public static bool isRespawning = false;

    //Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //Se llama al valor guardado en el PlayerPrefs para la mayor puntuación
        highScore = PlayerPrefs.GetFloat("High Score", 0);

        //Se pone ese valor en el texto te la UI
        highScoreNumber.text = highScore.ToString("00000");
        scoreNumber.text = score.ToString("00000");
        lifesNumber.text = lifes.ToString();

        LeanTween.moveX(menuBeams.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeInSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), 0f, 1.25f).setEase(LeanTweenType.easeInSine);
    }

    public void AddPoints(float points)
    {
        //Se va sumando a la puntuación los puntos obtenidos al destruir invaders
        score = score + points;

        //Se ponen esos valores en los textos de la UI
        scoreNumber.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;

            //Si la puntuación actual es superior a la mayor puntuación, se guarda el valor para las siguientes partidas
            PlayerPrefs.SetFloat("High Score", highScore);

            //Y se pone el valor en la UI
            highScoreNumber.text = highScore.ToString("00000");
        }
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
