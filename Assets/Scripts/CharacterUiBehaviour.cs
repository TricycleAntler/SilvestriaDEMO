using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterUiBehaviour", menuName ="ScriptableObjects/CharacterUiBehaviour")]
public class CharacterUiBehaviour : ScriptableObject
{
    public string tagName;
    public string layout;
     public Sprite characterSprite;
    public AnimationClip animClip;

}