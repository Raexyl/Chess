using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    protected Vector2[] possibleMoves;
    protected Collider2D coll2D;

    // Start is called before the first frame update
    public void Awake()
    {
        if((coll2D = GetComponent<Collider2D>()) == null) { Debug.Log(gameObject); Debug.Log("Error - Missing vital component."); Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {

    }
}
