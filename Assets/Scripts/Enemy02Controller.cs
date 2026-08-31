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
    [SerializeField] private float shotTime = 0.5f; // Tempo para atirar

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu RigidBody 2D
        meuRB = GetComponent<Rigidbody2D>();

        //Dando Velocidade ao meu Rigid
        meuRB.linearVelocity = new Vector2(0f, -velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        //Usa os métodos do pai
        DeathEnemy(); //Método de morrer

        Enemy2Shot(); //Método de atirar
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

        //SE for visivel e já tiver zerado o tempo de espera do tiro, ele cria o tiro
        if (visivel && shotTime <= 0)
        {
            //Cria a instancia do tiro em uma variável
            GameObject novoTiro = Instantiate(tiroInimigo, tiroPosition.position, Quaternion.identity);

            //Pego o player na cena 
            var player = FindAnyObjectByType<PlayerController>();

            //Pego a direção
            Vector2 direction = player.transform.position - novoTiro.transform.position;

            //Pego o RB do tiro
            Rigidbody2D tiroRB = novoTiro.GetComponent<Rigidbody2D>();

            //Normalizando a velocidade do tiro
            direction.Normalize();

            //Aplico velocidade ao tiro para ir em direção ao player
            tiroRB.linearVelocity = direction * shotVel;

            //Dou um tempo aleatório para o intervalo do tiro
            shotTime = UnityEngine.Random.Range(1.2f, 2f);
        }
    }
}
