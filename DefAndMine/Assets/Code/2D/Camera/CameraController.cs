using UnityEngine;

public class CameraController : MonoBehaviour
{
   private Field _field;

   private void Start()
   {
      _field = FindObjectOfType<Field>();
   }


   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Q))
      {
         _field.Rotate(false);
      }
      else if (Input.GetKeyDown(KeyCode.E))
      {
         _field.Rotate(true);
      }
   }
}