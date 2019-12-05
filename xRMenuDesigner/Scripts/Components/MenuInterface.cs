using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInterface : MonoBehaviour
{
    protected int ButtonNum = 0;
    protected bool isRootMenu = false;

    public virtual void InitMenu(int _buttonNum, bool _isRootMenu)
    {
        ButtonNum = _buttonNum;
        isRootMenu = _isRootMenu;
    }

    public void _DisableSelf()
    {
        if (gameObject != null)
            gameObject.SetActive(false);
    }

    public void _ActiveSelf()
    {
        if (gameObject != null)
            gameObject.SetActive(true);
    }

    public void SwitchActive()
    {
        if (this.isActiveAndEnabled)
            _DisableSelf();
        else
            _ActiveSelf();
    }
}
