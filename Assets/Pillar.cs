using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float dist;
    void Start()
    {
        
    }
    public bool isAllowed = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAllowed)
        {
            transform.position += Vector3.left * dist * Time.deltaTime;
        }
    }
}
