using UnityEngine;
using TMPro;
using System.Collections;

public class ChadController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_chad;
    [SerializeField]
    private Animator animator;

    private string lastText = "";

    void Start()
    {
        lbl_chad.gameObject.SetActive(false);
    }

    void Update()
    {
        string currentText = GameManager.ChadText;

        if (!string.IsNullOrEmpty(currentText) && currentText != lastText)
        {
            lastText = currentText;
            ShowMessage(currentText);
        }
    }

    void ShowMessage(string message)
    {
        lbl_chad.text = message;
        lbl_chad.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(TalkRoutine());
    }

    IEnumerator TalkRoutine()
    {
        animator.SetBool("isTalking", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isTalking", false);

        lbl_chad.gameObject.SetActive(false); 

        GameManager.ChadText = "";
    }
}