using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int cost;            // 카드 가격 
    public string name;         // 카드 이름
    public Sprite sprite;       // 카드 이미지
    public TypeEnum type;

    public string description;  // 카드 설명

    public int attack;          // 카드 공격력
    public int defense;         // 카드 방어력
    public float count;       // 카드 확률
}

public enum TypeEnum
{
    공격,
    방어,
    스킬

}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
