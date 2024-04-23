using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StatsSO")]
public class StatsSO : ScriptableObject
{
    [Header("Patrol State")]
    public float speed;
    public float cliffCheckDistance;
    public float obstacleDistance;

    [Header ("Player Detection")]
    public float  playerDetectionDist;
    public float detectionPauseTime;
    public float playerDetectedWaitTime;

    [Header("Charge State")]
    public float chargeTime;
    public float chargeSpeed;

    [Header("Melee Attack State")]
    public float meleeAttackDistance;
    public float damageAmount;
}
