using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance { get; set; }

    public GameObject notificationPopUp;
    public Animator notificationAnim;
    public TextMeshProUGUI notificationText;

    private Coroutine currentNotificationCoroutine;
    private bool isNotificationActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerNotification(string text)
    {
        if (currentNotificationCoroutine != null)
        {
            StopCoroutine(currentNotificationCoroutine);
            StartCoroutine(ForceCloseAndTriggerNewNotification(text));
        }
        else
        {
            DisplayNotification(text);
        }
    }

    private void DisplayNotification(string text)
    {
        notificationText.text = text;
        notificationPopUp.SetActive(true);
        notificationAnim.Play("In");
        isNotificationActive = true;
        currentNotificationCoroutine = StartCoroutine(CheckNotification());
    }

    private IEnumerator CheckNotification()
    {
        yield return new WaitForSeconds(5f);
        notificationAnim.Play("Out");
        yield return new WaitForSeconds(notificationAnim.GetCurrentAnimatorStateInfo(0).length);
        notificationPopUp.SetActive(false);
        currentNotificationCoroutine = null;
        isNotificationActive = false;
    }

    private IEnumerator ForceCloseAndTriggerNewNotification(string newText)
    {
        if (isNotificationActive)
        {
            notificationAnim.Play("Out");
            yield return new WaitForSeconds(notificationAnim.GetCurrentAnimatorStateInfo(0).length);
            notificationPopUp.SetActive(false);
            isNotificationActive = false;
        }
        DisplayNotification(newText);
    }

}
