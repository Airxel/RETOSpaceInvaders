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

        //Se llama al valor guardado en el PlayerPrefs para la mayor puntuación
        highScore = PlayerPrefs.GetFloat("High Score", 0);

        //Se pone ese valor en el texto te la UI
        highScoreNumber.text = highScore.ToString("00000");
        scoreNumber.text = score.ToString("00000");
        gameOverScoreNumber.text = score.ToString("00000");
        victoryScoreNumber.text = score.ToString("00000");
        livesNumber.text = lives.ToString();

        //Se activa y anima en menú principal
        mainMenu.SetActive(true);
        LeanTween.moveY(mainMenu.GetComponent<RectTransform>(), -25f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    /// <summary>
    /// Función que controla la puntuación
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(float points)
    {
        //Se va sumando a la puntuación los puntos obtenidos al destruir invaders
        score = score + points;

        //Se ponen esos valores en los textos de la UI
        scoreNumber.text = score.ToString("00000");
        gameOverScoreNumber.text = score.ToString("00000");
        victoryScoreNumber.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;

            //Si la puntuación actual es superior a la mayor puntuación, se guarda el valor para las siguientes partidas
            PlayerPrefs.SetFloat("High Score", highScore);

            //Y se pone el valor en la UI
            highScoreNumber.text = highScore.ToString("00000");
        }
    }

    /// <summary>
    /// Función que se activa cuando el jugador se queda sin vidas, se desactivan los elementos activos y se activa el menú de derrota
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
    /// Función que se activa cuando el jugador destruye a todos los invaders, se desactivan los elementos activos y se activa el menú de victoria
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
    /// Función que se activa cuando se pulsa el botón de jugar, se desactiva el menú principal y se activan los elementos de la selección de personaje
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
    /// Función que se activa cuando se pulsa el botón de opciones, aparecen las opciones de volumen
    /// </summary>
    public void OptionsIsClicked()
    {
        LeanTween.moveX(volumeMenu.GetComponent<RectTransform>(), 450f, 1f).setEase(LeanTweenType.easeInSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), -150f, 1f).setEase(LeanTweenType.easeOutSine);
    }

    /// <summary>
    /// Función que se activa cuando se pulsa el botón de X, desaparecen las opciones de volumen
    /// </summary>
    public void XIsClicked()
    {
        LeanTween.moveX(volumeMenu.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeOutSine);
        LeanTween.moveX(mainMenu.GetComponent<RectTransform>(), 0f, 1.25f).setEase(LeanTweenType.easeInSine);
    }

    /// <summary>
    /// Función que se activa cuando se pulsa el botón de menú principal, se recarga la escena
    /// </summary>
    public void MainMenuIsClicked()
    {
        SceneManager.LoadScene("Main Game");
    }

    /// <summary>
    /// Función que se activa cuando se pulsa el botón de salir, se sale del editor en este caso
    /// </summary>
    public void QuitIsClicked()
    {
        Debug.Log("Saliendo...");
        Application.Quit();

#if UNITY_EDITOR
        // Si estás en el Editor, para simular el cierre:
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Función por si hay que resetear manualmente la puntuación máxima
    /// </summary>
    private void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("High Score");
    }

    /// <summary>
    /// Prueba para un botón de restart/replay, ir a la selección de personaje sin pasar por el menú principal, reseteando los elementos
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
