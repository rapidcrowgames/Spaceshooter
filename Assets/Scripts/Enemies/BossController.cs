using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyFather
{
    //VARIÁVEIS

    [Header("Componentes")]
    //Variáveis de COMPONENTES do jogo
    private Rigidbody2D meuRB; //Pega meu RigidBody2D
    [SerializeField] private GameObject bossShot; //Pega qual é o tiro do BOSS
    [SerializeField] private GameObject bossDeathAnim; //Pega a animação de morte do BOSS

    [Header("Posições e Transform")]
    //Variáveis para pegar POSIÇÕES E TRANSFORM
    [SerializeField] private Transform shotLeft; //Pega a posição do tiro da esquerda
    [SerializeField] private Transform shotRight; //Pega a posição do tiro da direita
    [SerializeField] private Transform shotCenter; //Pega a posição do tiro do centro

    [Header("Variáveis de controle do BOSS")]
    //Variáveis de controle do BOSS
    [SerializeField] private int shotLevel = 1; //Define qual o level do tiro do BOSS (Muda de acordo com o ESTADO do boss)
    private float shotTimer = 0f; //Cuida da velocidade em que o tiro será disparado
    private PlayerController player; //Variável que pega o player controller (Script do player)
    private bool sideChoice = false; //Define se inicialmente no ESTADO o BOSS já escolheu uma lado para andar
    [SerializeField] private int lifeMAX; //Define a vida máxima do boss

    [Header("Variáveis do CANVAS")]
    //VARIÁVEIS DE CANVAS
    [SerializeField] private Image lifeBar; //Pega a imagem da barra de vida do CANVAS do Boss

    //Pega as minhas próprias coisas
    private void Awake()
    {
        //Diz que não sou um boss
        iamBoss = true;

        //Pegando meu Rigibody2D
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu script do player
        player = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //USA OS MÉTODOS E MACHINE
        StateMachine();

        //Método dos tiros
        ShotBoss();

        //Método da atualização da barra de vida do boss
        LifeUpdate();
    }

    //MÁQUINA DE ESTADOS DO BOSS 
    #region

    //ENUM dos estados
    enum action
    {
        state1,
        state2,
        state3,
        stateDeath
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
            case action.stateDeath:
                StateDeath();
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

        //Faz o boss se mover na horizontal
        MoveHorizontalBoss();

        //SE a minha vida chegar em 200, eu mudo de estado e o meu nível do tiro
        if (life <= 200)
        {
            //Mudo o level do meu tiro
            shotLevel = 2;

            //Mudo meu estado
            boss = action.state2;

            //Zero meu SideChoice
            sideChoice = false;
        }
    }

    private void State02() //Estado 2 do BOSS
    {
        //OBJETIVO DO ESTADO
        /*
         * O BOSS vai atirar apenas do CENTRO (QUE VAI NA DIREÇÃO DO PLAYER)
         * O BOSS vai ficar parado no centro
         */

        // Distância mínima para considerar que chegou ao centro
        float centerTolerance = 0.1f;

        // SE ainda estou longe do centro
        if (Mathf.Abs(transform.position.x) > centerTolerance)
        {
            // Descobre para qual lado preciso andar para chegar ao X = 0
            float direction = Mathf.Sign(-transform.position.x);

            meuRB.linearVelocity = new Vector2(direction * velocidade, 0f);
        }
        else
        {
            // Cheguei perto o suficiente do centro
            meuRB.linearVelocity = Vector2.zero;

            // Garante que fico exatamente no centro
            transform.position = new Vector2(0f, transform.position.y);
        }

        //SE minha vida chegar a 100 eu vou para o próximo estado, e mudo meu level do tiro
        if (life <= 100)
        {
            //Mudo meu level do tiro
            shotLevel = 3;

            //Mudo meu estado
            boss = action.state3;
        }
    }

    private void State03() //Estado 3 do BOSS
    {
        //OBJETIVO DO ESTADO
        /*
         * O BOSS vai atirar de todas posições, ASAS e CENTRO
         * O BOSS vai ficar se movendo para os lados
         */

        //Faz o boss se mover na horizontal
        MoveHorizontalBoss();

        //SE minha vida chegar a zero eu morro / vou para o estado de morte
        if (life <= 0)
        {
            //Vou para o estado de morte
            boss = action.stateDeath;
        }
    }

    private void StateDeath() //Estado de morte do boss
    {
        //OBJETIVO DO ESTADO
        /*
         * Ao chegar ao fim da vida, ele reproduz a animação de morte do BOSS 
         * Depois se destroi
         */

        Debug.Log("Estado MORTE");

        //Uso o método de morte do pai
        DeathEnemy(4.2f);
    }

    #endregion


    //MÉTODOS
    #region

    //Método dos TIROS do boss
    private void ShotBoss()
    {
        //Diminui o timer de tiro SE começou a fase do boss
        if (shotTimer > 0) shotTimer -= Time.deltaTime;
        
        //SE o boss está no level 1 do tiro, ele cria apenas tiros nas ASAS
        if (shotLevel == 1 && shotTimer <= 0 && player)
        {
            //Coloca a instancia dos tiros em variáveis (Criando os tiros nas posições das asas)
            GameObject tiroLeft = Instantiate(bossShot, shotLeft.position, Quaternion.identity);
            GameObject tiroRight = Instantiate(bossShot, shotRight.position, Quaternion.identity);

            //Pegando o RB de cada tiro
            Rigidbody2D tLeftRb = tiroLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D tRightRb = tiroRight.GetComponent<Rigidbody2D>();

            //Aplicando a velocidade e movimento ao tiro
            tLeftRb.linearVelocity = Vector2.down * shotVel;
            tRightRb.linearVelocity = Vector2.down * shotVel;

            //Reseta o timer
            shotTimer = 0.7f;
        }

        //SE o boss está no level 2 do tiro, ele cria apenas tiros no centro 
        if (shotLevel == 2 && shotTimer <= 0 && player)
        {
            //Cria apenas o tiro no centro
            GameObject tiroCenter = Instantiate(bossShot, shotCenter.position, Quaternion.identity);

            //Pegando qual a direção do player
            Vector2 direction = player.transform.position - tiroCenter.transform.position;

            //Pengado meu RigidBody
            Rigidbody2D tCenterRb = tiroCenter.GetComponent<Rigidbody2D>();

            //Normalizando minha velocidade
            direction.Normalize();

            //Aplicando a velocidade com base na minha direção
            tCenterRb.linearVelocity = direction * shotVel;

            //Reseta o timer
            shotTimer = 0.5f;
        }

        //SE o boss está no level 3 de tiro, ele cria tiro de todas direções
        if (shotLevel == 3 && shotTimer <= 0 && player)
        {
            //Coloca a instancia dos tiros em variáveis (Criando os tiros nas posições das asas e no centro)
            GameObject tiroLeft = Instantiate(bossShot, shotLeft.position, Quaternion.identity);
            GameObject tiroRight = Instantiate(bossShot, shotRight.position, Quaternion.identity);
            GameObject tiroCenter = Instantiate(bossShot, shotCenter.position, Quaternion.identity);

            //Definindo a direção do meu tiro do meio
            Vector2 direction = player.transform.position - tiroCenter.transform.position;

            //Pegando o RB de cada tiro
            Rigidbody2D tLeftRb = tiroLeft.GetComponent<Rigidbody2D>();
            Rigidbody2D tRightRb = tiroRight.GetComponent<Rigidbody2D>();
            Rigidbody2D tCenterRb = tiroCenter.GetComponent<Rigidbody2D>();

            //Normalizando a velocidade da minha direção
            direction.Normalize();

            //Aplicando a velocidade aos meus tiros
            tLeftRb.linearVelocity = Vector2.down * shotVel;
            tRightRb.linearVelocity = Vector2.down * shotVel;
            tCenterRb.linearVelocity = direction * shotVel;

            //Reseta o timer + tempo 
            shotTimer = 0.8f;
        }
    }

    //Método do BOSS se mover na horizontal (Direita e esquerda) 
    private void MoveHorizontalBoss()
    {
        //Escolhe aleatóriamente para qual lado ele sai primeiro
        var side = Random.Range(0, 2) == 0 ? -2f : 2f;

        //Faz o boss se mover para direita e esquerda SE ainda não escolhi uma direção
        if (!sideChoice)
        {
            meuRB.linearVelocity = new Vector2(side, 0f);

            //Já escolhi um lado
            sideChoice = true;
        }

        //SE ele bater na parede do lado ESQUERDO ele muda a direção
        if (transform.position.x <= -5.8f)
        {
            //Vou para a direita
            meuRB.linearVelocity = new Vector2(velocidade, 0f);
        }
        else if (transform.position.x >= 5.8f) //SE ele bater do lado DIREITO 
        {
            //Vou para a esquerda
            meuRB.linearVelocity = new Vector2(-velocidade, 0f);
        }
    }

    //MÉTODO de diminuir a vida do BOSS
    private void LifeUpdate()
    {
        //Pega a quantidade da barra de vida e deixa ela igual a QTD de vida
        lifeBar.fillAmount = ((float)life / (float)lifeMAX);

        //Convertendo o valor do FillAmout em 255 para mudar a cor da barra conforme a vida diminui
        lifeBar.color = new Color32(201, (byte) (lifeBar.fillAmount * 255), 47, 255);
    }


    #endregion
}
