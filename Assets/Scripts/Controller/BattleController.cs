using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BattleState
{
    SETUP,
    PLAYERTURN,
    ENEMYTURN,
    STANDOFF,
    WON,
    LOST
}

public class BattleController : MonoBehaviour
{
    public BattleState state;

    Hero playerHero;
    Hero enemyHero;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerLocation;
    public Transform enemyLocation;

    public HUDController playerHUD;
    public HUDController enemyHUD;

    public GameObject playerButtons;
    public GameObject enemyButtons;

    public string[] playerMoves = new string[3];
    public string[] enemyMoves = new string[3];

    int playerTurnCount = 0;
    int enemyTurnCount = 0;

    void Start()
    {
        StartCoroutine(SetupGame());
    }

    IEnumerator SetupGame()
    {
        state = BattleState.SETUP;
        // HIDE BUTTONS
        playerButtons.SetActive(false);
        enemyButtons.SetActive(false);

        // INSTANTIATE PLAYER
        GameObject playerGO = Instantiate(playerPrefab, playerLocation);
        GameObject enemyGO = Instantiate(enemyPrefab, enemyLocation);
        playerHero = playerGO.GetComponent<Hero>();
        enemyHero = enemyGO.GetComponent<Hero>();
        // MANAGE HUD

        playerHUD.SetHUD(playerHero);
        enemyHUD.SetHUD(enemyHero);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        playerButtons.SetActive(true);
    }


    // PLAYER AND ENEMY TURN
    public void HandleClick()
    {
        string selectedMove = EventSystem.current.currentSelectedGameObject.tag;
        Debug.Log(selectedMove);

        // PLAYER TURN
        if (state == BattleState.PLAYERTURN && playerTurnCount != 3)
        {
            playerMoves[playerTurnCount] = selectedMove;
            playerTurnCount += 1;

            if (playerTurnCount == 3)
            {
                state = BattleState.ENEMYTURN;
                playerButtons.SetActive(false);
                enemyButtons.SetActive(true);
                playerTurnCount = 0;
            }
        }
        else if (state == BattleState.ENEMYTURN && enemyTurnCount != 3)
        {
            enemyMoves[enemyTurnCount] = selectedMove;
            enemyTurnCount += 1;

            if (enemyTurnCount == 3)
            {
                enemyButtons.SetActive(false);
                enemyTurnCount = 0;
                StartCoroutine(StandOff());
            }
        }
        // ENEMY TURN

    }


    IEnumerator StandOff()
    {
        state = BattleState.STANDOFF;
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 3; i++)
        {
            string player = playerMoves[i];
            string enemy = enemyMoves[i];


            if (player == "Scissors")
            {
                if (enemy == "Scissors") BothDamage(50);
                else if (enemy == "Rock") TwentyDamage(enemyHero, playerHero.GetDamage(0.2f));
                else if (enemy == "Paper") FullDamage(enemyHero, playerHero.GetDamage());
            }
            else if (player == "Rock")
            {
                if (enemy == "Scissors") TwentyDamage(playerHero, enemyHero.GetDamage(0.2f));
                else if (enemy == "Rock") print("DRAW");
                else if (enemy == "Paper") FullDamage(playerHero, enemyHero.GetDamage());
            }
            else if (player == "Paper")
            {
                if (enemy == "Scissors") FullDamage(playerHero, enemyHero.GetDamage());
                else if (enemy == "Rock") FullDamage(enemyHero, playerHero.GetDamage());
                else if (enemy == "Paper") print("DRAW");
            }


            // SHOW ANIMATION OF CLASHING 

            // ANNOUNCE WHAT HAPPENED

            // UPDATE HUD
            yield return new WaitForSeconds(0.4f);
            playerHUD.SetHP(playerHero.currentHealth);
            enemyHUD.SetHP(enemyHero.currentHealth);
            // Check if there is a winner 
            if (playerHero.currentHealth <= 0)
            {
                StartCoroutine(AnnounceResult(BattleState.LOST, "YOU LOSE!"));
                break;
            }
            else if (enemyHero.currentHealth <= 0)
            {
                StartCoroutine(AnnounceResult(BattleState.WON, "YOU WIN!"));
                break;
            }


            // Change State to Player TURN
        }

        if (state != BattleState.WON || state != BattleState.LOST)
        {
            state = BattleState.PLAYERTURN;
            playerButtons.SetActive(true);
        }

    }


    // MOVES STAND OFF FUNCTIONS

    void FullDamage(Hero targetHero, int damage)
    {
        targetHero.TakeDamage(damage);
    }

    void TwentyDamage(Hero targetHero, int damage)
    {
        targetHero.TakeDamage(damage);
    }

    void BothDamage(int damage)
    {
        playerHero.TakeDamage(damage);
        enemyHero.TakeDamage(damage);
    }

    IEnumerator AnnounceResult(BattleState finalState, string resultMsg)
    {
        state = finalState;
        EndBattle();
        print(resultMsg);
        yield return new WaitForSeconds(2f);
    }

    void EndBattle()
    {
        Destroy(playerButtons);
        Destroy(enemyButtons);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHero.currentHealth -= 50;
            playerHUD.SetHP(playerHero.currentHealth);
            enemyHero.currentHealth -= 50;
            enemyHUD.SetHP(enemyHero.currentHealth);
        }
    }
}
