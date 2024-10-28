using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float time;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        if(time <= 0)
        {
            Destroy(gameObject);
        }
        Vector3 pos = transform.position;
        if (pos.x > 20 || pos.x < -20)
        {
            if (pos.x > 20)
                pos.x = 20;
            if (pos.x < -20)
                pos.x = -20;
            pos.x = -pos.x;
            transform.position = pos;
        }
        if (pos.y > 12.5 || pos.y < -12.5)
        {
            if (pos.y > 12.5)
                pos.y = 12.5f;
            if (pos.y < -12.5)
                pos.y = -12.5f;
            pos.y = -pos.y;
            transform.position = pos;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    { 
        Destroy (gameObject);
    }
}
