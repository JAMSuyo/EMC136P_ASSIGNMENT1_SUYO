using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public float offScreenDestroyTime = 5f; 
    private bool isOffScreen;               
    private float offScreenTimer = 0;       

     public delegate void Lose();
     public static event Lose LoseEvent;

    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 10);

        
        if (IsOffScreen())
        {
            
            offScreenTimer += Time.deltaTime;
            if (offScreenTimer >= offScreenDestroyTime)
            {
                Destroy(gameObject);  
            }
        }
        else
        {
            offScreenTimer = 0;  
        }
    }

    private bool IsOffScreen()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("GAMEOVER");
            LoseEvent?.Invoke();
        }
    }
}
