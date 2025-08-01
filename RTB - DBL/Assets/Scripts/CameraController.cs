using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   // Calling the player's GameObject 
   public GameObject player;

   // The distance between the camera and the player.
   private Vector3 offset;

   // Start is called before the first frame update.
   void Start()
   {
      // Calculate the initial position from the camera and the player 
      offset = transform.position - player.transform.position;
   }

   // This tells everything eles that its done 
   void LateUpdate()
   {
      // Maintain the same offset between the camera and player throughout the game.
      transform.position = player.transform.position + offset;
   }
}