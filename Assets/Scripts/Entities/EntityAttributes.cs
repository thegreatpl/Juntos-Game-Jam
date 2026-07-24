using UnityEngine;

public class EntityAttributes : MonoBehaviour
{

    public int Strength = 5;

    public int Dexterity = 5;

    public int Constitution = 5;

    public int Intelligence = 5;

    public int Charisma = 5;

    public int Wisdom = 5;


    public int MaxHP;

    public int CurrentHP; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP < 0)
            Destroy(gameObject);//do death better here. 
    }

    public float Speed { get { return Dexterity * 2.5f; } }
}
