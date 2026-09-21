using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;

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
    [SerializeField] private GameObject[] tiro = { }; //Pega o array de tipos de tiros do player
    [SerializeField] private GameObject particulaMorte; //Pega a particula da minha morte
    [SerializeField] private GameObject shield; //Pega o escudo do player

    //Variáveis das posições dos tiros
    [SerializeField] private Transform shotPosition; //Pega a posição de onde será criado o tiro
    [SerializeField] private Transform shotPositionLeft; //Pega o lado esquerdo da asa para criar o tiro
    [SerializeField] private Transform shotPositionRight; //Pega o lado direito da asa para criar o tiro

    //Pega a posição do escudo e atualiza ela o tempo todo e OUTRAS variáveis do escudo também
    private bool shieldOn = false; //Booleana para dizer se o escudo está ativado ou não
    [SerializeField] private Transform shieldTrans; //Variável que guarda a posição do escudo
    private GameObject aShield; //Variável da instância do escudo
    [SerializeField] private int shieldQTD = 3; //Quantidade de escudos que o player tem inicialmente
    [SerializeField] private float shieldTime = 3f; //Dura 3 segundos


    //Variáveis dos tipos de tiro e level do tiro
    [SerializeField] private int levelShot = 0; //Level do tiro do player

    //Variáveis de CANVAS
    [SerializeField] private TextMeshProUGUI txtLife; //Pega o texto da vida para o HUD
    [SerializeField] private TextMeshProUGUI txtShield; //Pega o texto do escudo para o HUD


    //Variáveis de espaço na cena
    [SerializeField] private float xMin; //Limite MIN do X
    [SerializeField] private float xMax; //Limite MAX do X
    [SerializeField] private float yMin; //Limite MIN do Y
    [SerializeField] private float yMax; //Limite MAX do Y


    //VARIÁVEIS DE SOM
    [SerializeField] private AudioSource audioSource; //Pega a "caixa de som" 
    [SerializeField] private AudioClip deathSound; //Pega o som da morte
    [SerializeField] private AudioClip shotSound; //Pega o som do tiro
    [SerializeField] private AudioClip shieldSound; //Pega o som do escudo



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
        ShieldFollow(); //Método de fazer o escudo me seguir
        PlayerHUD(); //Método que atualiza do HUD do player
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

        //Limitando os meus limites na tela
        float meuX = Mathf.Clamp(transform.position.x ,xMin, xMax);
        float meuY = Mathf.Clamp(transform.position.y ,yMin, yMax);

        //Passando para meu X e Y os meus limites
        transform.position = new Vector3(meuX, meuY, transform.position.z);

    }

    private void OnShot(InputValue value) //Pega o input para atirar
    {
        //Criando as variáveis dos tiros
        GameObject novoTiro; //Central
        GameObject novoTiroDir; //Direita
        GameObject novoTiroEsq; //Esquerda

        //SE o botão de atirar foi pressionado e o nível do tiro for 0 (Cria o primeiro tiro no centro)
        if (value.isPressed && levelShot == 0)
        {
            //CRIA O TIRO TIPO 1 CENTRALIZADO NO PLAYER

            //Crio a instancia do tiro, no meu X e meu Y definido no paínel do Unity
            novoTiro = Instantiate(tiro[0], shotPosition.position, Quaternion.identity);

            //Reproduz o som de tiro
            audioSource.PlayOneShot(shotSound);

            //Pega o rigidbody do tiro e coloca em uma nova variável
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Faz ele ir para cima
            rbTiro.linearVelocity = Vector2.up * vel;

        }

        //SE o LevelShot for 1 ele cria o Tiro tipo 2 nas laterais do player.
        if (value.isPressed && levelShot == 1)
        {
            //Cria a instancia do tiro, nas laterais e não no centro
            novoTiroEsq = Instantiate(tiro[1], shotPositionLeft.position, Quaternion.identity);
            novoTiroDir = Instantiate(tiro[1], shotPositionRight.position, Quaternion.identity);

            //Reproduz o som de tiro
            audioSource.PlayOneShot(shotSound);

            //Pega o RigidBody do tiro
            Rigidbody2D rbTiroEsq = novoTiroEsq.GetComponent<Rigidbody2D>();
            Rigidbody2D rbTiroDir = novoTiroDir.GetComponent<Rigidbody2D>();

            //Aplica a velocidade
            rbTiroEsq.linearVelocity = Vector2.up * vel;
            rbTiroDir.linearVelocity = Vector2.up * vel;
        }

        //SE o LevelShot for 1 ele cria o Tiro tipo 3 nas laterais do player e no centro.
        if (value.isPressed && levelShot >= 2)
        {
            //Cria a instancia do tiro, nas laterais e não no centro
            novoTiroEsq = Instantiate(tiro[2], shotPositionLeft.position, Quaternion.identity);
            novoTiroDir = Instantiate(tiro[2], shotPositionRight.position, Quaternion.identity);
            novoTiro    = Instantiate(tiro[2], shotPosition.position, Quaternion.identity);

            //Reproduz o som de tiro
            audioSource.PlayOneShot(shotSound);

            //Pega o RigidBody do tiro
            Rigidbody2D rbTiroEsq = novoTiroEsq.GetComponent<Rigidbody2D>();
            Rigidbody2D rbTiroDir = novoTiroDir.GetComponent<Rigidbody2D>();
            Rigidbody2D rbTiro = novoTiro.GetComponent<Rigidbody2D>();

            //Aplica a velocidade
            rbTiroEsq.linearVelocity = Vector2.up * vel;
            rbTiroDir.linearVelocity = Vector2.up * vel;
            rbTiro.linearVelocity = Vector2.up * vel;
        }
    }

    private void OnShield(InputValue value) //Pega o input do escudo
    {
        //SE apertar a tecla do escudo e TENHO escudos suficientes
        if (value.isPressed && shieldQTD > 0)
        {
            //Ele cria o escudo na minha posição
            aShield = Instantiate(shield, transform.position, Quaternion.identity);

            //Reproduz o som do escudo
            audioSource.PlayOneShot(shieldSound);

            //Indica que o escudo está ativo
            shieldOn = true;

            //Gasta 1 escudo
            shieldQTD--;
        }
    }

    //Evento de colisão com o tiro do inimigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SE eu colidir com o tiro do inimigo
        if (other.CompareTag("shotEnemy"))
        {
            //Reproduz o som de dano / Explosão
            audioSource.PlayOneShot(deathSound);

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

    //MÉTODO de fazer o escudo seguir o player
    private void ShieldFollow()
    {
        //Verifica se o escudo está realmente ativo
        if (shieldOn)
        {
            //SE sim o transform do escudo o transform do escudo é o mesmo que o meu
            aShield.transform.position = transform.position;
        }

        //Faz gastar o tempo de uso do escudo
        if (shieldTime > 0 && shieldOn) shieldTime -= Time.deltaTime;

        //SE o timer chegar a 0, ele não está mais usando o escudo
        if (shieldTime <= 0)
        {
            //Destroy a instancia do escudo
            Destroy(aShield);

            shieldOn = false;

            //Reseta o timer
            shieldTime = 3f;
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

            //Reproduz o som de explosão / Morte
            audioSource.PlayOneShot(deathSound);

            //Destruo a particula em 1 segundo
            Destroy(deathParty, 1f);

            //Me destruo
            Destroy(gameObject);

            //Pegando o GameManager da cena
            var gameManager = FindFirstObjectByType<GameManager>();

            //Acessando o método de voltar a tela inicial
            gameManager.InitialScene();
        }
    }

    //MÉTODO de mudar de tiro
    public void ChangeShot()
    {
        //SÓ aumenta o nível se ainda não cheguei no level máximo
        if (levelShot < 2) levelShot++;
    }

    //MÉTODO de pegar a variável LevelShot e deixa-la publica
    public int GetLevelShot()
    {
        return levelShot;
    }

    //MÉTODO do HUD
    private void PlayerHUD()
    {
        //Exibe a QTD de vida na variável
        txtLife.text = Mathf.Round(life).ToString();

        //Exibe a QTD de escudos
        txtShield.text = Mathf.Round(shieldQTD).ToString();
    }
}