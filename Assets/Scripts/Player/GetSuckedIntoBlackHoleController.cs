using UnityEngine;
public class GetSuckedIntoBlackHoleController : MonoBehaviour
{
    private static float BlackHoleSuckingSpeed = 1.5f; 
    
    private GameObject _blackHole;
    
    public void SetBlackHole(GameObject blackHoleGameObject)
    {
        _blackHole = blackHoleGameObject;
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _blackHole.transform.position, BlackHoleSuckingSpeed * Time.deltaTime);
        transform.Rotate(Vector3.back);
        if (VectorMath.AreVectorsEqual(transform.position, _blackHole.transform.position))
        {
            Destroy(_blackHole);
            Destroy(gameObject);
        }
    }
}