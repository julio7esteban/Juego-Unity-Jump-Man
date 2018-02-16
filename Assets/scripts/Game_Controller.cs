using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game_Controller : MonoBehaviour {
    public float parallaxSpeed = 0.02f;
    public RawImage Background;
    public RawImage plataform;
    public GameObject uiIdle;
    public GameObject uiScore;
    public enum GameState { Idle, Playing,Ended,  };
    public GameState gameState = GameState.Idle;
    public GameObject player;
    public Text pointsText;
    public Text recordText;
    public GameObject enemyGenerator;

    private int points = 0; 
    // Use this for initialization
    void Start () {
        recordText.text = "Mejor Puntaje: " + GetMaxScore().ToString();

    }

    // Update is called once per frame
    void Update() {

        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
        //empieza el juego 
        if (gameState == GameState.Idle && (userAction))
        {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState", "sprint");
            enemyGenerator.SendMessage("StartGeneration");



        }
        //si ya se esta jugando 
        else if (gameState == GameState.Playing)
        {

            Paralax();
          
        }
        else if (gameState == GameState.Ended)
        {

            if(userAction)
            {
                RestartGame();
            }

        }



    }
    void Paralax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        Background.uvRect = new Rect(Background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed, 0f, 1f, 1f);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("project");
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();

        if (points >= GetMaxScore())
        {

            recordText.text = "Mejor Puntaje: " + points.ToString();
            SaveScore(points);
        }
    }
    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);

    }
    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints); 
    }
}
