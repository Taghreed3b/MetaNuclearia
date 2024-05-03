// (c) Copyright Evolite Studio. All rights reserved.
//Website: https://www.evolite-studio.com
using UnityEngine;
using UnityEditor;
using TMPro;

#if UNITY_EDITOR

public class FixText : Editor
{
    [MenuItem("Tools/Arabic Writer/Fix selected text #F")]

    static void FixSelectedTexts()
    {
        GameObject SelectedText;
        SelectedText = Selection.activeGameObject;

      if(SelectedText != null){
        if(SelectedText.GetComponent<TextMeshPro>() != null){
      SelectedText.GetComponent<TextMeshPro>().text  = ArabicSupport.Fix(SelectedText.GetComponent<TextMeshPro>().text, true, false);
    }
    else if (SelectedText.GetComponent<TextMeshProUGUI>() != null)
    {
      SelectedText.GetComponent<TextMeshProUGUI>().text  = ArabicSupport.Fix(SelectedText.GetComponent<TextMeshProUGUI>().text, true, false);

    }
    else{
      Debug.Log("There's no text to fix! :) ");
    }
    }  
    else{
      Debug.Log("There's no text to fix! :) ");
    }


    
    }
}
#endif