using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    //Variáveis do PAI
    [SerializeField] protected int life;
    [SerializeField] protected GameObject particulaMorte; //Pega a particula de morte do inimigo
    [SerializeField] protected float velocidade; // Velocidade de movimento do inimigo
    [SerializeField] protected float shotVel; // Velocidade do projétil do tiro
    [SerializeField] protected int pontos; //Variável que cuida quantos pontos cada inimigo dará ao player

    [SerializeField] protected GameObject powerUp; //Pega o objeto power up

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// MÉTODOS
    /// </summary>

    //Método de morte
    public void DeathEnemy()
    {
        //Variável para descobrir se estou visivel
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        //SE minha vida chegar a 0 ou menor, eu me destruo / morro
        if (life <= 0)
        {
            //Crio a particula da morte
            GameObject deathParty = Instantiate(particulaMorte, transform.position, Quaternion.identity);

            //Depois de 1f segundos eu me destruo
            Destroy(deathParty, 1f);

            //Quando eu morrer, o jogador ganha pontos
            var GameController = FindFirstObjectByType<GameController>();

            GameController.EnemieQTD(); //Diminui a quantidade de inimigos criados
            GameController.GanhaPontos(pontos); //Acessa e da pontos ao jogador

            //Gera uma chance de criar o power up
            int chance = UnityEngine.Random.Range(0, 10);

            //Pega o player dentro deste escopo
            var player = FindFirstObjectByType<PlayerController>();

            //SE a chance for maior que 7 ele cria o poder e SE o player ainda não chegou no poder máximo
            if (chance > 7 && player.GetLevelShot() < 2)
            {
                //Cria o power up na minha instância do inimigo
                Instantiate(powerUp, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    //Método de perder vida
    public void PerdeVida(int value)
    {
        //SE o tiro do player me acertar eu perco um determinado valor de vida
        //SE eu estiver na tela
        if (transform.position.y < 4.7f)
        {
            life -= value;
        }
    }

    //SE eu colidir com o destruidor, eu morro
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor"))
        {
            //Eu morro
            life = 0;
        }
    }
}
