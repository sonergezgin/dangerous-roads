using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int goldCount;
    private float timer;
    private bool isGameOver;
    public float speed;
    public Text timerText;
    public Text scoreText;
    public Text gameOverText;
    public int totalGoldCount;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goldCount = 0;
        timer = 0f;
        isGameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isGameOver)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, .0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    void Update()
    {
        if(!isGameOver)
        {
            timer += Time.deltaTime;
            timerText.text = GetTime();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isGameOver)
        {
            RestartGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            goldCount += 1;
            if(goldCount >= totalGoldCount)
            {
                EndGame(true);
            }
        }
        else if(other.gameObject.CompareTag("Wall"))
        {
            EndGame(false);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    string GetTime()
    {
        return timer.ToString("0.00");
    }

    void EndGame(bool win)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        scoreText.text = win ? "You Win. Your Time : " + GetTime() : "You Lost";
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameOver = true;
    }
}
