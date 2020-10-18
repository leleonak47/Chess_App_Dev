using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Board positions, not world positions
    int matrixX;
    int matrixY;

    // false: moviment, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            //cp é quem esta sendo atacado
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            print(cp.name.Length);

            string army = cp.name.Substring(0, 5);
            string piece = cp.name.Substring(6, cp.name.Length - 6);

            print($"piece: {piece}| Army: {army}");

            int amountOfDamage = 0;

            switch (piece)
            {
                /*Matar o Rei = HitKill - isso nunca acontece pq antes de comer o rei, 
                vc bota ele em check mate e automaticamente ganha o jogo*/
                case "king": 
                    amountOfDamage = 1000;
                    if (army == "white") controller.GetComponent<Game>().Winner("pretas");
                    else if (army == "black") controller.GetComponent<Game>().Winner("brancas");
                break; 
                
                case "queen": amountOfDamage = 100; break;
                case "rock": amountOfDamage = 50; break;
                case "bishop": amountOfDamage = 30; break;
                case "knight": amountOfDamage = 30; break;
                case "pawn": amountOfDamage = 10; break;
            }

            controller.GetComponent<Game>().ControlCalculateModal(true);

            //Aqui eu preciso verificar a resposta da modal e aumentar o dano

            //if (army == "white")
            //{
            //    controller.GetComponent<Game>().WhitePoints -= amountOfDamage;
            //}
            //else // Se não for o branco, só pode ser o preto
            //{
            //    controller.GetComponent<Game>().BlackPoints -= amountOfDamage;
            //}

            Destroy(cp);
        }

        //Adicionar o movimento em questão ao histórico
        controller.GetComponent<Game>().moveHistory.Add(
            new MovementData(reference.gameObject.name,
            reference.gameObject,
            reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard(),
            matrixX,
            matrixY)
            );

        controller.GetComponent<Game>().SetPositionEmpty
            (
            reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard()
            );

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        reference.GetComponent<Chessman>().alreadyMove = true;

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }


    public void setCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void Setreference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
