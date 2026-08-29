using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    //Variáveis do PAI
    [SerializeField] private int life;
    [SerializeField] private GameObject particulaMorte; //Pega a particula de morte do inimigo
    [SerializeField] private float velocidade; // Velocidade de movimento do inimigo
    [SerializeField] private float shotVel; // Velocidade do projétil do tiro

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //USA OS MÉTODOS
        DeathEnemy();
    }


    /// <summary>
    /// MÉTODOS
    /// </summary>

    //Método de morte
    public void DeathEnemy()
    {
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
    public void perdeVida(int value)
    {
        //SE o tiro do player me acertar eu perco um determinado valor de vida
        life -= value;
    }
}
