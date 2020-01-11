using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collisions : MonoBehaviour {

    private ParticleSystem blueBulletParticle;
    private ParticleSystem orangeBulletParticle;
    private ParticleSystem deathParticle;
    private GameObject mainMenu;
    private GameObject bluePlayer;
    private GameObject orangePlayer;
    private GameObject input;

    // Use this for initialization
    void Start () {
        bluePlayer = GameObject.Find("blue player");
        orangePlayer = GameObject.Find("orange player");
        mainMenu = GameObject.Find("main menu");
        input = GameObject.Find("input");
        blueBulletParticle = GameObject.Find("blue bullet particle").GetComponent<ParticleSystem>();
        orangeBulletParticle = GameObject.Find("orange bullet particle").GetComponent<ParticleSystem>();
        deathParticle = GameObject.Find("death particle").GetComponent<ParticleSystem>();

        // bullets and borders collisions
        Physics2D.IgnoreLayerCollision(8, 9);
        // player bullets and player bullets
        Physics2D.IgnoreLayerCollision(8, 8);
        // players and players
        Physics2D.IgnoreLayerCollision(10, 10);
        // player bullets and players
        Physics2D.IgnoreLayerCollision(8, 10);
        // enemies and buttons
        Physics2D.IgnoreLayerCollision(0, 11);
        //player bullets and enemy bullets
        Physics2D.IgnoreLayerCollision(8, 12);
        // enemy bullets and enemies
        Physics2D.IgnoreLayerCollision(0, 12);
        // enemy bullets and buttons
        Physics2D.IgnoreLayerCollision(12, 11);
        // enemy bullets and bottom bar
        Physics2D.IgnoreLayerCollision(12, 13);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if enemy hits bottom border
        if (gameObject.name.Equals("bottom bar"))
        {
            if (collision.gameObject.CompareTag("blue enemy") || collision.gameObject.CompareTag("orange enemy"))
            {
                Image bar = GameObject.Find("bottom bar").GetComponent<Image>();
                if (bar.color.a > .25f)
                {
                    bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, bar.color.a - .25f);
                }
                else
                {
                    GameObject.Find("bottom bar").GetComponent<Collider2D>().enabled = false;
                    bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 0);
                }
                collision.gameObject.GetComponent<Enemy>().takeDamage(100);
            }
        }

        //if enemy hits bottom border
        if (gameObject.name.Equals("bottom border"))
        {
            if (collision.gameObject.CompareTag("blue enemy") || collision.gameObject.CompareTag("orange enemy"))
            {
                if(!(collision.gameObject.name.Equals("blue bullet") || collision.gameObject.name.Equals("orange bullet")))
                {
                    deathParticle.transform.position = collision.transform.position;
                    deathParticle.Play();
                    endGame();
                    input.GetComponent<Leaderboard>().endGame();
                }
                collision.gameObject.GetComponent<Enemy>().takeDamage(100);
            }
        }
        //if enemy hits left border
        if (gameObject.name.Equals("left border"))
        {
            if (collision.gameObject.CompareTag("blue enemy") || collision.gameObject.CompareTag("orange enemy"))
            {
                float vX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-vX, -5), ForceMode2D.Impulse);
            }
        }
        //if enemy hits right border
        if (gameObject.name.Equals("right border"))
        {
            if (collision.gameObject.CompareTag("blue enemy") || collision.gameObject.CompareTag("orange enemy"))
            {
                float vX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-vX, -5), ForceMode2D.Impulse);
            }
        }
        // blue enemy hits blue player
        if (gameObject.name.Equals("blue player"))
        {
            if (collision.gameObject.CompareTag("blue enemy"))
            {
                deathParticle.transform.position = collision.transform.position;
                deathParticle.Play();
                endGame();
                input.GetComponent<Leaderboard>().endGame();
                gameObject.SetActive(false);
            }
        }
        //orange enemy hits orange player
        if (gameObject.name.Equals("orange player"))
        {
            if (collision.gameObject.CompareTag("orange enemy"))
            {
                deathParticle.transform.position = collision.transform.position;
                deathParticle.Play();
                endGame();
                input.GetComponent<Leaderboard>().endGame();
                gameObject.SetActive(false);
            }
        }
        // blue bullet hits blue enemy
        if(gameObject.CompareTag("blue bullet"))
        {
            if(collision.gameObject.CompareTag("blue enemy"))
            {
                input.GetComponent<Leaderboard>().addScore();
                blueBulletParticle.transform.position = gameObject.transform.position;
                blueBulletParticle.Play();
                collision.gameObject.GetComponent<Enemy>().takeDamage(1);
                gameObject.SetActive(false);
            }
        }
        // orange bullet hits orange enemy
        if (gameObject.CompareTag("orange bullet"))
        {
            if (collision.gameObject.CompareTag("orange enemy"))
            {
                input.GetComponent<Leaderboard>().addScore();
                orangeBulletParticle.transform.position = gameObject.transform.position;
                orangeBulletParticle.Play();
                collision.gameObject.GetComponent<Enemy>().takeDamage(1);
                gameObject.SetActive(false);
            }
        }
    }

    public void endGame()
    {
        bluePlayer.GetComponent<Shoot>().StopAllCoroutines();
        orangePlayer.GetComponent<Shoot>().StopAllCoroutines();
        input.GetComponent<SpawnEnemies>().playing = false;
        input.GetComponent<SpawnEnemies>().despawn();
        input.GetComponent<SpawnEnemies>().StopAllCoroutines();
        mainMenu.SetActive(true);
    }
}