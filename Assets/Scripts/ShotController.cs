using UnityEngine;

public class ShotController : MonoBehaviour
{
    //Variável para pegar eu como game object
    public GameObject eu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Evento de colisão com Trigger de destruir os tiros
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Colisor"))
        {
            Destroy(eu);
        }
    }
}
