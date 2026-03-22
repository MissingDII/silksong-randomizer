namespace Silkipelago.Constants.FSM
{
    public static class EvaDialogueConstants
    {
        // FSM Names
        public const string DialogueFsmName = "Dialogue";
        public const string OwnerName = "Crest Upgrade Shrine";

        // FSM State Names
        public const string CrestUpgrade1dlg = "Crest Upg 1 Dlg";
        public const string CheckUpgrade = "Get Upgrade Points";
        public const string CheckUpgradeFinal = "Check Final Upgrade";
        public const string BindPrepareName = "Bind Prepare";
        public const string SetPreDlgName = "Set Pre Dlg";
        public const string UpgradeSlot1PreDlgName = "Upgrade Slot1 Pre Dlg";
        public const string EndDialogueName = "End Dialogue";

        // FSM Transitions
        public const string BindReadyTransition = "Bind Ready";
        public const string CheckCombo1Transition = "Check Combo 1";
        public const string UpgradeSequence2Transition = "Upgrade Sequence 2";

        // Dialogue States
        public const string MeetDlgState = "Meet Dlg";
        public const string RepeatDlgState = "Repeat Dlg";
        public const string GetUpgradePointsState = "Get Upgrade Points";
    }
}
