using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = .25f;
    public Vector3 direction =  new Vector3(1,1,1);
    public int damage = 0;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(Vector3.right);
    }
}
