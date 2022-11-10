using UnityEngine;
using UnityEngine.UI;
public class FloatingText
{
    //variables to store if the text is actice or a reference to its gameobject or a  text field or motion or duration or lastshown to calculate the time diff.
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    //Show function to show the text
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }
    //Hide to hide the text
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    //Update function to check if its active and then hide it after the duration mentioned.
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
