using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystems.BattleUI
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private Text lbDialog;
        [SerializeField] private float textSpeed;

        public void EnableDialog() => gameObject.SetActive(true);

        public void DisableDialog() => gameObject.SetActive(false);
        
        public IEnumerator SetDialogText(string content, bool isSetActive = false)
        {
            gameObject.SetActive(true);
            var contentText = "";
            foreach (var item in content.ToCharArray())
            {
                contentText += item;
                yield return new WaitForSeconds(textSpeed);
                lbDialog.text = contentText;
            }
            yield return new WaitForSeconds(0.5f);
            if (isSetActive != false) yield break;
            
            lbDialog.text = "";
            gameObject.SetActive(false);
        }
    }
}
