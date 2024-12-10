using UnityEngine;

[CreateAssetMenu(menuName = "ComboConfig/Player")]
public class MeleeAttackConfig : ScriptableObject
{
    public string AnimationName;
    public float TransitionDuration;
    public int ComboStateIndex = -1;
    public float ComboAttackTime;
}
