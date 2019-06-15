using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public EnemyModel archmageEye;
    public PlayerModel pModel;
    public Inventory inv;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject hud;
    public UnityEvent UpdatePlayerHealthEvent;
    public UnityEvent UpdateGoblinHealthEvent;
    public UnityEvent UpdateBossHealthEvent;
    public UnityEvent UpdatePlayerSpeedEvent;
    public UnityEvent UpdatePlayerInputEvent;
    public UnityEvent UpdatePlayerSpawnPointEvent;
    public static bool gameIsPaused = false;
    private bool superFast = false;
    private float isTime = 0;
    private float normalSpeed=0;

    private class GameState
    {
        public float healthPlayer;
        public float points;
        public float healthBoss;
        public float healthGoblin;
        public float speedPlayer;
        public bool isFlash;
        public bool inputDisabled;
        public Transform spawnPoint;
    }
    private GameState gameState = new GameState();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitGame();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance!=this)
                Destroy(gameObject);
        }
    }

    private void InitGame()
    {
        inv = new Inventory();

        gameState.points = pModel.points;
        gameState.healthPlayer = pModel.health;
        gameState.speedPlayer = pModel.normalSpeed;
        gameState.isFlash = pModel.isFlash;
        gameState.inputDisabled = false;
        gameState.spawnPoint = transform;
        gameState.healthBoss = archmageEye.health;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }  
        }

        if (superFast)
        {
            isTime += Time.deltaTime;
            if (isTime >= 10)
            {
                UpdatePlayerSpeed(normalSpeed);
                superFast = false;
            }
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0.5f;

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Castle");
    }

    public void LoadMainMenu()
    {
        pauseMenu.SetActive(false);
        hud.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public EnemyModel GetArchmageEyeModel()
    {
        if (archmageEye != null)
            return archmageEye;
        else
            return null;
    }

    public void UpdatePlayerHealth(float dec)
    {
        
        gameState.healthPlayer += dec;
        UpdatePlayerHealthEvent.Invoke();
    }

    public void UpdatePlayerPoints(float dec)
    {
        gameState.points += dec;
       // UpdatePlayerPoints.Invoke();
    }

    public void UpdateBossHealth(float dec)
    {
        gameState.healthBoss += dec;
        UpdateBossHealthEvent.Invoke();
    }

    public float GetHealth()
    {
        return gameState.healthPlayer;
    }

    public float GetPlayerPoints()
    {
        return gameState.points;
    }

    public float GetBossHealth()
    {
        return gameState.healthBoss;
    }

    public float GetPlayerSpeed()
    {
        return gameState.speedPlayer;
    }

    public bool GetPlayerFlashState()
    {
        return gameState.isFlash;
    }

    public void UpdatePlayerSpeed(float newSpeed)
    {
        gameState.speedPlayer = newSpeed;
        UpdatePlayerSpeedEvent.Invoke();
        
    }

    public void UpdatePlayerInput(bool newInputState)
    {
        gameState.inputDisabled = newInputState;
        UpdatePlayerInputEvent.Invoke();
    }

    public bool GetPlayerInputState()
    {
        return gameState.inputDisabled;
    }

    public void UpdatePlayerSpawnPoint(Transform newSpawnPoint)
    {
        gameState.spawnPoint = newSpawnPoint.transform;
        UpdatePlayerSpawnPointEvent.Invoke();
    }

    public Transform GetPlayerSpawnPoint()
    {
        return gameState.spawnPoint;
    }

    public void SpeedUp(float newSpeed)
    {
        normalSpeed = GetPlayerSpeed();
        superFast = true;
        gameState.speedPlayer = newSpeed;
        UpdatePlayerSpeedEvent.Invoke();
    }
}
