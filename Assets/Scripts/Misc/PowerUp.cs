using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //VARIÁVEIS

    //Variáveis de pegar componentes e objetos do jogo
    [SerializeField] private Rigidbody2D meuRB; //Pego meu RigidBody2D

    [SerializeField] private GameObject destroyAnim; //Pega a animação de destruido

    [SerializeField] private Transform transCam; //Pega o Transform da câmera

    //Variáveis de controle
    private int vel = 1; //Velocidade de movimento do powerup
    private float timerDestroy = 3f; //Timer para me auto-destruir após 3 segundos

    //VARIÁVEIS DE SOM
    [SerializeField] private AudioSource audioSource; //Pega a caixa de som
    [SerializeField] private AudioClip powerUpSound; //Pega o som do PowerUp

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pego meu RB
        meuRB = GetComponent<Rigidbody2D>();

        //SE eu fui criado, eu vou para uma direção aleatória
        var direction = UnityEngine.Random.Range(-1f, 1f);
        meuRB.linearVelocity = new Vector2(direction, direction) * vel;
    }

    // Update is called once per frame
    void Update()
    {
        //USA OS MÉTODOS
        SelfDestroy();
    }

    //SEMPRE QUE EU COLIDIR COM O PLAYER eu aumento o nível do tiro dele
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            //Acesso o player
            var player = FindAnyObjectByType<PlayerController>();

            //Reproduz o som do powerUp
            AudioSource.PlayClipAtPoint(powerUpSound, transCam.position);

            //Chamo o método dele
            player.ChangeShot();

            //Depois de fazer isso eu reproduzo a animação (Anim do impacto do tiro)
            GameObject particula = Instantiate(destroyAnim, transform.position, Quaternion.identity);

            //Destruo a particula
            Destroy(particula, 1f);

            //Me destruo depois
            Destroy(gameObject, 0.01f);

        }
    }

    //MÉTODOS//

    //Método de me destruir após 3 segundos
    private void SelfDestroy()
    {
        //OBJETIVO DO MÉTODO
        /*
         * Depois que eu fui criado, eu conto 3 segundos até me destruir e sumir
         */

        //Diminui o timer se ele já não estiver em 0
        if (timerDestroy > 0) timerDestroy -= Time.deltaTime;

        //SE chegar em 0 o timer
        if (timerDestroy <= 0)
        {
            //Eu me destruo
            //Depois de fazer isso eu reproduzo a animação (Anim do impacto do tiro)
            GameObject particula = Instantiate(destroyAnim, transform.position, Quaternion.identity);

            //Destruo a particula
            Destroy(particula, 1f);

            //Me destruo depois
            Destroy(gameObject, 0.01f);

            //Reseto o timer só por precaução
            timerDestroy = 3f;
        }
    }
}
