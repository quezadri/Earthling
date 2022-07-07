using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    public string textValue;
    public Text textElement;

   private void OnTriggerEnter2D(Collider2D collision){
 if (collision.tag == "Player"){
     textElement.text=textValue;
    }
}
    
    



     
}
