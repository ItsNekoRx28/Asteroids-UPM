using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMeteor : MonoBehaviour
{
    // Start is called before the first frame update
    private float tiempoDeVida = 3f;

    void Start()
    {
        Destroy(gameObject,tiempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy"){
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(),gameObject.GetComponent<Collider>() );
        }
    }
}
