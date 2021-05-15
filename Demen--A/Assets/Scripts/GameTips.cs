using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTips : MonoBehaviour
{

    public string[] Tips;
    public TMP_Text tipsText;
    int number;
    // Start is called before the first frame update
    void Start()
    {
        Tips = new string[6];
        Tips[0] = "Tip: Remember, Dementia isn't a single disease, it's an overall term like heart disease.";
        Tips[1] = "Tip: Symptoms for Dementia can be any of the following; Memory loss, Thinking speed problems, Language problems, Misunderstanding things, Misjudging things, Problems with mood and problems with movement.";
        Tips[2] = "Tip: People who suffer from Dementia could also suffer from personality changes and loss of empathy.";
        Tips[3] = "Tip: If you ever get stuck, lose an item or put the items in the wrong order press T to reset the items.";
        Tips[4] = "Tip: Press R to see the recipe that you have to follow.";
        Tips[5] = "Tip: Anyone with Dementia will usually need help from their friends or relatives, inlcuding help with making decisions";

        number = Random.Range(0, Tips.Length);
        tipsText.text = Tips[number];

    }
}
