using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject sky1;
    public GameObject sky2;

    Rigidbody2D rb1;
    Rigidbody2D rb2;

    public float bgVelocity = -1.5f;
    float skyLength = 0;

    public GameObject pipe;
    public int pipeCount = 5;
    GameObject[] pipes;
    void Start()
    {
        rb1 = sky1.GetComponent<Rigidbody2D>();
        rb2 = sky2.GetComponent<Rigidbody2D>();

        rb1.velocity = new Vector2(bgVelocity, 0);
        rb2.velocity = new Vector2(bgVelocity, 0);

        skyLength = sky1.GetComponent<BoxCollider2D>().size.x;

        pipes = new GameObject[pipeCount];

        for(int i=0; i < pipeCount; i++) {
            pipes[i] = Instantiate(pipe, new Vector2(8 + 3 * i, 2.5f + Random.value * 1.2f  - 0.6f), Quaternion.identity);
            pipes[i].transform.GetChild(0).localPosition += new Vector3(0.0f ,Random.value * 1.2f - 0.6f);
            pipes[i].AddComponent<Rigidbody2D>();
            pipes[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            pipes[i].GetComponent<Rigidbody2D>().velocity += new Vector2(bgVelocity, 0);
        }
    }

    // Update is called once per frame
    void Update() {
        if (sky1.transform.position.x <= -skyLength) {
            sky1.transform.position += new Vector3(2.0f * skyLength, 0, 0);
        }

        if (sky2.transform.position.x <= -skyLength) {
            sky2.transform.position += new Vector3(2.0f * skyLength, 0, 0);
        }

        for (int i = 0; i < pipeCount; i++) {
            if(pipes[i].transform.position.x < -3) {
                pipes[i].transform.position += new Vector3(15, 0, 0);
            }
        }
    }

    public void GameOver() {
        sky1.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        sky2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        for (int i = 0; i < pipeCount; i++) {
            pipes[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
