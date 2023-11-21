using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FirstOpen : MonoBehaviour
{
    [SerializeField] private StartAnimation dropdownAnim;
    [SerializeField] private StartAnimation loginAnim;
    [SerializeField] private GameObject login;
    [SerializeField] private bool isEnter;
    [SerializeField] private float duration = 1f;
    [SerializeField] private UnityEvent _event;

    public void ChangeIsEnter()
    {
        isEnter = false;
    }
    
    public void ChangeIsEnterTrue()
    {
        isEnter = true;
    }
    
    public void Enter()
    {
        if (isEnter)
            return;
        StartCoroutine(WaitAnim());
    }

    IEnumerator WaitAnim()
    {
        isEnter = true;
        yield return new WaitForSeconds(duration);
        dropdownAnim.ReturnToOpen(dropdownAnim.ReturnLoginLeft);
        loginAnim.ReturnToOpen(() =>
        {
            
          
            _event?.Invoke();
            login?.SetActive(true);
        });
    }
}