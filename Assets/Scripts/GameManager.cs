using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Pega a instancia do GameManager
    public static GameManager Instance;

    //Chama antes do Start
    private void Awake()
    {
        //SE já existe um gameManager, então ele destrói os novos que são criados
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //Instancia é igual a isso
        Instance = this;

        //Torna ele persistente
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Coroutine para o Initial Scene ser executado em 2 segundos
    IEnumerator FirstScene()
    {
        yield return new WaitForSeconds(2f);

        //Carrega a cena do jogo após dela
        SceneManager.LoadScene(0);
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
        StartCoroutine(FirstScene());
    }

    //Método de fechar o jogo
    public void ExitGame()
    {
        //Fechando o jogo
        Application.Quit();
    }
}
