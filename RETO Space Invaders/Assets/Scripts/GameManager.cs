using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu, gameOverMenu, victoryMenu, volumeMenu, selectionFastShipCollection, selectionBalancedShipCollection, selectionSlowShipCollection;

    [SerializeField]
    public TextMeshProUGUI scoreNumber, highScoreNumber, livesNumber, gameOverScoreNumber, victoryScoreNumber;

    public GameObject playerSelection, playerSelectionUI, informationUI, fastShipCollection, balancedShipCollection, slowShipCollection, mainShipCollection, enemiesSpawner, sheltersCollection;

    [SerializeField]
    private int initialLives = 3;

    [SerializeField]
    public int lives;

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
        //Se establecen las vidas que se van a usar en el script, con las idas iniciales
        lives = initialLives;

        //Se llama al valor guardado en el PlayerPrefs para la mayor puntuaci�n
        highScore = PlayerPrefs.GetFloat("High Score", 0);

        //Se pone ese valor en el texto te la UI
        highScoreNumber.text = highScore.ToString("00000");
        scoreNumber.text = score.ToString("00000");
        gameOverScoreNumber.text = score.ToString("00000");
        victoryScoreNumber.text = score.ToString("00000");
        livesNumber.text = lives.ToString();

        //Se activa y anima en men� principal
        mainMenu.SetActive(true);
        LeanTween.moveY(mainMenu.GetComponent<RectTransform>(), -25f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    /// <summary>
    /// Funci�n que controla la puntuaci�n
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(float points)
    {
        //Se va sumando a la puntuaci�n los puntos obtenidos al destruir invaders
        score = score + points;

        //Se ponen esos valores en los textos de la UI
        scoreNumber.text = score.ToString("00000");
        gameOverScoreNumber.text = score.ToString("00000");
        victoryScoreNumber.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;

            //Si la puntuaci�n actual es superior a la mayor puntuaci�n, se guarda el valor para las siguientes partidas
            PlayerPrefs.SetFloat("High Score", highScore);

            //Y se pone el valor en la UI
            highScoreNumber.text = highScore.ToString("00000");
        }
    }

    /// <summary>
    /// Funci�n que se activa cuando el jugador se queda sin vidas, se desactivan los elementos activos y se activa el men� de derrota
    /// </summary>
    public void PlayerDead()
    {
        //Se desactivan los enemigos que queden vivos
        InvadersManager.instance.DeactivateInvaders();

        sheltersCollection.SetActive(false);
        enemiesSpawner.SetActive(false);
        balancedShipCollection.SetActive(false);
        fastShipCollection.SetActive(false);
        slowShipCollection.SetActive(false);

        gameOverMenu.SetActive(true);
        LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -50f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    /// <summary>
    /// Funci�n que se activa cuando el jugador destruye a todos los invaders, se desactivan los elementos activos y se activa el men� de victoria
    /// </summary>
    public void PlayerVictory()
    {
        sheltersCollection.SetActive(false);
        enemiesSpawner.SetActive(false);
        balancedShipCollection.SetActive(false);
        fastShipCollection.SetActive(false);
        slowShipCollection.SetActive(false);

        victoryMenu.SetActive(true);
        LeanTween.moveY(victoryMenu.GetComponent<RectTransform>(), -50f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    /// <summary>
    /// Funci�n que se activa cuando se pulsa el bot�n de jugar, se desactiva el men� principal y se activan los elementos de la selecci�n de personaje
    /// </summary>
    public void PlayIsClicked()
    {
        mainMenu.SetActive(false);

        playerSelection.SetActive(true);
        playerSelectionUI.SetActive(true);
        LeanTween.moveY(selectionFastShipCollection, 55f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionBalancedShipCollection, 55f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionSlowShipCollection, 55f, 1.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    /// <summary>
    /// Funci�n que se activa cuando se pulsa el bot�n de opciones, aparecen las opciones de volumen
    /// </summary>
    public void OptionsIsClicked()
    {
        LeanTween.moveX(volumeMenu.GetComponent<RectTransform>(), 450f, 1f).setEase(LeanTweenType.easeInSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), -150f, 1f).setEase(LeanTweenType.easeOutSine);
    }

    /// <summary>
    /// Funci�n que se activa cuando se pulsa el bot�n de X, desaparecen las opciones de volumen
    /// </summary>
    public void XIsClicked()
    {
        LeanTween.moveX(volumeMenu.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeOutSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), 0f, 1.25f).setEase(LeanTweenType.easeInSine);
    }

    /// <summary>
    /// Funci�n que se activa cuando se pulsa el bot�n de men� principal, se recarga la escena
    /// </summary>
    public void MainMenuIsClicked()
    {
        SceneManager.LoadScene("Main Game");
    }

    /// <summary>
    /// Funci�n que se activa cuando se pulsa el bot�n de salir, se sale del editor en este caso
    /// </summary>
    public void QuitIsClicked()
    {
        Debug.Log("Saliendo...");
        Application.Quit();

#if UNITY_EDITOR
        // Si est�s en el Editor, para simular el cierre:
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Funci�n por si hay que resetear manualmente la puntuaci�n m�xima
    /// </summary>
    private void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("High Score");
    }

    /// <summary>
    /// Prueba para un bot�n de restart/replay, ir a la selecci�n de personaje sin pasar por el men� principal, reseteando los elementos
    /// </summary>
    public void ReplayIsClicked()
    {
        enemiesSpawner.SetActive(false);
        InvadersManager.instance.ClearInvaders();

        lives = initialLives;
        livesNumber.text = lives.ToString();

        score = 0f;
        scoreNumber.text = score.ToString("00000");

        victoryMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        informationUI.SetActive(false);

        playerSelection.SetActive(true);
        playerSelectionUI.SetActive(true);

        mainShipCollection.SetActive(true);

        LeanTween.moveY(selectionFastShipCollection, 55f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionBalancedShipCollection, 55f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(selectionSlowShipCollection, 55f, 1.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }
}
