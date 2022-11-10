using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    //All of the text objects will be formed in context of TextPrefabs which will be contained inside a text Container.
    public GameObject textContainer;
    public GameObject textPrefabs;

    //A list to store all the floating texts
    private List<FloatingText> floatingTexts = new List<FloatingText>();


    //This update is created as the FloatingText class does not have a real update method so it wont be able to update automatically.
    private void Update()
    {
        //This statement will update all the floating texts inside the array each second.
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Vector3 motion, float duration, Vector3 position, Color color)
    {
        //Create a new floating text object and initialize it using the function which will give u a new or old flating text from the pool of the texts avaible which can be reused.
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        //This converts the world space to the screen space so it can be used in the UI.
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    //Creating boolean funtion
    //This create a funtion which will return us a floating text whenever we want even if its available or not.
    private FloatingText GetFloatingText()
    {
        //This line is used to find a floating text inside the array which is not active.
        FloatingText txt = floatingTexts.Find(t => !t.active);

        //if there isnt any unactive text availiable then
        if (txt == null)
        {
            //then we create a new floating text with all the properties
            txt = new FloatingText();
            //instantiating the newly created floating text's game object with new GameObject named textPrefabs
            txt.go = Instantiate(textPrefabs);
            //The parent of this new game object will gonna be the tranform of the textContainer
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;

    }
}
