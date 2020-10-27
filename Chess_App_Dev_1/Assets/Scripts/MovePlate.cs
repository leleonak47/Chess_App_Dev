using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    public PointRules pointRule;

    GameObject reference = null;

    // Board positions, not world positions
    int matrixX;
    int matrixY;

    // false: moviment, true: attacking
    public bool attack = false;

    public int AmountOfDamage;

    public void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            // Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        if (attack)
        {
            //cp é quem esta sendo atacado
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            //Enviando para a modal o nome da peça destruida, para fazer o calculo de dano
            controller.GetComponent<Game>().CalculoModal.GetComponent<CalculoModal>().chessPiece = cp;

            controller.GetComponent<Game>().ControlCalculateModal(true);
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

        OnPawnAtEndOfTable(reference, matrixY);
    }

    /// <summary>
    /// Quando o peão chegar no final do tabuleiro
    /// </summary>
    public void OnPawnAtEndOfTable(GameObject ChessPiece,int PosY)
    {
        if (ChessPiece.gameObject.name == "black_pawn" && PosY == 0)
        {
            controller.GetComponent<Game>().BlackPoints += pointRule.PawnHeal;

            Destroy(ChessPiece);

            print("Preto chegou ao final do tabuleiro");
        }
        else if (ChessPiece.gameObject.name.Contains("white_pawn") && PosY == 7)
        {
            controller.GetComponent<Game>().WhitePoints += pointRule.PawnHeal;

            Destroy(ChessPiece);

            print("Branco chegou ao final do tabuleiro");
        }
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
