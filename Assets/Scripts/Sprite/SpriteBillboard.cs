using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    //based on this: https://www.youtube.com/watch?v=FjJJ_I9zqJo&t=5s
    //because I know I'll be here later to figure out how I did this. 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        var cam = GameManager.Instance.GetCurrentCamera(); 

        if (cam == null )
            return;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,cam.transform.rotation.eulerAngles.y, transform.eulerAngles.z);

    }
}
