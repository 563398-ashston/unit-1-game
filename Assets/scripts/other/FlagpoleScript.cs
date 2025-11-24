using UnityEngine;

public class FlagpoleScript : MonoBehaviour
{
    public Transform resetPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().resetPoint = resetPoint;
            collision.gameObject.GetComponent<HealthScript>().ResetHealth();


            print("reset point is " + resetPoint);
        }
    }
}
