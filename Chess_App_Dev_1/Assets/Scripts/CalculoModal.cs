using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Nome: Leonardo Oliveira Barcelos
/// Data: 19/09/2020
/// </summary>

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

    public void submit()
    {
        //gameObject.GetComponent<InputField>().interactable = false;

        if (ValidaCalculo(number1, number2, int.Parse(resposta.text), simbol))
        {
            UpdateTitle("Acertou!");
        }
        else
        {
            UpdateTitle("Errou!");
        }


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
        int sinal = Random.Range(1, 5);

        if (sinal == 1) simbol = "+";       // soma
        else if (sinal == 2) simbol = "-";  // subtração
        else if (sinal == 3) simbol = "*";  // multiplicação
        else if (sinal == 4) simbol = "/";  // divisão

        if ((sinal == 3) || (sinal == 4)) { number2 = Random.Range(1, 100); number1 = Random.Range(1, 100); }
        else { number2 = Random.Range(1, 1000); number1 = Random.Range(1, 1000); };

        UpdateTitle("Resolva a conta abaixo!");
        Number1.text = number1.ToString();
        Simbol.text = simbol;
        Number2.text = number2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTitle(string message)
    {
        title.text = message;
    }
}
