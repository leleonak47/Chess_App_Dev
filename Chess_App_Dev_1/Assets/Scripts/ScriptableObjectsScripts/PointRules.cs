using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageRule", menuName = "Scriptable/DamageRule")]
public class PointRules : ScriptableObject
{
    /// <summary>
    /// Informações de dano e multiplicadores de dano
    /// </summary>
    public int KingDamage;
    public int QueenDamage;
    public int RookDamage;
    public int BishopDamage;
    public int KnightDamage;
    public int PawnDamage;
    public float MissDamageMultiplier;
    public float HitDamageMultiplier;

    /// <summary>
    /// Informações de pontos adicionais
    /// </summary>
    public int PawnHeal; //Quando o peão chega do outro lado do tabuleiro, ganha alguns pontos
}
