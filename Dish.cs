using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dish", menuName = "Dish")]
public class Dish : ScriptableObject
{
  public string dishName;
  public Sprite dishImage;
  public int dishPrice;
}
