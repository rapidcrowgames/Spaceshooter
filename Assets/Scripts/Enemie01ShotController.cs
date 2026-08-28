using UnityEngine;

public class Enemie01ShotController : MonoBehaviour
{

    //Variáveis de controle
    [SerializeField] private GameObject particulaTiro; //Pega o objeto da particula do tiro

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Colisor para se destruir com o DESTRUIDOR
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destruidor")) //Se eu colidir com o paredão
        {
            Destroy(gameObject); //Destruo eu mesmo
        }

        //SE eu colidir com o player
        if (other.CompareTag("Jogador"))
        {
            //Crio a animação do impacto do tiro
            GameObject shotParty = Instantiate(particulaTiro, transform.position, Quaternion.identity);

            //Destruo a particula do tiro depois de 1f segundo
            Destroy(shotParty, 1f);

            //Me destruo
            Destroy(gameObject); 
        }
    }
}
