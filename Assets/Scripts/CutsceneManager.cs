using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{

    public PlayableDirector tutorialCutscene;
    public PlayableDirector HypermetropiaCutscene;
    public PlayableDirector CataractCutscene;
    public PlayableDirector MonocromacyCutscene;
    public PlayableDirector endingCutscene;

    // private void Start()
    // {
    //     PhaseManager.Instance.OnPhaseChanged += PhaseManager_OnPhaseChanged;
    // }

    // private void PhaseManager_OnPhaseChanged(object sender, EventArgs e)
    // {
    //     StartCutScene();
    // }

    public void StartCutScene()
    {
        switch (PhaseManager.Instance.phase)
        {
            case PhaseManager.Phase.Tutorial:
                break;
            case PhaseManager.Phase.PhaseHypermetropia:
                HypermetropiaCutscene.Play();
                break;
            case PhaseManager.Phase.PhaseCataract:
                CataractCutscene.Play();
                break;
            case PhaseManager.Phase.PhaseMonochromacy:
                MonocromacyCutscene.Play();
                break;
            case PhaseManager.Phase.PhaseBlind:
                StartCoroutine(PlayEnding());
                break;
        }
    }

    IEnumerator PlayEnding()
    {
        endingCutscene.Play();
        yield return new WaitForSeconds(((float)endingCutscene.duration));
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
