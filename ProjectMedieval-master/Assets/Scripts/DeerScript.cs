using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeerScript : MonoBehaviour {

    
    public float timer;

    
    
    
    
    public float newtarget;
    
    
    
    public Animation an;
    
    public Vector3 target;

    void Start()
    {
       
        an = GetComponent<Animation>();
       
       
    }

    void Update()
    {
      
        timer += Time.deltaTime;

        if (timer >= newtarget)
        {
            newTarget();
            timer = 0;
            

        }
        
    }

    void newTarget()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;
        
        float xPos = myX + Random.Range(myX - 100, myX + 100);
        float zPos = myZ + Random.Range(myZ - 100, myZ + 100);

        target = new Vector3(xPos, gameObject.transform.position.y, zPos);
        
        
    }

}
