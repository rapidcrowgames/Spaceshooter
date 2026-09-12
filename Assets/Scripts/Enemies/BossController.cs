using UnityEngine;

public class BossController : EnemyFather
{
    //VARIÁVEIS

    //Variáveis de COMPONENTES do jogo
    [SerializeField] private Rigidbody2D meuRB; //Pega meu RigidBody2D

    //Variáveis para pegar POSIÇÕES E TRANSFORM
    [SerializeField] private Transform ShotLeft; //Pega a posição do tiro da esquerda
    [SerializeField] private Transform ShotRight; //Pega a posição do tiro da direita
    [SerializeField] private Transform ShotCenter; //Pega a posição do tiro do centro


    //Variáveis de controle
    private bool sideChoice = false; //Define se inicialmente no ESTADO o BOSS já escolheu uma lado para andar

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu Rigibody2D
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //USA OS MÉTODOS E MACHINE
        StateMachine();
    }

    //MÁQUINA DE ESTADOS DO BOSS 
    #region

    //ENUM dos estados
    enum action
    {
        state1,
        state2,
        state3
    }

    //Variável para o SWITCH do tipo do ENUM
    action boss = action.state1;

    private void StateMachine()
    {
        //SWITCH
        #region

        switch(boss)
        {
            case action.state1:
                State01();
                break;

            case action.state2:
                State02();
                break;

            case action.state3:
                State03();
                break;
        }

        #endregion
    }

    #endregion


    //ESTADOS
    #region
    private void State01() //Estado 1 do BOSS
    {
        //OBJETIVO DO ESTADO
        /*
         * O BOSS vai atirar apenas das posições das ASAS
         * O BOSS vai se movimentar na esquerda e direita, indo e voltando
         */

        //Escolhe aleatóriamente para qual lado ele sai primeiro
        var side = UnityEngine.Random.Range(-2f, 2f);

        //Faz o boss se mover para direita e esquerda SE ainda não escolhi uma direção
        meuRB.linearVelocity = new Vector2(side, 0f) * velocidade;

        //SE ele bater na parede do lado direito ele muda a direção
    }

    private void State02() //Estado 2 do BOSS
    {
        //OBJETIVO DO ESTADO
        /*
         * O BOSS vai atirar apenas do CENTRO (QUE VAI NA DIREÇÃO DO PLAYER)
         * O BOSS vai continuar se movendo na horizontal
         */
    }

    private void State03() //Estado 3 do BOSS
    {
        //OBJETIVO DO ESTADO
        /*
         * O BOSS vai atirar de todas posições, ASAS e CENTRO
         * O BOSS vai ficar parado no centro
         */
    }

    #endregion


    //MÉTODOS
    #region

    //Método dos TIROS do boss
    private void ShotBoss()
    {

    }


    #endregion
}
