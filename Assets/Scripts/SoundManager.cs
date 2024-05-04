using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUNDEFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUNDEFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        // DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        // DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        // CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        // BaseCounter.OnPlacedAnyObject += BaseCounter_OnPlacedAnyObject;
        // PlayerController.Instance.OnPlayerPickUp += PlayerController_OnPlayerPickUp;
        // TrashCounter.OnTrashedAnyObject += TrashCounter_OnTrashedAnyObject;
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstep(Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipRefSO.footstep[UnityEngine.Random.Range(0, audioClipRefSO.footstep.Length)], position, volume);
    }

    public void IncreaseVolume()
    {
        if (volume < 1f)
        {
            volume += .1f;
            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUNDEFFECTS_VOLUME, volume);
            PlayerPrefs.Save();
        }
    }

    public void DecreaseVolume()
    {
        if (volume > 0f)
        {
            volume -= .1f;
            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUNDEFFECTS_VOLUME, volume);
            PlayerPrefs.Save();
        }
    }

    public float GetVolume()
    {
        return volume;
    }
}
