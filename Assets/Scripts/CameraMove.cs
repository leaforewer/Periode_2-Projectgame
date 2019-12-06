using UnityEngine;
 using System.Collections;
 
 public class CameraMove : MonoBehaviour {
 
 
     public void PlayAnimation() {
         GetComponent<Animator>().SetBool("Throw",true);
     }
 
 }
