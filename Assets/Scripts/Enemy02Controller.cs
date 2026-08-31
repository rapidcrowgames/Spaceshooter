using System;
using UnityEngine;

public class Enemy02Controller : EnemyFather
{
    //Variáveis de controle ////

    //Pegando componentes em variáveis
    [SerializeField] private Rigidbody2D meuRB; //RigidBody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pegando meu RigidBody 2D
        meuRB = GetComponent<Rigidbody2D>();

        //Dando Velocidade ao meu Rigid
        meuRB.linearVelocity = new Vector2(0f, -velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        //Usa os métodos do pai
        DeathEnemy(); //Método de morrer
    }
}
