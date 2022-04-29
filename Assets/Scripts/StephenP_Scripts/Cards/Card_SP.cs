using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_SP : MonoBehaviour
{
    public Sprite CardUpFace;
    public Sprite CardDownFace;
    protected abstract void OnPlay();
    protected abstract void OnSetAsTrap();
}
