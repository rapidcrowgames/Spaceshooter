using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// VARIÁVEIS DE CONTROLE
    /// </summary>

    //Variáveis de movimento
    private float vel = 4f;

    //Variáveis para pegar componentes do Player
    [SerializeField] private Rigidbody2D meuRB;

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

        //Aplicando esse movimento no meu RB
        meuRB.linearVelocity = movimento * vel;
    }
}
