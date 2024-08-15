
using Gameplay;
using UnityEngine;
using UnityEngine.UI;


public class SettingButtons : MonoBehaviour
{
   public bool isOpen;
   public GameObject stopPanel;
   
   public void OpenOrCloseStopPanel()
   {
      AudioManager.Instance.PlayInController("Click", "SFX");
      isOpen = !isOpen;
      if (isOpen)
      {
         EventHandler.CallOnGameStop();
         UIManager.Instance.settingButton.GetComponent<Image>().color = new Color(1, 1, 1, 0);
         stopPanel.SetActive(true);
      }
      else
      {
         EventHandler.CallOnGameContinue();
         UIManager.Instance.settingButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
         stopPanel.SetActive(false);
      }
   }

   public void ExitGame()
   {
      AudioManager.Instance.PlayInController("Click", "SFX");
      Application.Quit();
   }
}
