using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject smaller;
    [SerializeField] Player player;
    [SerializeField] int points;
    private void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if(health <= 0)
        {
            player.AddPoints(points);
            if (smaller != null)
            {
                GameObject b = Instantiate(smaller, transform.position + transform.up*2, transform.rotation  );
                b.GetComponent<Rigidbody2D>().velocity = b.transform.right * 2;
                b = Instantiate(smaller, transform.position + transform.up * -2, transform.rotation );
                b.GetComponent<Rigidbody2D>().velocity = b.transform.right * -2;
            }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("bullet"))
        {
            this.health--;
        }
    }
}
