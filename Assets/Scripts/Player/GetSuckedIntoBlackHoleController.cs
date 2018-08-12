using UnityEngine;
public class GetSuckedIntoBlackHoleController : MonoBehaviour
{
    private static float BlackHoleSuckingSpeed = 1.5f;
    private float deathAnimationTimer = 5f;
    
    private GameObject _blackHole;
    
    public void SetBlackHole(GameObject blackHoleGameObject)
    {
        _blackHole = blackHoleGameObject;
    }

    private void Start() 
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _blackHole.transform.position, BlackHoleSuckingSpeed * Time.deltaTime);
        transform.Rotate(Vector3.back);

        deathAnimationTimer -= Time.deltaTime;
        if (deathAnimationTimer <= 0f) {
            Destroy(gameObject);
            Destroy(_blackHole);
        }
    }
}