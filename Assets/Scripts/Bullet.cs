using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("SetFalse",2f); // disable after 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){ 
            IncreaseScore();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
        else
            Debug.Log("He collisionado con otra cosa...");
    }

    private void IncreaseScore(){
        Player.score++;
        Debug.Log(Player.score);
        UpdateScoreText();
    }

    private void UpdateScoreText(){
        GameObject UI = GameObject.FindWithTag("UI");
        UI.GetComponent<Text>().text = "SCORE: " + Player.score;
    }
    
}
