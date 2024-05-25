using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu()]
public class BookSO : ScriptableObject
{
    public string bookType;
    public List<Sprite> listBook;
}
