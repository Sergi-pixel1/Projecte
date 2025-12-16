using UnityEngine;

public class Player2DNivell3 : MonoBehaviour
{
    [SerializeField] private float velocidad = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.Translate(Input.GetAxis("Horizontal") * velocidad * Time.deltaTime, 0.0f, 0.0f);


    }
}
