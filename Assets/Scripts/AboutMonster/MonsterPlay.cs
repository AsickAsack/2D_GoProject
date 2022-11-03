using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterPlay : MonoBehaviour
{

    Monster monster;
    float Mass;
    float Drag;
    float Skill_Range;

    public abstract void Initialize();
    public abstract void Skill();






}
