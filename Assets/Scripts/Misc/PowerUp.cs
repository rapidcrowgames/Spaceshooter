using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //VARIÁVEIS

    //Variáveis de pegar componentes e objetos do jogo
    [SerializeField] private Rigidbody2D meuRB; //Pego meu RigidBody2D


    //Variáveis de controle
    private int vel = 1; //Velocidade de movimento do powerup

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pego meu RB
        meuRB = GetComponent<Rigidbody2D>();

        //SE eu fui criado, eu começo a me movimentar lentamente para baixo
        meuRB.linearVelocity = Vector2.down * vel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
