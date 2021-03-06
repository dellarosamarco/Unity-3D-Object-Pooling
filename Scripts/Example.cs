using UnityEngine;
using TMPro;

public class Example : MonoBehaviour
{
    public GameObject sphere;
    public Transform objectsContainer;
    public Pool pool;
    public TextMeshProUGUI pooledObjectsText;
    public int poolSize;

    private void Start()
    {
        pooledObjectsText.text = "Pooled objects : " + poolSize.ToString();

        pool = new Pool(
            mainObject : sphere, 
            poolSize : poolSize, 
            initialState : false, 
            parent : objectsContainer,
            startPosition : randomPos(),
            pooledObjectName : "Sphere"
        );
    }

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0)
        {
            //Spawn next object to a random position
            GameObject sphere = pool.spawnAt(Vector3.zero);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().velocity = randomVelocity();

            //Spawn object to a random position by giving its index
            //pool.spawnAt(randomPos(), index : 5);

            //Spawn a certain object to a random position
            //pool.spawnAt(randomPos(), pool.pool[5]);

            //Increase pool size
            //pool.increasePoolSize(5, initialState: true, parent : objectsContainer);

            timer = 0;
        }
    }

    private Vector3 randomPos()
    {
        Vector3 temp;
        temp.x = Random.Range(-1f, 1f);
        temp.y = Random.Range(1f, 1f);
        temp.z = Random.Range(-1f, 1f);
        return temp;
    }

    private Vector3 randomVelocity()
    {
        Vector3 temp;
        temp.x = Random.Range(-10f, 10f);
        temp.y = Random.Range(-10f, 15f);
        temp.z = Random.Range(-10f, 10f);
        return temp;
    }
}
