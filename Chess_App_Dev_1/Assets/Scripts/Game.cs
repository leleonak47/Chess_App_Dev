using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    // Start is called before the first frame update
    void Start()
    {
        //chesspiece.SetActive(true);
        Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
    }
}
