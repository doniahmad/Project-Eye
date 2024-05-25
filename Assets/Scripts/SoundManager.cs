using System;
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
        TableWastafel.OnOpenWashtafel += TableWastafel_OnOpenWashtafle;
        RakDoor.OnActionRakDoor += RakDoor_OnActionRakDoor;
        Door.OnActionDoor += Door_OnActionRakDoor;
        RakDoorSlide.OnActionRakDoorSlide += RakDoorSlide_OnActionRakDoorSlide;
        PlaceableItem.OnActionDropItem += PlaceableItem_OnActionDropItem;
        Item.OnActionTakeItem += Item_OnActionTakeItem;
        GloveBox.OnActionUseGlove += GloveBox_OnActionUseGlove;
    }

    private void GloveBox_OnActionUseGlove(object sender, EventArgs e)
    {
        GloveBox gloveBox = sender as GloveBox;
        PlaySound(audioClipRefSO.glove, gloveBox.transform.position, volume);
    }

    private void Item_OnActionTakeItem(object sender, EventArgs e)
    {
        Item item = sender as Item;
        PlaySound(audioClipRefSO.pickup, item.transform.position, volume);
    }

    private void PlaceableItem_OnActionDropItem(object sender, PlaceableItem.DropItem e)
    {
        PlaySound(audioClipRefSO.drop, e.placementPosition, volume);
    }

    private void RakDoorSlide_OnActionRakDoorSlide(object sender, EventArgs e)
    {
        RakDoorSlide rakDoorSlide = sender as RakDoorSlide;
        PlaySound(audioClipRefSO.openSlide, rakDoorSlide.transform.position, volume);
    }

    private void Door_OnActionRakDoor(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.openDoor, PlayerController.Instance.transform.position, volume);
    }

    private void RakDoor_OnActionRakDoor(object sender, EventArgs e)
    {
        RakDoor rakDoor = sender as RakDoor;
        PlaySound(audioClipRefSO.openDoor, rakDoor.transform.position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstep(Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClipRefSO.footstep[UnityEngine.Random.Range(0, audioClipRefSO.footstep.Length)], position, volume);
    }

    private void TableWastafel_OnOpenWashtafle(object sender, EventArgs e)
    {
        TableWastafel tableWastafel = sender as TableWastafel;
        PlaySound(audioClipRefSO.washtafel, tableWastafel.transform.position, volume);
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
