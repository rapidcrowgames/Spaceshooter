using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //VARIÁVEIS 

    //Variáveis para pegar objetos do jogo
    [SerializeField] private GameObject[] inimigos = { }; //Array que pega os inimigos do jogo
    [SerializeField] private GameObject boss; //Variável que pega o boss do jogo
    [SerializeField] private GameObject bossAnim; //Variável que pega animação de entrada do boss

    //Variáveis que pegam Transform e posições da cena
    [SerializeField] private Transform createBossAnimPosition; //Posição que vai criar a animação do boss
    [SerializeField] private Transform createBossPosition; //Posição que vai criar o boss

    //Variáveis de cena
    [SerializeField] private int pontos = 0; //Pontos do jogo
    [SerializeField] private int level = 1; //Level do jogo
    private float createTimeEnemies = 0f; //Tempo inicial para criar os inimigos
    [SerializeField] private float waitTime = 4f; //Tempo de espera para criar os inimigos
    private Vector2 spawnPosition; //Variável que contem os eixos X e Y
    [SerializeField] private float xMax = -7f; //Variável que define o valor minimo do nascimento do inimigo no eixo X
    [SerializeField] private float xMin = 7f; //Variável que define o valor máximo do nascimento do inimigo no eixo X
    [SerializeField] private float yMin = 8f; //Variável que define o valor máximo do nascimento do inimigo no eixo Y
    [SerializeField] private float yMax = 10f; //Variável que define o valor máximo do nascimento do inimigo no eixo Y
    private int arrayInd = 0; //Variável que cuida do indice do array dos inimigos : MÉTODO WaveCreate()
    [SerializeField] private int qtdInimigos = 0; //Variável que controla quantos inimigos já foram criados na WAVE

    //Variáveis de TEXTO
    [SerializeField] private TextMeshProUGUI textoPontos; //Pega o texto que exibe a pontuação
    [SerializeField] private TextMeshProUGUI textoLevel; //Pega o texto que exibe o level


    //Variáveis do MÉTODO de criar o BOSS
    private float createBossAnimTime = 3f; // Controla quanto tempo vai levar para criar o boss na cena
    private float createBossTime = 6.9f; // Contador que diminuir para criar o BOSS definitivo
    private bool bossCreated = false; // Flag que garante que o boss seja criado só uma vez
    private bool bossAnimCreated = false; //Flag que garante que a animação seja criada uma vez só

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //USA OS MÉTODOS
        #region

        //Método de criar as WAVES
        WaveCreate();

        //Método de criar o BOSS
        CallBoss();

        #endregion
    }

    //MÉTODOS

    //MÉTODO de checar se alguém já ocupou o espaço onde o inimigo vai nascer para evitar de nascerem um
    //em cima do outro
    private bool PositionCheck(Vector2 position, Vector2 size)
    {
        //Cria a checagem em uma variável
        Collider2D inPosition = Physics2D.OverlapBox(position, size, 0f);

        //SE tem alguém na minha posição então eu ativo uma variável de controle
        if (inPosition)
        {
            //Tem alguém na minha posição
            return true;
            
        }
        else
        {
            //Não tem ninguém na minha posição
            return false;
        }
    }

    //Método do timer para criar inimigos
    private void WaveCreate()
    {
        //OBJETIVO DO MÉTODO
        /*
         * FAZER COM QUE, QUANDO O TIMER ESTEJA EM 0, ELE CRIE NOVOS INIMIGOS
         * DE ACORDO COM O NÍVEL DA PARTIDA
        */

        //SE o timer ainda não for 0, ele diminui o tempo do timer
        if (createTimeEnemies > 0) createTimeEnemies -= Time.deltaTime;

        //SE o timer chegou em 0, ele cria novos inimigos
        if (createTimeEnemies <= 0 && qtdInimigos <= 0 && level < 10)
        {
            //Variáveis do laço
            int inimigosCriados = level * 4; //Cria 4 inimigos de acordo com level: [SE for level 2 cria 8, se for level 3 cria 12 e etc.]

            //Variável que verifica quantas tentativas teve o while para evitar travar o jogo
            int tentativas = 0;

            //Laço de repetição para criar uma QTD de inimigos por vez
            while (qtdInimigos < inimigosCriados)
            {
                //Aumenta as tentativas
                tentativas++;

                //SE tentou 200x e não teve sucesso ele sai do laço
                if (tentativas > 200)
                {
                    break; //Sai do laço
                }

                //Escolhe uma posição no eixo X e Y para o inimigo nascer
                spawnPosition.x = UnityEngine.Random.Range(xMin, xMax);
                spawnPosition.y = UnityEngine.Random.Range(yMin, yMax);

                //Variável que cuida da chance 
                float chance = UnityEngine.Random.Range(0f, level);

                //SE a chance for maior que 4, ele começa a criar mais inimigos do tipo 2
                if (chance > 4f)
                {
                    arrayInd = 1;
                }
                else
                {
                    arrayInd = 0;
                }

                //Checa se existe alguém no meu lugar
                bool colisao = PositionCheck(spawnPosition, inimigos[arrayInd].transform.localScale);

                //Cria a instancia do inimigo SE não tiver ninguém na mesma posição que eu
                if (colisao)
                {
                    continue; //SE ele colidir com alguém, ele continua o método
                }
                
                GameObject novoInimigo = Instantiate(inimigos[arrayInd], spawnPosition, Quaternion.identity);
                

                //Aumenta a QTD de inimigos mortos
                qtdInimigos++;
            }
        }
    }

    //MÉTODO que chama o boss após chegar no level 10
    private void CallBoss()
    {
        //Diminui o timer de criação do BOSS
        if (createBossAnimTime > 0 && level >= 10) createBossAnimTime -= Time.deltaTime;

        //Assim que chega no level 10, ele cria a animação do BOSS na cena e apenas após ele passar 3 segundos
        if (level >= 10 && createBossAnimTime <= 0 && !bossAnimCreated)
        {
            //Cria a instancia na posição númerada
            GameObject bossAnimation = Instantiate(bossAnim, createBossAnimPosition.position, Quaternion.identity);

            //Após 7 segundos, ele destroy a animação
            Destroy(bossAnimation, 7f);

            //Flag que já criou a animação
            bossAnimCreated = true;

            //Flag para criar o boss
            bossCreated = true;
        }

        //Criou a animação e já começa a diminuir o timer para criar o boss verdadeiro
        if (createBossTime > 0 && bossAnimCreated) createBossTime -= Time.deltaTime;

        //E após 7 segundos ele cria o boss e usa TAG para garantir que crie o BOSS só uma vez
        if (createBossTime <= 0 && bossCreated)
        {
            //Crio o BOSS
            GameObject bossReal = Instantiate(boss, createBossPosition.position, Quaternion.identity);

            //Já criei o boss
            bossCreated = false;
        }
    }

    //Método de verificar se o inimigo já morreu
    public void EnemieQTD()
    {
        //Diminui a quantidade de inimigos criados, que morreram
        qtdInimigos--;

        //Reseta o timer SE todos inimigos já morreram
        if (qtdInimigos <= 0)
        {
            createTimeEnemies = waitTime;
        }
    }

    //Método de ganhar pontos
    public void GanhaPontos(int pontos)
    {
        //OBJETIVO DO MÉTODO
        /*
         * Fazer ganhar pontos de acordo com o parâmetro
         * Ganhar level a cada 100 PONTOS
         * Exibir o texto do LEVEL e dos PONTOS
         * Aumenta o tempo de criação das WAVES de acordo com o level
        */

        //Ganho pontos
        this.pontos += pontos;

        //SE meu pontos forem aumentando de 200 em 200, eu subo de level
        level = (this.pontos / 200) + 1;

        //Exibe o texto dos pontos de acordo com os pontos atuais
        textoPontos.text = Mathf.Round(this.pontos).ToString();

        //Exibe o texto do level
        textoLevel.text = Mathf.Round(level).ToString();

        //Aumenta o tempo de espera a cada level que passa
        //waitTime = level * 8f;
    }
}
