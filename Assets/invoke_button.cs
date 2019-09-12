using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invoke_button : MonoBehaviour {

    public void invokeButton(Button button)
    {
        button.onClick.Invoke();
    }
}
