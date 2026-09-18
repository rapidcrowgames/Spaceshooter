using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Chama antes do Start
    private void Awake()
    {
        //Não vou ser destruido quando mudar de cena
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método de Iniciar o jogo
    public void NewGame()
    {
        //Carrega a cena do jogo
        SceneManager.LoadScene(1);
    }

    //Método de ir para tela inicial
    public void InitialScene()
    {
        //Carrega a cena do jogo
        SceneManager.LoadScene(0);
    }

    //Método de fechar o jogo
    public void ExitGame()
    {
        //Fechando o jogo
        Application.Quit();
    }
}
