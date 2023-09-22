using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool godMode = true;

    public float thrustForce = 10f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;
    private Rigidbody _rigid;

    public static int score = 0;

    private Vector3 newPos;
    public float xBorderLimit = 10f;
    public float yBorderLimit = 10f;
    public static int cantidadInicial = 20; 

    public static List<GameObject> poolBalas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        for (int i = 0; i < cantidadInicial; i++)
        {
            GameObject bala = Instantiate(bulletPrefab);
            bala.SetActive(false);
            poolBalas.Add(bala);
        }
    }

    // Update is called once per frame
    void Update()
    {
         
        var newPos = transform.position;
        if (newPos.x > xBorderLimit){
            newPos.x = -xBorderLimit+1;
        }else if (newPos.x < -xBorderLimit){
            newPos.x = xBorderLimit-1;
        }else if (newPos.y > yBorderLimit){
            newPos.y = -yBorderLimit+1;
        }else if (newPos.y < -yBorderLimit){
            newPos.y = yBorderLimit-1;
        }
        transform.position = newPos;

        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustForce * thrustDirection * thrust);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        GameObject bala = poolBalas.Find(b => !b.activeInHierarchy);
        if(Input.GetKeyDown(KeyCode.Space) && bala!=null){
            
            bala.transform.position = gun.transform.position;
            bala.SetActive(true);
            StartCoroutine(SetFalse(bala));

            Bullet bulletScript = bala.GetComponent<Bullet>();
            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(godMode){
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(),gameObject.GetComponent<Collider>() );
        }else if(collision.gameObject.tag == "Enemy"){
            Player.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else{
            Debug.Log("He collisionado con otra cosa...");
        }     
    }

    IEnumerator SetFalse(GameObject bala){
         yield return new WaitForSeconds(2f);
         bala.SetActive(false);
    }
}
