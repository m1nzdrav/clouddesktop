// Copyright (c) 2015 - 2019 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

#if dUI_Playmaker

using Doozy.Engine.Soundy;
using HutongGames.PlayMaker;
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global

namespace Doozy.Integrations.Playmaker
{
    // ReSharper disable once UnusedMember.Global
    [ActionCategory("Soundy")]
    [HutongGames.PlayMaker.Tooltip("_play the passed AudioClip (with Soundy)")]
    public class PlayAudioClip : FsmStateAction
    {
        [UIHint(UIHint.FsmBool)]
        public FsmBool DebugMode;

        [UIHint(UIHint.Variable)]
        public AudioClip AudioClip;

        [UIHint(UIHint.FsmBool)]
        [HutongGames.PlayMaker.Tooltip("If TRUE this action will finish immediately")]
        public FsmBool FinishImmediately;
        
        public override void Reset()
        {
            DebugMode = new FsmBool {UseVariable = false, Value = false};
            AudioClip = null;
            FinishImmediately = new FsmBool { UseVariable = false, Value = true };
        }

        public override void OnEnter()
        {
            if (DebugMode.Value) DDebug.Log("Playmaker - State Name [" + State.Name + "] - _play AudioClip");
            SoundyManager._play(AudioClip);
            if (FinishImmediately.Value) Finish();
        }
    }
}

#endif