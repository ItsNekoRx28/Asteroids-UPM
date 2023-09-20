using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 10f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;
    private Rigidbody _rigid;

    public static int score = 0;

    private Vector3 newPos;
    public float xBorderLimit = 10f;
    public float yBorderLimit = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
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

        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Enemy"){
            Player.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else{
            Debug.Log("He collisionado con otra cosa...");
        }     
    }
}
