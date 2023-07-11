using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void TakeDame()
    {
            gameObject.SetActive(false);
            transform.position = SpawnEnemy.Instance.transform.position;
            playerController.Instance.curscore++;

    }
    
}
