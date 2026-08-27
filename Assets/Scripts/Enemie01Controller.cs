using UnityEngine;

public class Enemie01Controller : MonoBehaviour
{
    /// <summary>
    /// VARIÁVEIS DE CONTROLE
    /// </summary>

    //Variável para pegar componentes do inimigo 01
    [SerializeField] private Rigidbody2D meuRB;

    //Variáveis de movimento
    [SerializeField] private float velocidade = 3f;

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
        
    }
}
