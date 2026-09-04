using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //VARIÁVEIS 

    //Variáveis para pegar objetos do jogo
    [SerializeField] private GameObject[] inimigos = { }; //Array que pega os inimigos do jogo

    //Variáveis de cena
    [SerializeField] private int pontos = 0; //Pontos do jogo
    [SerializeField] private int level = 1; //Level do jogo
    private float createTimeEnemies = 0f; //Tempo inicial para criar os inimigos
    [SerializeField] private float waitTime = 8f; //Tempo de espera para criar os inimigos
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

        #endregion
    }

    //MÉTODOS

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
        if (createTimeEnemies <= 0)
        {
            //Variáveis do laço
            int inimigosCriados = level * 4; //Cria 4 inimigos de acordo com level: [SE for level 2 cria 8, se for level 3 cria 12 e etc.]

            //Laço de repetição para criar uma QTD de inimigos por vez
            while (qtdInimigos < inimigosCriados)
            {

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

                //Cria a instancia do inimigo
                GameObject novoInimigo = Instantiate(inimigos[arrayInd], spawnPosition, Quaternion.identity);

                //Aumenta a QTD de inimigos mortos
                qtdInimigos++;

                //Reseta o timer SE todos inimigos já morreram
                if (qtdInimigos <= 0)
                {
                    createTimeEnemies = waitTime;
                }
            }
        }
    }

    //Método de verificar se o inimigo já morreu
    public void EnemieQTD()
    {
        //Diminui a quantidade de inimigos criados, que morreram
        qtdInimigos--;
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
