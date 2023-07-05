using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    private Vector3 startpo;

    private void Start()
    {
        startpo = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed*Time.deltaTime);
        if(transform.position.y< -21.7209)
        {
            transform.position = startpo;
        }

    }
}
