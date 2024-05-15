using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{

    public PlayableDirector tutorialCutscene;
    public PlayableDirector HypermetropiaCutscene;

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
        }
    }
}
