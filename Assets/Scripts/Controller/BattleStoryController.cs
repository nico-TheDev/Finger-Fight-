using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class BattleStoryController : MonoBehaviour
{
    public BattleState state;

    BattleAnimationController battleAnim;
    public int currentRound = 1;
    public int currentStage;
    Hero playerHero;
    Hero enemyHero;

    public GameObject[] playerPrefab = new GameObject[3];
    public GameObject[] enemyPrefab = new GameObject[3];

    public Transform playerLocation;
    public Transform enemyLocation;

    public HUDController playerHUD;
    public HUDController enemyHUD;

    public GameObject playerButtons;
    public GameObject enemyButtons;

    GameObject playerGO;
    GameObject enemyGO;
    // SET IT TO PRIVATE IN PROD
    public string[] playerMoves = new string[3];
    public string[] enemyMoves = new string[3];

    int playerTurnCount = 0;
    int enemyTurnCount = 0;

    AudioSource audioFX;
    public AudioClip attackFX;

    public Image background;
    public Sprite[] stages = new Sprite[3];

    public SceneController scene;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoad;
    }

    void OnLoad(Scene scene, LoadSceneMode mode)
    {
        battleAnim = GetComponent<BattleAnimationController>();
        audioFX = gameObject.GetComponent<AudioSource>();
        // Save the current progress of the player
        SetupStage();
        StartCoroutine(SetupGame());
    }

    void SetupStage()
    {
        currentStage = PlayerPrefs.GetInt("currentStage");

        FindObjectOfType<AudioManager>().StopAll();

        // KNIGHT
        if (currentStage == 0)
        {
            PlayerPrefs.SetString("enemyChar", "Knight");
            FindObjectOfType<AudioManager>().Play("KnightStage");
            background.sprite = stages[currentStage];
        }
        // SAMURAI
        else if (currentStage == 1)
        {
            PlayerPrefs.SetString("enemyChar", "Samurai");
            FindObjectOfType<AudioManager>().Play("SamuraiStage");
            background.sprite = stages[currentStage];
        }
        // ALIEN
        else if (currentStage == 2)
        {
            PlayerPrefs.SetString("enemyChar", "Alien");
            FindObjectOfType<AudioManager>().Play("AlienStage");
            background.sprite = stages[currentStage];
        }

    }

    IEnumerator SetupGame()
    {
        Time.timeScale = 1;
        state = BattleState.SETUP;
        // HIDE BUTTONS
        playerButtons.SetActive(false);
        enemyButtons.SetActive(false);

        // SETUP PLAYER
        string playerChar = PlayerPrefs.GetString("playerChar");
        string enemyChar = PlayerPrefs.GetString("enemyChar");
        int playerIndex = 0;
        int enemyIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            if (playerPrefab[i].tag == playerChar)
            {
                playerIndex = i;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (enemyPrefab[i].tag == enemyChar)
            {
                enemyIndex = i;
            }
        }

        // INSTANTIATE PLAYER
        if (playerGO == null && enemyGO == null)
        {

            playerGO = Instantiate(playerPrefab[playerIndex], playerLocation);
            enemyGO = Instantiate(enemyPrefab[enemyIndex], enemyLocation);
            playerHero = playerGO.GetComponent<Hero>();
            enemyHero = enemyGO.GetComponent<Hero>();
        }
        // MANAGE HUD

        playerHUD.SetHUD(playerHero);
        enemyHUD.SetHUD(enemyHero);

        // INTRO ANIMATION

        battleAnim.PlayFinger();
        yield return new WaitForSeconds(1.2f);
        battleAnim.PlayFight();


        // yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        playerButtons.SetActive(true);
    }

    public void HandleClick()
    {
        StartCoroutine(Click());
    }
    // PLAYER AND ENEMY TURN
    public IEnumerator Click()
    {
        string selectedMove = EventSystem.current.currentSelectedGameObject.tag;
        // PLAYER TURN
        if (state == BattleState.PLAYERTURN && playerTurnCount != 3)
        {
            playerMoves[playerTurnCount] = selectedMove;
            playerTurnCount += 1;

            if (playerTurnCount == 3)
            {
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(0.5f);
                playerButtons.SetActive(false);
                playerTurnCount = 0;
                StartCoroutine(GetEnemyMoves());
            }
        }
    }

    IEnumerator GetEnemyMoves()
    {
        for (int i = 0; i < 3; i++)
        {
            string[] moves = { "Rock", "Paper", "Scissors" };
            int randomMove = Random.Range(0, 3);
            enemyMoves[enemyTurnCount] = moves[randomMove];
            enemyTurnCount += 1;
        }

        if (enemyTurnCount == 3)
        {
            enemyTurnCount = 0;
            print("ENEMY IS THINKING...");
            yield return new WaitForSeconds(3f);
            StartCoroutine(StandOff());
        }
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
            print($"{player} VS {enemy}");
            yield return new WaitForSeconds(0.4f);
            playerHUD.SetHP(playerHero.currentHealth);
            enemyHUD.SetHP(enemyHero.currentHealth);
            // Check if there is a winner 
            if (playerHero.currentHealth <= 0)
            {
                StartCoroutine(AnnounceResult(BattleState.LOST, "YOU LOSE!"));
                enemyHUD.addRoundWon();
                break;
            }
            else if (enemyHero.currentHealth <= 0)
            {
                StartCoroutine(AnnounceResult(BattleState.WON, "YOU WIN!"));
                playerHUD.addRoundWon();
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
        audioFX.PlayOneShot(attackFX);
    }

    void TwentyDamage(Hero targetHero, int damage)
    {
        targetHero.TakeDamage(damage);
        audioFX.PlayOneShot(attackFX);
    }

    void BothDamage(int damage)
    {
        playerHero.TakeDamage(damage);
        enemyHero.TakeDamage(damage);
        audioFX.PlayOneShot(attackFX);
    }

    IEnumerator AnnounceResult(BattleState finalState, string resultMsg)
    {
        state = finalState;
        print(resultMsg);
        yield return new WaitForSeconds(2f);
        EndBattle();
    }

    void EndBattle()
    {
        playerButtons.SetActive(false);
        playerHero.ResetHealth();
        enemyHero.ResetHealth();

        state = BattleState.SETUP;
        if (playerHUD.roundWonCount == 2)
        {
            playerHUD.displayRoundWon();
            enemyHUD.displayRoundWon();
            print("YOU WIN.BATTLE END");
            currentStage += 1;
            PlayerPrefs.SetInt("currentStage", currentStage);
            scene.LoadVersus();
        }
        else if (enemyHUD.roundWonCount == 2)
        {
            playerHUD.displayRoundWon();
            enemyHUD.displayRoundWon();
            print("YOU LOSE.BATTLE END");
            currentStage += 1;
            PlayerPrefs.SetInt("currentStage", currentStage);
            scene.LoadVersus();
        }
        else
        {
            playerHUD.displayRoundWon();
            enemyHUD.displayRoundWon();
            StartCoroutine(SetupGame());
        }
    }
}

