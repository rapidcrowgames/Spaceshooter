using UnityEngine;

public class Enemie01Controller : EnemyFather
{
    /// <summary>
    /// VARIÁVEIS DE CONTROLE
    /// </summary>
    
    //Variável para pegar componentes do inimigo 01
    [SerializeField] private Rigidbody2D meuRB;

    //Variável para pegar objetos ou componentes de objetos do jogo
    [SerializeField] private GameObject tiroInimigo; //Pega o tiro do inimigo
    [SerializeField] private Transform shotPosition; //Pega a posição de onde meu tiro deve nascer

    //Variáveis do timer do tiro
    private float shotTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Diz que não sou um boss
        iamBoss = false;

        //Pegando meu RigidBody ao iniciar o game
        meuRB = GetComponent<Rigidbody2D>();

        //Dando velocidade ao meu RigidBody
        meuRB.linearVelocity = new Vector2(0f, -velocidade);

        //Inicio um valor aleatório para o tiro do Inimigo 1
        shotTimer = Random.Range(4f, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Usa os métodos
        EnemyShot(); //Método do tiro do inimigo

        DeathEnemy(1f); //Método de morrer
    }

    //MÉTODOS

    private void EnemyShot() //Método do tiro do inimigo
    {
        //Pegando a visibilidade dos filhos do Inimigo
        //bool enemyVisible = GetComponentInChildren<SpriteRenderer>().isVisible;

        //Diminuo o timer se ele ainda não for zero
        if (shotTimer > 0) shotTimer -= Time.deltaTime;

        //SE o timer chegou em zero, e o inimigo for visível na tela ele cria o tiro
        if (shotTimer <= 0 && transform.position.y <= 4.3f)
        {
            //Crio a instancia do tiro em uma variável e faço ele nascer na minha posição no meu X e no meu Y
            GameObject novoTiro = Instantiate(tiroInimigo, shotPosition.position, Quaternion.identity);

            //Reproduz o som do tiro
            audioSource.PlayOneShot(shotSound);

            //Pego o RigidBody do tiro
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Dou velocidade a ele
            rbTiro.linearVelocity = new Vector2(0f, -shotVel);

            //Reinicio o timer de forma aleatória
            shotTimer = Random.Range(2.5f, 3.5f);
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

        //SE eu colidir com o escudo do player
        if (other.gameObject.CompareTag("Shield"))
        {
            //Eu morro
            life = 0;
        }
    }

}
