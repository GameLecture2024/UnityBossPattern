using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _lifeTime);   
    }
}
