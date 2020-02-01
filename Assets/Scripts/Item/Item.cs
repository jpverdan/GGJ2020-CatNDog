using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Parte[] listaDePartes;
    public float raioSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Quebrar();
        }
    }

    void Quebrar()
    {
        foreach (var _parte in listaDePartes)
        {
            float _x = Random.Range(-raioSpawn, raioSpawn);
            float _y = Random.Range(-raioSpawn, raioSpawn);
            var _objeto = Instantiate(_parte, transform.position + new Vector3(_x, _y, 0), Quaternion.identity);
            _objeto.transform.parent = transform;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioSpawn);
    }
}
