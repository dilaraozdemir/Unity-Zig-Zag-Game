using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    delegate void TurnDelegate();
    TurnDelegate turn;

    public Text hScoreText, scoreText;
    int score, hScore;

    public ParticleSystem effectPrefab;
    Animator animator { get { return GetComponent<Animator>(); } }
    GameManager gameManager { get { return FindObjectOfType<GameManager>(); } }
    public float moveSpeed = 2;
    bool lookingRight = true;
    public Transform rayOrigin;

    private void Awake()
    {
        hScore =  PlayerPrefs.GetInt("myhscoreval");
        hScoreText.text = hScore.ToString();
    #if UNITY_EDITOR
        turn = TurnUsingKeyboard;
    #endif
    #if UNITY_ANDROID
            turn = TurnUsingTouch;
    #endif
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted)
        {
            animator.SetTrigger("gameStarted");
            // transform.position += transform.forward * Time.deltaTime * moveSpeed;
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            turn();
            CheckFalling();
        }
    }
    Vector3 crystalPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("crystal"))
        {
            crystalPos = other.transform.position + Vector3.up*1.5f;
            MakeScore();
            other.gameObject.SetActive(false);
            MakeEffect();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject, 3f);
    }
    private void MakeEffect()
    {
        var effect = Instantiate(effectPrefab, crystalPos, Quaternion.identity);
        Destroy(effect, 1f);
    }
    
    private void MakeScore()
    {
        score++;
        scoreText.text = score.ToString();
        
        if(score > hScore)
        {
            hScore = score;
            hScoreText.text = hScore.ToString();
            PlayerPrefs.SetInt("myhscoreval", hScore);
        }
    }

    float elapsedTime;
    float freq = 4;
    private void CheckFalling()
    {
        if ((elapsedTime += Time.deltaTime) >= 1/freq)
        {
            if (!Physics.Raycast(rayOrigin.position, Vector3.down))
            {
                animator.SetTrigger("falling");
                gameManager.RestartGame();
            }
            elapsedTime = 0;
        }
        
    }
    private void Turn()
    {
        if (lookingRight)
        {
            transform.Rotate(0, -90, 00);
        }
        else
        {
            transform.Rotate(0, 90, 00);
        }
        lookingRight = !lookingRight;
    }
    private void TurnUsingKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn();
        }
    }
    private void TurnUsingTouch()
    {
        float firstTouchX = 0;
        float lastTouchX = 0;
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    firstTouchX = Input.GetTouch(0).position.x;
                    break;
                case TouchPhase.Moved:
                    lastTouchX = Input.GetTouch(0).position.x;
                    break;
                case TouchPhase.Ended:
                    lastTouchX = Input.GetTouch(0).position.x;
                    var diff = Mathf.Abs(lastTouchX - firstTouchX);
                    if (diff > 50) Turn();
                    break;

            }
        }
    }

    
}
