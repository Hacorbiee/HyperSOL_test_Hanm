using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static playerController Instance { get; private set; }
    [SerializeField] private float movespeed;
    [SerializeField] private Transform[] firepoint;
    Rigidbody2D rb;
    public GameObject firepointPrefab;
    public Button buttonSt;
    public Button buttonQui;
    public Button buttonAg;
    public Button buttonP;
    public Text Score;
    public Text Lose;
    [SerializeField] private Vector2 _speed;
    private Vector2 trany;
    [SerializeField] private float fireRate = 1f; // Tốc độ bắn đạn, 1 đạn/giây
    private float fireTimer = 0f; // Biến đếm thời gian
    int curscore = 0;
    void Start()
    {
        trany = transform.position;
        rb = GetComponent<Rigidbody2D>();
        buttonSt.gameObject.SetActive(true);
        buttonQui.gameObject.SetActive(true);
        buttonP.gameObject.SetActive(false);
    }
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

    public void OnMove(InputValue inputValue)
    {
        _speed = inputValue.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = ("Score:" + curscore);

        Vector2 movement = _speed * movespeed;
        rb.velocity = new Vector2(movement.x, trany.y);

        // Tăng giá trị của fireTimer
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
        for (int i = 0; i < firepoint.Length; i++)
        {
            Instantiate(firepointPrefab, firepoint[i].position, firepoint[i].rotation);
            curscore++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Lose.gameObject.SetActive(true);
            Time.timeScale = 0f;
            buttonAg.gameObject.SetActive(true);
        }
    }
}
