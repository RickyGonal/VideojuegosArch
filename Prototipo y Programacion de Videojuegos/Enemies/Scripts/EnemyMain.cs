using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    #region Variables
    public EnemyBaseState currentState;

    public PatrolState patrolState;
    public PlayerDetected playerDetectedState;
    public ChargeState chargeState;
    public MeleeAttackState meleeAttackState;
    public ShootingState shootingState;

    public Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer, obstacleLayer, playerLayer, damageablePlayer;

    public int facingDirection;


    public GameObject alert;

    public float stateTime; //How Much TIme I'm in this state

    public StatsSO stats;

    #endregion

    #region Unity Callbacks
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }

    private void Awake()
    {
        patrolState = new PatrolState(this, "patrol");
        playerDetectedState = new PlayerDetected(this, "playerDetected");
        chargeState = new ChargeState(this, "charge");
        meleeAttackState = new MeleeAttackState(this, "meleeAttack");
        shootingState = new ShootingState(this, "shotting");

        currentState = patrolState;
        currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        //New
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        //New
        currentState.PhysicsUpdate();
    }

    #endregion

    #region Checks

    public bool CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, stats.cliffCheckDistance, groundLayer);
        RaycastHit2D hitObstacle = Physics2D.Raycast(ledgeDetector.position, Vector2.right, stats.obstacleDistance, obstacleLayer);

        if (hit.collider == null || hitObstacle.collider == true)
            return true;
        else
            return false;
    }

    public bool CheckforPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(ledgeDetector.position, (facingDirection == 1? Vector2.right : Vector2.left), stats.playerDetectionDist, playerLayer);
        if(hitPlayer.collider == true)
            return true;
        else 
            return false;
    }

    public bool CheckForMeleeTarget()
    {
        RaycastHit2D hitMeleeTarget = Physics2D.Raycast(ledgeDetector.position, (facingDirection == 1 ? Vector2.right : Vector2.left), stats.meleeAttackDistance, playerLayer);
        if (hitMeleeTarget.collider == true)
            return true;
        else
            return false;
    }

    #endregion

    #region Other Functions

    public void SwitchState(EnemyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        stateTime = Time.time;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ledgeDetector.position, (facingDirection == 1 ? Vector2.right : Vector2.left) * 2);
    }*/

    #endregion
}
