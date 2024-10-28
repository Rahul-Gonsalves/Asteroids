using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] float spawnProt;
    [SerializeField] int points;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float angleChange;
    [SerializeField] float acceleration;
    private bool hitAsteroid;
    Vector3 original = new Vector3(0,0,0);
    [SerializeField] int lives;
    [SerializeField] SpriteRenderer front;
    [SerializeField] SpriteRenderer back;
    [SerializeField] SpriteRenderer back1;
    [SerializeField] SpriteRenderer flash;
    [SerializeField] GameObject bullet;
    float time;
    [SerializeField] float fireRate;
    [SerializeField] float speed;
    [SerializeField] Text pointsText;
    [SerializeField] Asteroids small;
    [SerializeField] Asteroids medium;
    [SerializeField] Asteroids big;
    private bool immune;
    float time1;
    [SerializeField] float spawnRate;

    // Start is called before the first frame update
    private bool frontOn;
    private bool backOn;    
    private bool flashOn;
    void Start()
    {
        this.points = 0;
        time = 0f;
        time1 = 10f;
        rb = GetComponent<Rigidbody2D>();
        lives = 3;
        frontOn = false;
        backOn = false;
        flashOn = false;
        pointsText.text = "Points: " + points;
        int angle = Random.Range(0, 360);
        Asteroids b = Instantiate(big, new Vector3(-30, -30, 0), Quaternion.identity);
        b.GetComponent<Rigidbody2D>().rotation = angle;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        time1 = spawnRate * 5;
    }
    private void Update()
    {
        
        if(lives == 2)
        {
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag("Life 1");
            if(objects.Length == 1)
                Destroy(objects[0]);    
        }
        if (lives == 1)
        {
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag("Life 2");
            if (objects.Length == 1)
                Destroy(objects[0]);
        }
        if (lives == 0)
        {
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag("Life 3");
            if (objects.Length == 1)
                Destroy(objects[0]);
           
        }


        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if(time1 > 0)
        {
            time1 -= Time.deltaTime;
        }
        //All the code for screen wrap
        Vector3 pos = transform.position;
        if(time1 <= 0)
        {
            int asteroid = Random.Range(0, 4);
            int angle = Random.Range(0, 360);
            if(asteroid == 1)
            {
                Asteroids b = Instantiate(small, new Vector3(-30, -30, 0), Quaternion.identity);
                b.GetComponent<Rigidbody2D>().rotation = angle;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5,5), Random.Range(-5, 5));
                time1 = spawnRate;
            }
            else if(asteroid == 2)
            {
                Asteroids b = Instantiate(medium, new Vector3(-30, -30, 0), Quaternion.identity);
                b.GetComponent<Rigidbody2D>().rotation = angle;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                time1 = spawnRate;
            }
            else if (asteroid == 3)
            {
                Asteroids b = Instantiate(big, new Vector3(-30, -30, 0), Quaternion.identity);
                b.GetComponent<Rigidbody2D>().rotation = angle;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), Random.Range(-10, 5));
                time1 = spawnRate;
            }
        }
        if(pos.x>20 || pos.x < -20)
        {
            if(pos.x > 20)
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            frontOn = true;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            frontOn = false;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            backOn = true;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            backOn = false;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            flashOn = true;
            if (time <= 0)
            {
                time = fireRate;
                GameObject b = Instantiate(bullet, transform.position + transform.right * 2f, transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = b.transform.right * speed;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && lives != 0)
            flashOn = false;
        pointsText.text = "Points: " + points;
        if (lives <= 0)
        {
            pointsText.text = "Game Over: \n Press R to restart";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (lives <= 0)
            {
                lives = 3;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }
    void FixedUpdate()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        if (lives >0)
        {
            move(y);

            transform.Rotate(0, 0, x * -angleChange);
            if (frontOn)
                this.front.enabled = true;
            else
                this.front.enabled = false;
            if(backOn)
            {
                this.back.enabled = true;
                this.back1.enabled = true;
            }
            else
            {
                this.back.enabled = false;
                this.back1.enabled = false;
            }
            if (flashOn)
                flash.enabled = true;
            else
                flash.enabled = false;
        }
        
    }
    void move(float speed)
    {
        Vector2 amount = transform.right * speed/ (1/acceleration);
        rb.AddForce(amount);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!immune)
        {
            if (collision.gameObject.tag.Equals("Big") || collision.gameObject.tag.Equals("Medium") || collision.gameObject.tag.Equals("Small"))
            {
                transform.position = original;
                hitAsteroid = true;
                immune = true;
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                lives--;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                StartCoroutine(SpawnProt1(spawnProt));
                



            }
        }

    }
    public void AddPoints(int point)
    {
        this.points = points + point;
    }
    IEnumerator SpawnProt1(float spawn)
    {
 
        yield return new WaitForSeconds(spawn);
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1.7f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(1.3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);


        immune = false;

        hitAsteroid = false;
        

    }

}
