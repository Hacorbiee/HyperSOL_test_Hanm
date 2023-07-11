using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float movespeed;
    [SerializeField] private Vector2 direc;
    private bool hasHitEnemy;



    void Start()
    {
        hasHitEnemy = false;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(direc * Time.deltaTime * movespeed);
        Destroy(gameObject,6);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHitEnemy)
        {
            return; // Nếu viên đạn đã trúng quái vật, thoát khỏi hàm
        }
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();


        if (enemy!= null && SpawnEnemy.Instance.canHit)
        {

            enemy.TakeDame();
            hasHitEnemy = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boom"))
        {
            hasHitEnemy = true;
            Destroy(gameObject);
        }
    }

}
