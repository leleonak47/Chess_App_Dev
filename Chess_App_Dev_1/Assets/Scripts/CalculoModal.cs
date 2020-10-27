using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Nome: Leonardo Oliveira Barcelos
/// Data: 19/09/2020
/// </summary>

public enum Operador
{
    Soma = 1,
    Subtracao = 2,
    multiplicacao = 3,
    divisao = 4
}
public class CalculoModal : MonoBehaviour
{
    private int number1;
    private int number2;
    private string simbol;

    public Text title;
    public Text Number1;
    public Text Simbol;
    public Text Number2;
    public InputField resposta;

    public int AmountOfDamage;
    public PointRules DmgRule;
    public GameObject chessPiece;

    public void submit()
    {
        StartCoroutine(Submit(1));
    }

    public void OnClosePressed()
    {
        ApplyDamage(1);
        SetupModal();
    }

    public bool ValidaCalculo(int valor1, int valor2, int resultado, string sinal)
    {
        try
        {
            if (sinal == "+")       Assert.IsTrue((valor1 + valor2) == resultado);  // soma
            else if (sinal == "-")  Assert.IsTrue((valor1 - valor2) == resultado);  // subtração
            else if (sinal == "*")  Assert.IsTrue((valor1 * valor2) == resultado);  // multiplicação
            else if (sinal == "/")  Assert.IsTrue((valor1 / valor2) == resultado);  // divisão

            return true;
        }catch
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupModal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTitle(string message)
    {
        title.text = message;
    }

    void ApplyDamage(float damageMultiplier = 1)
    {
        string army = chessPiece.name.Substring(0, 5);
        string piece = chessPiece.name.Substring(6, chessPiece.name.Length - 6);

        print($"piece: {piece}| Army: {army}");

        switch (piece)
        {
            /*Matar o Rei = HitKill - isso nunca acontece pq antes de comer o rei, 
            vc bota ele em check mate e automaticamente ganha o jogo*/
            case "king":
                AmountOfDamage = DmgRule.KingDamage;
                if (army == "white") GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().Winner("pretas");
                else if (army == "black") GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().Winner("brancas");
                break;

            case "queen": AmountOfDamage = DmgRule.KingDamage; break;
            case "rook": AmountOfDamage = DmgRule.RookDamage; break;
            case "bishop": AmountOfDamage = DmgRule.BishopDamage; break;
            case "knight": AmountOfDamage = DmgRule.KnightDamage; break;
            case "pawn": AmountOfDamage = DmgRule.PawnDamage; break;
        }

        if (army == "white")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().WhitePoints -= (int)(AmountOfDamage * damageMultiplier);
        }
        else // Se não for o branco, só pode ser o preto
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().BlackPoints -= (int)(AmountOfDamage * damageMultiplier);
        }

        Destroy(chessPiece);
    }

    void SetupModal()
    {
        Operador sinal = (Operador)Random.Range(1, 5); 
        //int sinal = Random.Range(1, 5);

        if (sinal == Operador.Soma) simbol = "+";
        else if (sinal == Operador.Subtracao) simbol = "-";
        else if (sinal == Operador.multiplicacao) simbol = "*";
        else if (sinal == Operador.divisao) simbol = "/";

        if (sinal == Operador.divisao)
        { 
            number1 = Random.Range(1, 50) * 2;
            number2 = Random.Range(1, number1 < 10 ? (number1 + 1) : 10);
        }
        else if(sinal == Operador.multiplicacao)
        {
            number1 = Random.Range(1, 100);
            number2 = Random.Range(1, number1);
        }
        else if (sinal == Operador.Subtracao)
        {
            number1 = Random.Range(1, 1000);
            number2 = Random.Range(1, number1);
        }
        else
        {
            number2 = Random.Range(1, 1000);
            number1 = Random.Range(1, 1000);
        };

        UpdateTitle("Resolva a conta abaixo!");
        Number1.text = number1.ToString();
        Simbol.text = simbol;
        Number2.text = number2.ToString();
        resposta.text = "";
    }

    IEnumerator Submit(float seconds)
    {
        //gameObject.GetComponent<InputField>().interactable = false;

        if (resposta.text == string.Empty)
        {
            UpdateTitle("Por favor, informe um valor ou clique no X");
            yield return null;
        }

        if (ValidaCalculo(number1, number2, int.Parse(resposta.text), simbol))
        {
            UpdateTitle("Acertou!");
            ApplyDamage(DmgRule.HitDamageMultiplier);
        }
        else
        {
            UpdateTitle("Errou!");
            ApplyDamage(DmgRule.MissDamageMultiplier);
        }

        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);

        SetupModal();
    }
}
