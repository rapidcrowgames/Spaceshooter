using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    //Variáveis do PAI
    [SerializeField] protected int life;
    [SerializeField] protected GameObject particulaMorte; //Pega a particula de morte do inimigo
    [SerializeField] protected float velocidade; // Velocidade de movimento do inimigo
    [SerializeField] protected float shotVel; // Velocidade do projétil do tiro

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// MÉTODOS
    /// </summary>

    //Método de morte
    public void DeathEnemy()
    {
        //Variável para descobrir se estou visivel
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        //SE minha vida chegar a 0 ou menor, eu me destruo / morro
        if (life <= 0)
        {
            //Crio a particula da morte
            GameObject deathParty = Instantiate(particulaMorte, transform.position, Quaternion.identity);

            //Depois de 1f segundos eu me destruo
            Destroy(deathParty, 1f);

            Destroy(gameObject);
        }
    }

    //Método de perder vida
    public void PerdeVida(int value)
    {
        //SE o tiro do player me acertar eu perco um determinado valor de vida
        //SE eu estiver na tela
        if (transform.position.y < 4.7f)
        {
            life -= value;
        }
    }

    //SE eu colidir com o destruidor, eu morro
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor"))
        {
            //Me destruo
            Destroy(gameObject, 0.5f);
        }
    }
}
