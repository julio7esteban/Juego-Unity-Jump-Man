using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public GameObject game;
    public GameObject enemyGeneration;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        bool gamePlaying = game.GetComponent<Game_Controller>().gameState == Game_Controller.GameState.Playing;
        if (gamePlaying && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0)))
        {
            UpdateState("jump");
        }


    }

    public void UpdateState(string state = null )
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("Die");
            game.GetComponent<Game_Controller>().gameState = Game_Controller.GameState.Ended;
            enemyGeneration.SendMessage("cancelGeneration",true);

        }else if (other.gameObject.tag == "point")
        {

            game.SendMessage("IncreasePoints");
        }
    }
}
