using HarmonyLib;
using HutongGames.PlayMaker;
using Silkipelago.Constants.FSM;
using System;

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
                RedirectCheckUpgrade(fsmInstance);
                RedirectedFsms.Add(fsmId);
            }
        }

        private static void RedirectCheckUpgrade(Fsm fsmInstance)
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
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityStartPatch), nameof(RedirectCheckUpgrade), ex);
            }
        }
    }
}
