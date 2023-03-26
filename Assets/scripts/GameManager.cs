using System.Collections;
using System.Collections.Generic;

using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //game objects(and their components) in game world
    public List<GameObject> ballGameObjects = new List<GameObject>();
    public List<Ball> ballScripts = new List<Ball>();
    List<Rigidbody2D> ballsRigidbodies = new List<Rigidbody2D>();
    public GameObject platformGameObject;

    //Scripts of game objects
    [SerializeField] Platform platfromScript;

    //PreFabs
    [SerializeField] GameObject ballPreFab;

    //Game Start and end
    public Vector3 ballOffsetFromPlatform = new Vector3(0, 0.3f,0);
    public TextMeshProUGUI gameStartText;

    //State Machine
    StateMachine stateMachine;
    public PreGameState preGame;
    public GameOnState gameOn;
    public GameEndState gameEnd;

    //Power-Ups
        //[SerializeField] int BallMultiplier;
    
    //lifes
    [SerializeField] Image lifesImage;
    int lifes = 3;
    [SerializeField] Sprite[] lifesSprites;
    
    //game finished
    [SerializeField] TextMeshProUGUI gameFinishedText;
    [SerializeField] GameObject blocks;
    
    //current power-ups statuses
    bool isBallSpedUp = false;
    bool isBallSlowedDown = false;
    bool isPlatformSpedUp = false;
    bool isPlatformSlowedDown = false;
    
    //Coroutine objects
    Coroutine BallSpeedUpCoroutineObject;
    Coroutine BallSpeedDownCoroutineObject;
    Coroutine PlatformSpeedUpCoroutineObject;
    Coroutine PlatformSpeedDownCoroutineObject;
    
    //Audio
    [SerializeField] AudioManager audioManager;
    private void Start()
    {
        stateMachine = new StateMachine();
        preGame = new PreGameState(stateMachine, this);
        gameOn = new GameOnState(stateMachine, this);
        gameEnd = new GameEndState(stateMachine, this);
        stateMachine.Initialize(preGame);

        //platformStartSpeed = platfromScript.platformSpeed;
        SpawnBallAtGameStart();
    }
    private void Update()
    {
        stateMachine.currentState.HandleInput();
        stateMachine.currentState.LogicUpdate();
    }    
    /// <summary>
    /// Resets the state of balls and the platform. Do not forget that this method is called in GameManager.LifeLost().
    /// </summary>
    public void GameReset()
    {
        StopAllCoroutines();
        isBallSpedUp = false;
        isBallSlowedDown = false;
        isPlatformSpedUp = false;
        isPlatformSlowedDown = false;
        platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
        //gameStarted = false;
        foreach (GameObject item in ballGameObjects)
        {
            Destroy(item);
        }
        ballGameObjects.Clear();
        ballsRigidbodies.Clear();
        ballScripts.Clear();
        SpawnBallAtGameStart();
    }
    /// <summary>
    /// Ends the game, meaning vanishes platform, blocks and balls. Does not subtract life, nor spawns a new ball hovering over the platform. Use this method ONLY when life goes to 0. If life does not drop to zero, and you want the game to continue, please use "GameManager.GameReset()". Do Not Forget that this method is called both in GameManager.LifeLost() and GameManager.LifeLostPowerUp().
    /// </summary>
    public void GameEnd()
    {
        blocks.gameObject.SetActive(false);
        lifesImage.gameObject.SetActive(false);
        platformGameObject.SetActive(false);
        gameFinishedText.gameObject.SetActive(true);
        foreach (GameObject item in ballGameObjects)
        {
            Destroy(item);
        }
        ballGameObjects.Clear();
    }
    void LifeLostPowerUp()
    {
        lifes--;
        if (lifes == 2)
        {
            lifesImage.sprite = lifesSprites[1];
        }
        else if (lifes == 1)
        {
            lifesImage.sprite = lifesSprites[0];
        }
        else if (lifes == 0)
        {
            stateMachine.ChangeState(gameEnd);
        }
    }
    /// <summary>
    /// Subtracts a life from player's life pool and Resets the game (GameManager.GameReset() is called), or ends it (GameManager.GameEnd() is called) in case life dropped to 0
    /// </summary>
    public void LifeLost()
    {
        lifes--;
        if (lifes == 2)
        {
            lifesImage.sprite = lifesSprites[1];
            stateMachine.ChangeState(preGame);
        }
        else if (lifes == 1)
        {
            lifesImage.sprite = lifesSprites[0];
            stateMachine.ChangeState(preGame);
        }
        else if (lifes == 0)
        {
            stateMachine.ChangeState(gameEnd);
        }
    }
    
    void LifeUpPowerUp()
    {
        if (lifes >= 3)
        {
            return;
        }
        LifeUp();
    }
    public void LifeUp()
    {
        lifes++;
        if (lifes == 3)
        {
            lifesImage.sprite = lifesSprites[2];
        }
        else if (lifes == 2)
        {
            lifesImage.sprite = lifesSprites[1];
        }
        else if (lifes == 1)
        {
            lifesImage.sprite = lifesSprites[0];
        }
    }

    
    void SpawnBallAtGameStart()
    {
        GameObject tempBallPrefab = Instantiate(ballPreFab);
        ballGameObjects.Add(tempBallPrefab);
        ballsRigidbodies.Add(tempBallPrefab.GetComponent<Rigidbody2D>());
        ballScripts.Add(tempBallPrefab.GetComponent<Ball>());
        ballScripts[ballScripts.Count-1].AssignVariables(this, audioManager);
    }
    IEnumerator PlatformSpeedUpCoroutine()
    {
        platfromScript.platformSpeed = StaticConstants.PlatformSpedUpSpeed;
        isPlatformSpedUp = true;
        yield return new WaitForSeconds(5);
        platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
        isPlatformSpedUp = false;
    }
    void PlatformSpeedUpPickedUp()
    {
        if (isPlatformSpedUp)
        {
            StopCoroutine(PlatformSpeedUpCoroutineObject);
            platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
            PlatformSpeedUpCoroutineObject = StartCoroutine(PlatformSpeedUpCoroutine());
        }
        else if (isPlatformSlowedDown)
        {
            StopCoroutine(PlatformSpeedDownCoroutineObject);
            platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
            PlatformSpeedUpCoroutineObject = StartCoroutine(PlatformSpeedUpCoroutine());
        }
        else
        {
            PlatformSpeedUpCoroutineObject = StartCoroutine(PlatformSpeedUpCoroutine());
        }        
    }
    IEnumerator PlatformSpeedDownCoroutine()
    {        
        platfromScript.platformSpeed = StaticConstants.PlatformSlowedDownSpeed;
        isPlatformSlowedDown = true;
        yield return new WaitForSeconds(5);
        platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
        isPlatformSlowedDown = false;
    }
    void PlatformSpeedDownPickedUp()
    {
        if (isPlatformSlowedDown)
        {
            StopCoroutine(PlatformSpeedDownCoroutineObject);
            platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
            PlatformSpeedDownCoroutineObject = StartCoroutine(PlatformSpeedDownCoroutine());
        }
        else if (isPlatformSpedUp)
        {
            StopCoroutine(PlatformSpeedUpCoroutineObject);
            platfromScript.platformSpeed = StaticConstants.PlatformStartSpeed;
            PlatformSpeedDownCoroutineObject= StartCoroutine(PlatformSpeedDownCoroutine());
        }
        else
        {
            PlatformSpeedDownCoroutineObject = StartCoroutine(PlatformSpeedDownCoroutine());
        }
        
    }
    IEnumerator BallSpeedUpCoroutine()
    {
        Ball.currentBallSpeed = StaticConstants.BallSpedUpSpeed;
        foreach (Rigidbody2D item in ballsRigidbodies)
        {
            item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
        }        
        isBallSpedUp = true;        
        yield return new WaitForSeconds(5);
        Ball.currentBallSpeed = StaticConstants.BallStartSpeed;
        foreach (Rigidbody2D item in ballsRigidbodies)
        {
            item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
        }
        isBallSpedUp = false;
    }
    void BallSpeedUpPickedUp()
    {
        if (isBallSpedUp)
        {
            StopCoroutine(BallSpeedUpCoroutineObject);
            Ball.currentBallSpeed = StaticConstants.BallStartSpeed;
            foreach (Rigidbody2D item in ballsRigidbodies)
            {
                item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
            }
            BallSpeedUpCoroutineObject = StartCoroutine(BallSpeedUpCoroutine());
        }
        if (isBallSlowedDown)
        {
            StopCoroutine(BallSpeedDownCoroutineObject);
            Ball.currentBallSpeed = StaticConstants.BallStartSpeed;
            foreach (Rigidbody2D item in ballsRigidbodies)
            {
                item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
            }
            BallSpeedUpCoroutineObject = StartCoroutine(BallSpeedUpCoroutine());
        }
        else
        {
            BallSpeedUpCoroutineObject = StartCoroutine(BallSpeedUpCoroutine());
        }
    }
    IEnumerator BallSpeedDownCoroutine()
    {
        Ball.currentBallSpeed = StaticConstants.BallSlowedDownSpeed;
        foreach (Rigidbody2D item in ballsRigidbodies)
        {
            item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
        }
        isBallSlowedDown = true;
        yield return new WaitForSeconds(5);
        Ball.currentBallSpeed = StaticConstants.BallStartSpeed;
        foreach (Rigidbody2D item in ballsRigidbodies)
        {
            item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
        }
        isBallSlowedDown = false;
    }
    void BallSpeedDownPickedUp()
    {
        if (isBallSlowedDown)
        {
            StopCoroutine(BallSpeedDownCoroutineObject);
            Ball.currentBallSpeed = StaticConstants.BallStartSpeed; 
            foreach (Rigidbody2D item in ballsRigidbodies)
            {
                item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
            }
            BallSpeedDownCoroutineObject = StartCoroutine(BallSpeedDownCoroutine());
        }
        else if (isBallSpedUp)
        {
            StopCoroutine(BallSpeedUpCoroutineObject);
            Ball.currentBallSpeed = StaticConstants.BallStartSpeed;
            foreach (Rigidbody2D item in ballsRigidbodies)
            {
                item.velocity = item.velocity.normalized * Ball.currentBallSpeed;
            }
            BallSpeedDownCoroutineObject = StartCoroutine(BallSpeedDownCoroutine());
        }
        else
        {
            BallSpeedDownCoroutineObject = StartCoroutine(BallSpeedDownCoroutine());
        }
    }
    
    
    public void ZebranoWzmocnienie(PowerUpType powerUpType)
    {
        if (powerUpType == PowerUpType.PlatfromSpeedUp)
        {
            PlatformSpeedUpPickedUp();
        }
        else if (powerUpType == PowerUpType.PlatfromSpeedDown)
        {
            PlatformSpeedDownPickedUp();
        }
        else if (powerUpType == PowerUpType.BallSpeedUp)
        {
            BallSpeedUpPickedUp();
        }
        else if (powerUpType == PowerUpType.BallSpeedDown)
        {
            BallSpeedDownPickedUp();
        }
        else if (powerUpType == PowerUpType.LifeUp)
        {
            LifeUpPowerUp();
        }
        else if (powerUpType == PowerUpType.LifeDown)
        {
            LifeLostPowerUp();
        }
    }
}