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

    //Variáveis para pegar componentes do Player
    [SerializeField] private Rigidbody2D meuRB;

    //Variáveis para pegar objetos do jogo
    [SerializeField] private GameObject tiro;

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
            GameObject novoTiro = Instantiate(tiro, transform.position, Quaternion.identity);

            //Pega o rigidbody do tiro e coloca em uma nova variável
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Faz ele ir para cima
            rbTiro.linearVelocity = Vector2.up * vel;
            
        }
    }
}
