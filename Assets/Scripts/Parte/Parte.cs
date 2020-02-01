using UnityEngine;

public class Parte : MonoBehaviour {

    public Color corOriginal;


    private void Start() {
        GetComponent<SpriteRenderer>().color = corOriginal;
        
    }

    private void Update() {
        
    }
}