using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnemyindex : MonoBehaviour
{
    // Start is called before the first frame update
    public static CurrentEnemyindex Instance { get; private set; }
    [SerializeField] private float fireRate = 0.25f; // Tốc độ bắn đạn, 1 đạn/giây
    private float fireTimer = 0f; // Biến đếm thời gian

    public int currentEnemyindex = 1;
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
    private void Update()
    {
        fireTimer += Time.deltaTime;
        // Kiểm tra nếu đã đủ thời gian để bắn đạn
        if (fireTimer >= fireRate)
        {
            currentEnemyindex++;

            fireTimer = 0f; // Đặt lại giá trị fireTimer về 0 để bắt đầu đếm lại
        }
    }
}
