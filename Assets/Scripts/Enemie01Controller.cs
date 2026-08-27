using UnityEngine;

public class Enemie01Controller : MonoBehaviour
{
    /// <summary>
    /// VARIÁVEIS DE CONTROLE
    /// </summary>

    //Variável para pegar componentes do inimigo 01
    [SerializeField] private Rigidbody2D meuRB;

    //Variável para pegar objetos do jogo
    [SerializeField] private GameObject tiroInimigo; //Pega o tiro do inimigo

    [SerializeField] private Transform shotPosition; //Pega a posição de onde meu tiro deve nascer

    //Variáveis de movimento
    [SerializeField] private float velocidade = 1f;
    [SerializeField] private float shotVel = 4f;

    //Variáveis do timer do tiro
    [SerializeField] private float shotTimer = 0.9f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu RigidBody ao iniciar o game
        meuRB = GetComponent<Rigidbody2D>();

        //Dando velocidade ao meu RigidBody
        meuRB.linearVelocity = new Vector2(0f, -velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        //Usa os métodos
        EnemyShot(); //Método do tiro do inimigo

    }

    //MÉTODOS

    private void EnemyShot() //Método do tiro do inimigo
    {
        //Pegando a visibilidade dos filhos do Inimigo
        bool enemyVisible = GetComponentInChildren<SpriteRenderer>().isVisible;

        Debug.Log(enemyVisible);

        //Diminuo o timer se ele ainda não for zero
        if (shotTimer > 0) shotTimer -= Time.deltaTime;

        //SE o timer chegou em zero, e o inimigo for visível na tela ele cria o tiro
        if (shotTimer <= 0 && enemyVisible)
        {
            //Crio a instancia do tiro em uma variável e faço ele nascer na minha posição no meu X e no meu Y
            GameObject novoTiro = Instantiate(tiroInimigo, shotPosition.position, Quaternion.identity);

            //Pego o RigidBody do tiro
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Dou velocidade a ele
            rbTiro.linearVelocity = new Vector2(0f, -shotVel);

            //Reinicio o timer de forma aleatória
            shotTimer = Random.Range(0.8f, 1f);
        }
    }

    //Colisor para se destruir com o DESTRUIDOR
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor"))
        {
            Destroy(gameObject); //Destruo eu mesmo
        }
    }
}
