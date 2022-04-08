using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode shootKey;
    public bool playerOne;
    public bool playerTwo;

    public float speed = 16;

    public GameObject trailPrefab;

    private bool shootAvailable = true;

    Collider2D trail;

    Vector2 lastTrailEnd;



    void Start()
    {
        if (playerOne)
        {
            trailPrefab.GetComponent<SpriteRenderer>().color = Color.cyan;
        }else if (playerTwo)
        {
            trailPrefab.GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnTrail();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && shootAvailable == true)
        {
            shootAvailable = false;
            Debug.Log("Acaba de disparar.");
            StartCoroutine(Coroutine());

        }
        
        if (Input.GetKeyDown(upKey) && GetComponent<Rigidbody2D>().velocity.y >=0 )
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnTrail();
        }
        else if (Input.GetKeyDown(downKey)&&  GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            spawnTrail();
        }
        else if (Input.GetKeyDown(rightKey) && GetComponent<Rigidbody2D>().velocity.x >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnTrail();
        }
        else if (Input.GetKeyDown(leftKey) && GetComponent<Rigidbody2D>().velocity.x <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            spawnTrail();
        }

        fitColliderBetween(trail, lastTrailEnd, transform.position);
    }

    void spawnTrail()
    {
        lastTrailEnd = transform.position;

        GameObject t = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        trail = t.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;

        float dist = Vector2.Distance(a, b);

        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist+1, 1);

        }
        else
        {
            co.transform.localScale = new Vector2(1, dist+1);
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co != trail)
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<Movement>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(5);
        shootAvailable = true;
        Debug.Log("Acaba de recuperar el disparo.");
    }
}
