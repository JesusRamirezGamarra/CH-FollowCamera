using UnityEngine;

public class Destruir_objetos : MonoBehaviour
{
    public float destroyTime;
    void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
