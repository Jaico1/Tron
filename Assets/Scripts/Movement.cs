using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    //Identificadores de las teclas para movimiento
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode shootKey;

    //Identificadores del jugador
    public bool playerOne;
    public bool playerTwo;

    //Velocidad de movimiento
    public float speed = 16;

    //Prefabs que se van a utilizar
    public GameObject trailPrefab;
    public Bullet bulletPrefab;

    //Verificacion de disponibilidad de disparo
    private bool shootAvailable = true;

    //Integers para las vidas de cada jugador
    private int livesOne;
    private int livesTwo;

    //Colision del rastro del jugador
    Collider2D trail;
    BoxCollider2D bulletCol;

    //Verificacion de posicion del ultimo cambio de direccion
    Vector2 lastTrailEnd;


    //Funcion Start donde definimos el color inicial y movimiento inicial de ambos jugadores
    void Start()
    {
        livesOne = PlayerPrefs.GetInt("livesOne");
        livesTwo = PlayerPrefs.GetInt("livesTwo");
        UIManager.Instance.UpdateScores(PlayerPrefs.GetInt("livesOne"), PlayerPrefs.GetInt("livesTwo"));

        //Definicion de colores de rastro inicial
        if (playerOne)
        {
            this.trailPrefab.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("redOne"), PlayerPrefs.GetFloat("greenOne"), PlayerPrefs.GetFloat("blueOne"));
        }
        else if (playerTwo)
        {
            this.trailPrefab.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("redTwo"), PlayerPrefs.GetFloat("greenTwo"), PlayerPrefs.GetFloat("blueTwo"));
        }

        //Comienzan moviendose hacia arriba
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnTrail();
    }

    //Funcion Update que se repite cada frame
    void Update()
    {
        //Verificacion de disparo
        if (Input.GetKeyDown(shootKey) && shootAvailable == true)
        {
            shootAvailable = false;
            Bullet b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 2;
            bulletCol = b.GetComponent<BoxCollider2D>();
            StartCoroutine(Coroutine());

        }

        //Verificacion de color de rastro
        if (playerOne)
        {
            this.trailPrefab.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("redOne"), PlayerPrefs.GetFloat("greenOne"), PlayerPrefs.GetFloat("blueOne"));
        }
        else if (playerTwo)
        {
            this.trailPrefab.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat("redTwo"), PlayerPrefs.GetFloat("greenTwo"), PlayerPrefs.GetFloat("blueTwo"));
        }

        //Verificacion de movimiento del jugador
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

        //Creacion de rastro/pared entre el ultimo punto y el jugador
        fitColliderBetween(trail, lastTrailEnd, transform.position);
    }

    //Funcion para generar un bloque de rastro/pared
    void spawnTrail()
    {
        lastTrailEnd = transform.position;

        GameObject t = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        trail = t.GetComponent<Collider2D>();
    }

    //Funcion para expandir el colisionador del rastro/pared
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

    //Funcion para verificar colisiones OnTrigger
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co != trail && co!= bulletCol)
        {
            GetComponent<Movement>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if(livesOne > 1 && livesTwo > 1)
            {
                if (playerOne)
                {
                    PlayerPrefs.SetInt("livesOne", livesOne-1);
                }
                else if (playerTwo)
                {
                    PlayerPrefs.SetInt("livesTwo", livesTwo - 1);
                }
                UIManager.Instance.UpdateScores(PlayerPrefs.GetInt("livesOne"), PlayerPrefs.GetInt("livesTwo"));
                StartCoroutine(CoroutineDeath());
            }
            else
            {
                if (playerOne)
                {
                    PlayerPrefs.SetInt("livesOne", livesOne - 1);
                }
                else if (playerTwo)
                {
                    PlayerPrefs.SetInt("livesTwo", livesTwo - 1);
                }
                UIManager.Instance.UpdateScores(PlayerPrefs.GetInt("livesOne"), PlayerPrefs.GetInt("livesTwo"));
                if (playerOne)
                {
                    PlayerPrefs.SetInt("win", 1);
                }else if (playerTwo)
                {
                    PlayerPrefs.SetInt("win", 0);
                }
                StartCoroutine(CoroutineLoss());
            }
            
        }
    }

    //Subrutina para esperar varios segundos
    IEnumerator Coroutine()
    {
        int player;
        if (playerOne)
        {
            player = 1;
        }
        else
        {
            player = 0;
        }
        UIManager.Instance.UpdateShoot(true, player);
        yield return new WaitForSeconds(5);
        UIManager.Instance.UpdateShoot(false, player);
        shootAvailable = true;
    }

    IEnumerator CoroutineDeath()
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator CoroutineLoss()
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync("SampleScene");
        SceneManager.LoadScene("VictoryScene");
    }
}
