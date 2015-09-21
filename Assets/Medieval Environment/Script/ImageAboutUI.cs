using UnityEngine;
using System.Collections;

public class ImageAboutUI : MonoBehaviour {

    public int currImageID = -1;
    public bool showTestBtn = true;

   public UILabel decLabel;

   DataManager dm = null;

   UIEventManager uiEventMgr = null;

   public GameObject TestButton;

   void OnEnable()
   {
       if (uiEventMgr == null)
           uiEventMgr = (FindObjectOfType(typeof(UIEventManager)) as UIEventManager);

       if (dm == null)
           dm = FindObjectOfType(typeof(DataManager)) as DataManager;

       currImageID = uiEventMgr.CurrClickImageID;

       showTestBtn = uiEventMgr.showTestBtn;

       if (currImageID != -1)
       {
           decLabel.text = dm.GetString("Image" + currImageID.ToString() + "Desc");
       }
        
       TestButton.SetActive(showTestBtn);
        
   }

    void OnDisable()
   {
       currImageID = -1;
       showTestBtn = true;
   }

}
