using System;
using UnityEngine;

public class LevelMove : MonoBehaviour
{
  public GameObject interior;  
  public GameObject exterior;  

  private void OnTriggerEnter2D(Collider2D other)
  {
      if (other.CompareTag("Player"))
      {
          if (interior != null) interior.SetActive(true);
          if (exterior != null) exterior.SetActive(false);
      }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
      if (other.CompareTag("Player"))
      {
          if (interior != null) interior.SetActive(false);
          if (exterior != null) exterior.SetActive(true);
      }
  }    
  
}
