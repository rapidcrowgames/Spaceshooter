using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// VARIÁVEIS DE CONTROLE
    /// </summary>

    //Variáveis de movimento
    private float vel = 7f;

    //Variáveis de vida
    [SerializeField] private int life = 3;

    //Variáveis para pegar componentes do Player
    [SerializeField] private Rigidbody2D meuRB;

    //Variáveis para pegar objetos do jogo
    [SerializeField] private GameObject tiro; //Pega o objeto tiro

    [SerializeField] private GameObject particulaMorte; //Pega a particula da minha morte

    [SerializeField] private Transform shotPosition; //Pega a posição de onde será criado o tiro

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu RigidBody
        meuRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        ///<summary>
        /// USA OS MÉTODOS
        /// </summary>

        PerdeVida(); //Método de morrer
    }

    ///<summary>
    /// MÉTODOS e INPUTS
    /// </summary>
    
    private void OnMove(InputValue value) //Pega o Input criado para movimentar o player em 8 direções
    {
        Vector2 movimento = value.Get<Vector2>();

        //Normalizando minha velocidade nas diagonais também
        movimento.Normalize();

        //Aplicando esse movimento no meu RB
        meuRB.linearVelocity = movimento * vel;

    }

    private void OnShot(InputValue value) //Pega o input para atirar
    {
        //SE o botão de atirar foi pressionado
        if (value.isPressed)
        {
            //Crio a instancia do tiro, no meu X e meu Y definido no paínel do Unity
            GameObject novoTiro = Instantiate(tiro, shotPosition.position, Quaternion.identity);

            //Pega o rigidbody do tiro e coloca em uma nova variável
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Faz ele ir para cima
            rbTiro.linearVelocity = Vector2.up * vel;
            
        }
    }

    //Evento de colisão com o tiro do inimigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SE eu colidir com o tiro do inimigo
        if (other.CompareTag("shotEnemy"))
        {
            life--;
        }
    }

    //Evento de colisão com inimigo
    private void OnCollisionEnter2D(Collision2D other)
    {
        //SE eu colidir com inimigos eu perco vida
        if (other.gameObject.CompareTag("Enemy"))
        {
            life--;
        }
    }

    //MÉTODO de perder vida
    private void PerdeVida()
    {
        //SE minha vida chegar a 0 ou menos, eu morro
        if (life <= 0)
        {
            //Crio a particula de morte
            GameObject deathParty = Instantiate(particulaMorte, transform.position, Quaternion.identity);

            //Destruo a particula em 1 segundo
            Destroy(deathParty, 1f);

            //Me destruo
            Destroy(gameObject);
        }
    }


}
