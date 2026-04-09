using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Game339.Shared.Diagnostics;
using Game.Runtime;

public class TurnBasedCombat : MonoBehaviour
{
    private IGameLog _log;
    [SerializeField] private GameObject fighterPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform enemySpawnPoint;

    private GameObject player;
    private TurnBasedFighter playerFighter;
    private GameObject enemy;
    private TurnBasedFighter enemyFighter;

    [Header("Parry UI")]
    [SerializeField] private GameObject parryUI;
    [SerializeField] private Image parryBarFill;
    [SerializeField] private TextMeshProUGUI parryStatusText;

    [Header("Parry Settings")]
    [SerializeField] private float parryDuration = 1f; // Total time for the bar to fill from 0 to 1.0
    [SerializeField] private float parryWindowMin = 0.4f;
    [SerializeField] private float parryWindowMax = 0.8f;

    private bool isParryWindowActive = false;
    private float parryProgress = 0f;
    private bool hasInputBeenDetected = false;

    
    
    
    public enum GameTurn
    {
        Player,
        Enemy,
    }

    public GameTurn currentTurn = GameTurn.Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _log = ServiceResolver.Resolve<IGameLog>();
        _log.Info("TurnBasedCombat initialized");
        InitTurnBasedCombat(FishContainer.GetRandomFish(), FishContainer.GetRandomFish());
    }



    public void InitTurnBasedCombat(FishDataObj playerFish, FishDataObj enemyFish)
    {
        _log.Info($"Initializing combat: Player={playerFish.FishName}, Enemy={enemyFish.FishName}");
        if (player == null)
        {
            player = Instantiate(fighterPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        }
        playerFighter = player.GetComponent<TurnBasedFighter>();
        playerFighter.InitFishFighter(playerFish);

        if (enemy == null)
        {
            enemy = Instantiate(fighterPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
        }
        enemyFighter = enemy.GetComponent<TurnBasedFighter>();
        enemyFighter.InitFishFighter(enemyFish);
        enemyFighter.FlipFishSprite();
    }


    public void AttachUI()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurn == GameTurn.Player)
        {
            OnPlayerTurn();
        }

        if (currentTurn == GameTurn.Enemy)
        {
            if (!isParryWindowActive)
            {
                OnEnemyTurn();
            }
            else
            {
                HandleParryLogic();
            }
        }

        
    }

    private void HandleParryLogic()
    {
        // Increment progress based on time relative to total duration
        parryProgress += Time.deltaTime / parryDuration;
        parryBarFill.fillAmount = Mathf.Clamp01(parryProgress);

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !hasInputBeenDetected)
        {
            hasInputBeenDetected = true;
            CheckParryTiming();
        }

        if (parryProgress >= 1f)
        {
            if (!hasInputBeenDetected)
            {
                OnParryFail("Miss!");
            }
            FinishEnemyTurn();
        }
    }

    private void CheckParryTiming()
    {
        if (parryProgress >= parryWindowMin && parryProgress <= parryWindowMax)
        {
            OnParrySuccess();
        }
        else
        {
            OnParryFail("Too Early/Late!");
        }
    }

    private void OnParrySuccess()
    {
        parryStatusText.text = "Parried!";
        _log.Info("Parry Success!");
        // Reduce damage or take no damage
        // We'll call enemy attack now but with reduced effect or handle it here
        enemyFighter.TakeDamage(0); // For now, maybe reflect damage or just block

        if (enemyFighter.fishData.Health <= 0)
        {
            OnPlayerWin();
        }
    }

    private void OnParryFail(string message)
    {
        parryStatusText.text = message;
        _log.Warn("Parry Fail: " + message);
        playerFighter.TakeDamage(enemyFighter.GetFishDamage());
        _log.Info("Enemy did: " + enemyFighter.GetFishDamage() + " Damage. Player HP now: " + playerFighter.fishData.Health);

        if (playerFighter.fishData.Health <= 0)
        {
            OnPlayerLose();
        }
    }

    private void FinishEnemyTurn()
    {
        isParryWindowActive = false;
        // Small delay could be added here to show the status text
        Invoke(nameof(ResetParryUI), 0.1f);
        
        // Only transition to player turn if game is not over
        if (playerFighter.fishData.Health > 0 && enemyFighter.fishData.Health > 0)
        {
            currentTurn = GameTurn.Player;
        }
    }

    private void ResetParryUI()
    {
        parryUI.SetActive(false);
        parryStatusText.text = "";
    }
    
    


    public void OnPlayerTurn()
    {
        
    }

    public void OnPlayerAttack()
    {
        enemyFighter.TakeDamage(playerFighter.GetFishDamage());
        _log.Info("player did: " +  playerFighter.GetFishDamage() + " Damage" + " Enemy HP now: " + enemyFighter.fishData.Health);

        if (enemyFighter.fishData.Health <= 0)
        {
            OnPlayerWin();
        }
        else
        {
            currentTurn = GameTurn.Enemy;
        }
    }
    

    public void OnPlayerWin()
    {
        _log.Info("Player Wins!");
        // Add additional win logic here (e.g., rewards, scene transition)
    }

    public void OnPlayerLose()
    {
        _log.Info("Player Lost!");
        // Add additional lose logic here (e.g., game over screen, restart)
    }

    public void OnEnemyTurn()
    {
        _log.Info("enemy turn starting");
        // Start enemy attack sequence which includes the parry bar
        OnEnemyAttack();
    }

    public void OnEnemyAttack()
    {
        enableParryBar();
    }

    public void enableParryBar()
    {
        parryUI.SetActive(true);
        parryBarFill.fillAmount = 0;
        parryProgress = 0;
        hasInputBeenDetected = false;
        parryStatusText.text = "Press SPACE!";
        isParryWindowActive = true;
    }
    
    
    
    
    


    public void SetPlayer(GameObject player)
    {
        this.player = player;
        if (player != null)
        {
            playerFighter = player.GetComponent<TurnBasedFighter>();
        }
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
        if (enemy != null)
        {
            enemyFighter = enemy.GetComponent<TurnBasedFighter>();
        }
    }

}
