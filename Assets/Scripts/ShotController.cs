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

    //Evento de colisão com objetos do jogo (Inimigo, colisores, etc)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Colisor")) //Colisor que destroi o tiro fora da tela
        {
            //Me destruo
            Destroy(eu);
        }

        if (other.CompareTag("Enemy")) //Colide com os inimigos
        {
            //Me destruo
            Destroy(eu);
        }
    }
}
