using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    public int amountToPool = 16;

    private List<GameObject> poolObj = new List<GameObject>();

    [SerializeField] private GameObject pretab;  

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
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(pretab);
            obj.SetActive(false);
            poolObj.Add(obj);
        }
    }

    // Update is called once per frame
    public GameObject GetPoolObj()
    {
        for (int i = 0; i < poolObj.Count; i++)
        {
            if (!poolObj[i].activeInHierarchy)
            {
                return poolObj[i];
            }
        }
        return null;
    }
}
