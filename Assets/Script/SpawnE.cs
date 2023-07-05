using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnE : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform firepoint;
    [SerializeField] private float fireRate = 1f; // Tốc độ bắn đạn, 1 đạn/giây
    private float fireTimer = 0f; // Biến đếm thời gian
    public GameObject firepointPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        // Kiểm tra nếu đã đủ thời gian để bắn đạn
        if (fireTimer >= fireRate)
        {
            FireBullet();
            fireTimer = 0f; // Đặt lại giá trị fireTimer về 0 để bắt đầu đếm lại
        }
    }
    void FireBullet()
    {
            Instantiate(firepointPrefab, firepoint.position, firepoint.rotation);
    }
}
