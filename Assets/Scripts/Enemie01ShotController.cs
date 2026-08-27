using UnityEngine;

public class Enemie01ShotController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Colisor para se destruir com o DESTRUIDOR
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor")) //Se eu colidir com o paredão
        {
            Destroy(gameObject); //Destruo eu mesmo
        }

        //SE eu colidir com o player
        if (other.CompareTag("Jogador"))
        {
            //Me destruo
            Destroy(gameObject); 
        }
    }
}
