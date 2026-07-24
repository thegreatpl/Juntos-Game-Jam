using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Camera GetCurrentCamera()
    {
        return Camera.main;
    }
}
