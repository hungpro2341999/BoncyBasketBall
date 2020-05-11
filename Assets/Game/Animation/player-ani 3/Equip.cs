using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equip",menuName = "Use")]
public class Equip : ScriptableObject
{
   	public enum TypeChange { hand, Leg, Head }
	public TypeChange equipType;
		public Sprite sprite;
		public string description;
		public int yourStats;
	
}
