using TMPro;
using UnityEngine;
public class KeySwitch : MonoBehaviour
{
    public TMP_InputField inputField;
    private void Start()
    {
        inputField.onValidateInput += (text, index, addedChar) =>
        {
            if (addedChar != '\n')
            {
                inputField.text = "";
            }

            return ' ';
        };
    }

    private void OnGUI()
    {
        if (!inputField.isFocused)
        {
            return;
        }
        var e = Event.current;
        if (!e.isKey || e.keyCode is KeyCode.None or KeyCode.Escape or KeyCode.KeypadEnter)
        {
            return;
        }
        inputField.text = e.keyCode.ToString();
        inputField.MoveTextEnd(false);
    }
}
