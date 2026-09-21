using UnityEngine;

public class BossAnimationFinal : MonoBehaviour
{

    #region MÉTODOS

    //Método de voltar a tela inicial
    private void BackNewGame()
    {
        //Pega o componente do GameManager
        var gameManager = FindFirstObjectByType<GameManager>();

        //Acessa o InitialScene
        gameManager.InitialScene();
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
