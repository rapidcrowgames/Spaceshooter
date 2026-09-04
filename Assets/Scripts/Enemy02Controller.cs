using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy02Controller : EnemyFather
{
    //Variáveis de controle ////

    //Pegando componentes em variáveis
    [SerializeField] private Rigidbody2D meuRB; //RigidBody
    [SerializeField] private GameObject tiroInimigo; // Pega qual o objeto do meu tiro
    [SerializeField] private Transform tiroPosition; //Pego a posição de onde o tiro deve sair

    //Variáveis do tiro
    [SerializeField] private float shotTime = 1f; // Tempo para atirar

    //Variáveis de movimento
    [SerializeField] private int side; // Descobre em qual lado da tela estou -> 0 ESQUERDA || 1 DIREITA
    private bool sideChoice = false; //Descobre se já foi escolhido um lado

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu RigidBody 2D
        meuRB = GetComponent<Rigidbody2D>();

        //Dando Velocidade ao meu Rigid para ele ir para baixo ao iniciar o game
        meuRB.linearVelocity = new Vector2(0f, -velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        //Usa os métodos do pai
        DeathEnemy(); //Método de morrer

        Enemy2Shot(); //Método de atirar

        Enemy2Move(); //Método de movimentação
    }


    //Método de tiro do Inimigo 2
    private void Enemy2Shot()
    {
        //OBJETIVOS DO MÉTODO:
        //Só vai atirar quando estiver visível na cena
        //Deve ter um intervalo entre os tiros e uma aleatoridade

        //Pega a visibilidade dele em uma variável
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        //SE o tempo do tiro ainda não for 0, eu diminuo ele
        if (shotTime > 0) shotTime -= Time.deltaTime;

        //Pego o player na cena 
        var player = FindAnyObjectByType<PlayerController>();

        //SE for visivel e já tiver zerado o tempo de espera do tiro e, tiver encontrado o player, ele cria o tiro
        if (visivel && shotTime <= 0 && player)
        {
            //Cria a instancia do tiro em uma variável
            GameObject novoTiro = Instantiate(tiroInimigo, tiroPosition.position, Quaternion.identity);

            //Pego a direção
            Vector2 direction = player.transform.position - novoTiro.transform.position;

            //Pego o RB do tiro
            Rigidbody2D tiroRB = novoTiro.GetComponent<Rigidbody2D>();

            //Normalizando a velocidade do tiro
            direction.Normalize();

            //Aplico velocidade ao tiro para ir em direção ao player
            tiroRB.linearVelocity = direction * shotVel;

            //Definindo o ângulo em que o tiro deve sair
            float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //Passando o angulo para o tiro
            novoTiro.transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90);

            //Dou um tempo aleatório para o intervalo do tiro
            shotTime = UnityEngine.Random.Range(1.6f, 2f);
        }
    }


    //MÉTODO DO MOVIMENTO DO INIMIGO 2
    private void Enemy2Move()
    {
        //OBJETIVO DO MÉTODO
        /*
         - Fazer com que ele chegue na metade da tela
         - Quando chegar, se ele estiver do lado esquerdo, ele deve ir para a direita, e vice-versa
        */

        //Descobre se o inimigo chegou mais ou menos no meio da tela
        if (transform.position.y <= 2.15f)
        {
            //SE cheguei, então eu verifico de qual lado estou (ESQUERDA ou DIREITA)
            if (transform.position.x < 0f && !sideChoice)
            {
                //ESTOU NA ESQUERDA
                side = 0;

                //Já escolhi um lado
                sideChoice = true;

                //Paro de me mover para baixo
                meuRB.linearVelocity = new Vector2(0f, 0f);
            }
            else if (transform.position.x >= 0f && !sideChoice)
            {
                //SE é igual ou maior que 0, estou na direita
                side = 1;

                //Já escolhi um lado
                sideChoice = true;

                //Paro de me mover para baixo
                meuRB.linearVelocity = new Vector2(0f, 0f);
            }

            //SE estou na esquerda
            if (side == 0 && sideChoice)
            {
                //Me movo para direita
                meuRB.linearVelocity = new Vector2(velocidade, -velocidade);
            }
            else if (side == 1 && sideChoice) //SE estou na direita
            {
                //Me movo para esquerda
                meuRB.linearVelocity = new Vector2(-velocidade, -velocidade);
            }

        }
    }


    //EVENTO DE COLISÃO
    private void OnCollisionEnter2D(Collision2D other)
    {
        //SE eu colidir com o player, minha vida vai para zero e eu tiro vida do player
        if (other.gameObject.CompareTag("Jogador"))
        {
            life = 0;

            //O player perder vida, está no código do player quando ele colidir com "Enemy"
        }
    }
}
