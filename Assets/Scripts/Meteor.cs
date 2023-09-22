using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{


    public GameObject Mmeteor;
    private float tiempoDeVida = 3f;
    public float fuerzaExplosion = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,tiempoDeVida);
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Bullet")){ 
            CreateMinimeteors();
        }
        else
            Debug.Log("He collisionado con otra cosa...");
    }

    private void CreateMinimeteors()
    {
        // Crear dos objetos nuevos
        
        GameObject MiniMeteor1 = Instantiate(Mmeteor, transform.position, Quaternion.identity);
        GameObject MiniMeteor2 = Instantiate(Mmeteor, transform.position, Quaternion.identity);

        // Aplicar fuerzas para que salgan en direcciones diferentes
        Rigidbody rb1 = MiniMeteor1.GetComponent<Rigidbody>();
        Rigidbody rb2 = MiniMeteor2.GetComponent<Rigidbody>();

        if (rb1 != null && rb2 != null)
        {
            Vector2 direccion1 = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 direccion2 = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            rb1.AddForce(direccion1 * fuerzaExplosion, ForceMode.Impulse);
            rb2.AddForce(direccion2 * fuerzaExplosion, ForceMode.Impulse);
        }

        // Puedes ajustar 'fuerzaExplosion' para controlar la intensidad de la explosión.
    }
}
