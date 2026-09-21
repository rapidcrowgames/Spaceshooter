using UnityEngine;

public class ShieldController : MonoBehaviour
{

    #region VARIÁVEIS

    //Variáveis de SOM
    [SerializeField] private AudioSource audioSource; //Pega a caixa de som
    [SerializeField] private AudioClip shieldClose; //Som do escudo fechando

    #endregion

    #region MÉTODOS

    //MÉTODO DE REPRODUZIR O SOM DO ESCUDO FECHANDO
    private void ShieldClose()
    {
        //Reproduz o som apenas uma vez
        audioSource.PlayOneShot(shieldClose);
    }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
