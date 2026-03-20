using HarmonyLib;
using HutongGames.PlayMaker;
using System;
using Silkipelago.Constants.FSM;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(Fsm))]
    [HarmonyPatch(nameof(Fsm.Start))]
    public static class FSMUtilityStartPatch
    {
        // Cache to track which FSMs we've already redirected
        private static readonly System.Collections.Generic.HashSet<int> RedirectedFsms = new();

        public static void Prefix(Fsm __instance)
        {
            BasePatch.SafeExecuteVoid(() => RedirectFsmTransitionsAtStart(__instance), nameof(FSMUtilityStartPatch), nameof(Prefix));
        }

        private static void RedirectFsmTransitionsAtStart(Fsm fsmInstance)
        {
            if (fsmInstance == null)
                return;

            // Get the PlayMakerFSM component that owns this Fsm
            var playMakerFsm = fsmInstance.Owner as PlayMakerFSM;
            if (playMakerFsm == null || playMakerFsm.gameObject == null)
                return;

            // Only track Crest Upgrade Shrine Dialogue FSM
            if (fsmInstance.Name != EvaDialogueConstants.DialogueFsmName || 
                playMakerFsm.gameObject.name != EvaDialogueConstants.OwnerName)
                return;

            // Redirect all target states during initialization
            var fsmId = playMakerFsm.GetInstanceID();
            if (!RedirectedFsms.Contains(fsmId))
            {
                RedirectBindPrepareTransition(fsmInstance);
                RedirectSetPreDlgTransition(fsmInstance);
                RedirectUpgradeSlot1PreDlgTransition(fsmInstance);
                RedirectedFsms.Add(fsmId);
            }
        }

        private static void RedirectBindPrepareTransition(Fsm fsmInstance)
        {
            try
            {
                var bindPrepareState = fsmInstance.GetState(EvaDialogueConstants.BindPrepareName);
                if (bindPrepareState == null)
                    return;

                var transitions = bindPrepareState.Transitions;
                if (transitions == null || transitions.Length == 0)
                    return;

                foreach (var transition in transitions)
                {
                    if (transition.ToState == EvaDialogueConstants.BindReadyTransition)
                    {
                        transition.ToState = EvaDialogueConstants.EndDialogueName;
                        var endDialogueState = fsmInstance.GetState(EvaDialogueConstants.EndDialogueName);
                        if (endDialogueState != null)
                        {
                            transition.ToFsmState = endDialogueState;
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityStartPatch), nameof(RedirectBindPrepareTransition), ex);
            }
        }

        private static void RedirectSetPreDlgTransition(Fsm fsmInstance)
        {
            try
            {
                var setPreDlgState = fsmInstance.GetState(EvaDialogueConstants.SetPreDlgName);
                if (setPreDlgState == null)
                    return;

                var transitions = setPreDlgState.Transitions;
                if (transitions == null || transitions.Length == 0)
                    return;

                foreach (var transition in transitions)
                {
                    if (transition.ToState == EvaDialogueConstants.CheckCombo1Transition)
                    {
                        transition.ToState = EvaDialogueConstants.EndDialogueName;
                        var endDialogueState = fsmInstance.GetState(EvaDialogueConstants.EndDialogueName);
                        if (endDialogueState != null)
                        {
                            transition.ToFsmState = endDialogueState;
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityStartPatch), nameof(RedirectSetPreDlgTransition), ex);
            }
        }

        private static void RedirectUpgradeSlot1PreDlgTransition(Fsm fsmInstance)
        {
            try
            {
                var upgradeSlot1State = fsmInstance.GetState(EvaDialogueConstants.UpgradeSlot1PreDlgName);
                if (upgradeSlot1State == null)
                    return;

                var transitions = upgradeSlot1State.Transitions;
                if (transitions == null || transitions.Length == 0)
                    return;

                foreach (var transition in transitions)
                {
                    if (transition.ToState == EvaDialogueConstants.UpgradeSequence2Transition)
                    {
                        transition.ToState = EvaDialogueConstants.EndDialogueName;
                        var endDialogueState = fsmInstance.GetState(EvaDialogueConstants.EndDialogueName);
                        if (endDialogueState != null)
                        {
                            transition.ToFsmState = endDialogueState;
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityStartPatch), nameof(RedirectUpgradeSlot1PreDlgTransition), ex);
            }
        }
    }
}
