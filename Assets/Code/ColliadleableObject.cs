using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliadleableObject : MonoBehaviour
{
    private Collider2D z_collider;
    [SerializeField]
    private ContactFilter2D filter;
    private List<Collider2D> collidedobjects = new List<Collider2D>(1);

    protected virtual void Start()
    {
        z_collider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        z_collider.OverlapCollider(filter, collidedobjects);
        foreach(var i in collidedobjects)
        {
            OnCollided(i.gameObject);
        }

    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        Debug.Log("Collided with" + collidedObject.name);
    }
}
