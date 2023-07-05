using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public Transform[] waypoints;

    private int currentWaypointIndex;
    public int enemyIndex;
    private bool cur;
    private bool allEnemiesFinished;
    private bool movingUp;

    private void Start()
    {
        cur = false;
        allEnemiesFinished = false;
        movingUp = true;
        StartCoroutine(ChangeDirectionCoroutine());
    }

    private void Update()
    {
        if (enemyIndex == CurrentEnemyindex.Instance.currentEnemyindex || cur)
        {
            cur = true;
            int nextWaypoint = currentWaypointIndex + 1;
            if (nextWaypoint < waypoints.Length)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWaypoint].position, moveSpeed * Time.deltaTime);
                if (transform.position == waypoints[nextWaypoint].position)
                {
                    currentWaypointIndex = nextWaypoint;
                }
            }
            else
            {
                // Kiểm tra nếu tất cả enemy đã hoàn thành các waypoint
                if (!allEnemiesFinished)
                {
                    allEnemiesFinished = CheckAllEnemiesFinished();
                }

                if (allEnemiesFinished)
                {
                    float yOffset = 0.003f * (movingUp ? 1 : -1);
                    transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

                }
            }
        }
    }

    private bool CheckAllEnemiesFinished()
    {
        // Kiểm tra nếu tất cả enemy đã hoàn thành các waypoint
        EnemyController[] enemyControllers = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemyController in enemyControllers)
        {
            if (enemyController.currentWaypointIndex < enemyController.waypoints.Length - 1)
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator ChangeDirectionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            movingUp = !movingUp;
        }
    }
    public void TakeDame()
    {

        if (allEnemiesFinished)
        {
            Destroy(gameObject);

        }
        
    }
    
}
