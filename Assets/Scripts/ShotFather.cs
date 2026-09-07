using UnityEngine;

public class ShotFather : MonoBehaviour
{
    [SerializeField] protected GameObject particulaTiro; //Pega a particula do tiro

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
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy")) //Colide com os inimigos
        {
            //Acessa o inimigo e tira a QTD de vida escolhida
            other.GetComponent<EnemyFather>().PerdeVida(1);

            //Crio na minha posição a animação da particula do tiro e 1.5 segundos depois ela se destrói
            GameObject shotParty = Instantiate(particulaTiro, transform.position, Quaternion.identity);

            Destroy(shotParty, 1.5f); //Destruo a animação do impacto

            //Me destruo, o tiro
            Destroy(gameObject);
        }
    }
}
