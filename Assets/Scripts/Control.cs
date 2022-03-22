using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public Sprite[] birdSprite;

    private SpriteRenderer sr;

    bool forword = true;
    int spriteCounter = 0;
    float frameCounter = 0;
    public float duration;

    Rigidbody2D rb;

    int point = 0;
    bool gameOver = false;

    public Text scoreText;

    GameObject gameControl;

    AudioSource gameSounds;
    public AudioClip birdFlapSound;
    public AudioClip gameOverSound;
    public AudioClip scoreSound;

    int highestScore = 0;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        gameSounds = GetComponent<AudioSource>();

        highestScore = PlayerPrefs.GetInt("Highest Score");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !gameOver) {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, 200));

            //gameSounds.clip = birdFlapSound;
            //gameSounds.Play();
            gameSounds.PlayOneShot(birdFlapSound);
        }
    }


    void FixedUpdate() {
        AnimateBird();
    }

    void AnimateBird() {
        if(rb.velocity.y > 0) {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }

        frameCounter += Time.fixedDeltaTime;
        if (frameCounter >= duration) {
            frameCounter -= duration;

            if(forword) {
                spriteCounter++;
                if (spriteCounter == 2)
                    forword = false;
            }
            else {
                spriteCounter--;
                if (spriteCounter == 0)
                    forword = true;
            } 
        }

        sr.sprite = birdSprite[spriteCounter];
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Pipe") {
            if (!gameOver) {
                gameSounds.PlayOneShot(gameOverSound);
            }

            gameOver = true;

            if (point > highestScore) {
                PlayerPrefs.SetInt("Highest Score", point);
            }

            point = 0;
            scoreText.text = "Score: " + point;

            gameControl = GameObject.FindGameObjectWithTag("GameControl");
            gameControl.GetComponent<GameControl>().GameOver();

            Invoke("ReturMainMenu", 1.5f);
        }
        else if (collision.gameObject.tag == "Obstacle") {
            if (!gameOver) {
                gameSounds.PlayOneShot(gameOverSound);
            }

            gameOver = true;

            if (point > highestScore) {
                PlayerPrefs.SetInt("Highest Score", point);
            }

            point = 0;
            scoreText.text = "Score: " + point;

            gameControl = GameObject.FindGameObjectWithTag("GameControl");
            gameControl.GetComponent<GameControl>().GameOver();

            Invoke("ReturMainMenu", 1.5f);

        }
        else if (collision.gameObject.tag == "Point") {
            point++;
            scoreText.text = "Score: " + point;

            gameSounds.PlayOneShot(scoreSound);
        }
    }

    void ReturMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
