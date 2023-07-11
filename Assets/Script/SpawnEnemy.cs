using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy Instance { get; private set; }
    [SerializeField] private float moveSpeed = 5f;   // Tốc độ di chuyển của enemy
    [SerializeField] private Transform spawnPoint;
    private int rows = 4;    // Số hàng trong xếp hàng
    private int columns = 4; // Số cột trong xếp hàng
    [SerializeField] public Transform[] waypoints1;
    [SerializeField] public Transform[] waypoints2;
    [SerializeField] public Transform[] waypoints3;
    List<GameObject> enemies = new List<GameObject>();
    public bool canHit = false;
    int waypointIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {


        // Sinh ra các enemy từ Object Pool
        for (int i = 0; i < rows * columns; i++)
        {
            GameObject enemy = ObjectPool.Instance.GetPoolObj();
            if (enemy != null)
            {
                enemy.transform.position = spawnPoint.position;
                enemy.SetActive(true);
                enemies.Add(enemy);
            }
        }
        StartCoroutine(SortAndMoveEnemies());
    }
    private bool CheckAnyEnemyActive()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
    private void Update()
    {
        CheckAnyEnemyActive();
        // Kiểm tra xem có còn enemy nào active hay không
        if (CheckAnyEnemyActive())
        {
            // Có enemy active
            return;
        }
        else
        {
            canHit = false;
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
                
            }
            StartCoroutine(SortAndMoveEnemies());
        }

    }

    private IEnumerator SortAndMoveEnemies()
    {


        // Sắp xếp các enemy thành hình vuông 4x4
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                if (index < enemies.Count)
                {
                    Vector3 targetPositionSquare = CalculateTargetPositionSquare(i, j);
                    yield return StartCoroutine(MoveEnemy(enemies[index], targetPositionSquare));
                }
            }
        }

        // Chờ 3 giây
        yield return new WaitForSeconds(3f);
        foreach (GameObject enemy in enemies)
        {
            if (waypointIndex < waypoints1.Length)
            {
                Vector3 targetPosition = waypoints1[waypointIndex].position;
                yield return StartCoroutine(MoveEnemy(enemy, targetPosition));
                waypointIndex++;
            }
        }

        yield return new WaitForSeconds(3f);
        waypointIndex = 0;
        foreach (GameObject enemy in enemies)
        {
            if (waypointIndex < waypoints2.Length)
            {
                Vector3 targetPosition = waypoints2[waypointIndex].position;
                yield return StartCoroutine(MoveEnemy(enemy, targetPosition));
                waypointIndex++;
            }
        }

        yield return new WaitForSeconds(3f);
        waypointIndex = 0;
        foreach (GameObject enemy in enemies)
        {
            if (waypointIndex < waypoints3.Length)
            {
                Vector3 targetPosition = waypoints3[waypointIndex].position;
                yield return StartCoroutine(MoveEnemy(enemy, targetPosition));
                waypointIndex++;
            }
        }
         canHit = true;
}


    private Vector3 CalculateTargetPositionSquare(int row, int column)
    {
        // Tính toán vị trí xếp hàng theo hình vuông
        Vector3 targetPosition = spawnPoint.position + new Vector3(column * 1, -row * 1, 0f);
        return targetPosition;
    }



    private IEnumerator MoveEnemy(GameObject enemy, Vector3 targetPosition)
    {
        // Di chuyển enemy từ vị trí hiện tại đến vị trí xếp hàng
        while (enemy.transform.position != targetPosition)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
