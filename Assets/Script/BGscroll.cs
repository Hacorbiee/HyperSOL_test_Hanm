using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer mesh;
    public float speed = 0.1f;

    private void Start()
    {

    }
    private void Update()
    {
        Vector2 offset = mesh.material.mainTextureOffset;
        offset = offset + new Vector2 (0, speed * Time.deltaTime);
        mesh.material.mainTextureOffset = offset;

    }
}
