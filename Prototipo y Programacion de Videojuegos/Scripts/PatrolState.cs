using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer, obstacleLayer, playerLayer;

    bool TurningRight;
    bool playerDetected;

    public float raycastDistance, obstacleDistance, playerDetectionDist;
    public float speed;

    public float detectionPauseTime;
    public GameObject alert;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TurningRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForObstacles();
        CheckforPlayer();
    }

    void CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitObstacle = Physics2D.Raycast(ledgeDetector.position, Vector2.right, obstacleDistance, obstacleLayer);

        if (hit.collider == null || hitObstacle.collider == true)
        {
            Rotate();
        }
    }

    void CheckforPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(ledgeDetector.position, (TurningRight ? Vector2.right : Vector2.left), playerDetectionDist, playerLayer);
        if(hitPlayer.collider == true)
        {
            StartCoroutine(PlayerDetected());
        }
    }

    IEnumerator PlayerDetected()
    {
        playerDetected = true;
        rb.velocity = Vector2.zero;
        alert.SetActive(true);
        yield return new WaitForSeconds(1);

        Debug.Log("TE VI!");
    }

    private void FixedUpdate()
    {
        if (!playerDetected)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    void Rotate()
    {
        TurningRight = !TurningRight;
        transform.Rotate(0, 180, 0);
        speed = -speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ledgeDetector.position, (TurningRight ? Vector2.right : Vector2.left) * playerDetectionDist);
    }
}
