using UnityEngine;

public class GameController : MonoBehaviour
{
    //VARIÁVEIS 

    //Variáveis para pegar objetos do jogo
    [SerializeField] private GameObject[] inimigos = { }; //Array que pega os inimigos do jogo

    //Variáveis de cena
    private int pontos = 0; //Pontos do jogo
    private int level = 1; //Level do jogo
    private float createTimeEnemies = 0f; //Tempo inicial para criar os inimigos
    [SerializeField] private float waitTime = 5f; //Tempo de espera para criar os inimigos
    private Vector2 spawnPosition; //Variável que contem os eixos X e Y
    [SerializeField] private float xMax = -7f; //Variável que define o valor minimo do nascimento do inimigo no eixo X
    [SerializeField] private float xMin = 7f; //Variável que define o valor máximo do nascimento do inimigo no eixo X
    [SerializeField] private float yMin = 6f; //Variável que define o valor máximo do nascimento do inimigo no eixo Y
    [SerializeField] private float yMax = 10f; //Variável que define o valor máximo do nascimento do inimigo no eixo Y

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
            //Escolhe uma posição no eixo X e Y para o inimigo nascer
            spawnPosition.x = UnityEngine.Random.Range(xMin, xMax);
            spawnPosition.y = UnityEngine.Random.Range(yMin, yMax);

            //Cria a instancia do inimigo
            GameObject novoInimigo = Instantiate(inimigos[0], spawnPosition, Quaternion.identity);

            //Reseta o timer
            createTimeEnemies = waitTime;
        }
    }
}
