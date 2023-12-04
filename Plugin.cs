using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace TermMacros
{
    [BepInPlugin("me.kdcf.termspeak", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    [HarmonyPatch(typeof(Terminal))]
    public class TermSpeak : BaseUnityPlugin
    {
        private Harmony _harmony = new Harmony("me.kdcf.termspeak");
        private static ManualLogSource _log = BepInEx.Logging.Logger.CreateLogSource("TermSpeak");
        
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin me.kdcf.termspeak is loaded!");
            _harmony.PatchAll(typeof(TermSpeak));
        }

        [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
        [HarmonyPostfix]
        public static void OpenTerminal(Terminal __instance)
        {
            SetWalkieMode(true);
        }

        [HarmonyPatch(nameof(Terminal.QuitTerminal))]
        [HarmonyPostfix]
        public static void CloseTerminal(Terminal __instance)
        {
            SetWalkieMode(false);
        }

        public static void SetWalkieMode(bool enabled)
        {
            _log.LogInfo("Terminal opened! Trying to find walkie talkie...");
            var player = GameNetworkManager.Instance.localPlayerController;
            _log.LogInfo($"There are {player.ItemSlots.Length} item slots to check.");
            GrabbableObject walkieTalkie = null;
            for (int i = 0; i < player.ItemSlots.Length; i++)
            {
                // isBeingUsed on the WalkieTalkie represents if it's powered or not.
                // It doesn't represent if it's being held or not, which is what we want.
                // We don't care if it's powered or not when trying to disable it, just in case
                // it's somehow unpowered while the user is holding it.
                if (player.ItemSlots[i] is WalkieTalkie && (!enabled || player.ItemSlots[i].isBeingUsed))
                {
                    walkieTalkie = player.ItemSlots[i];
                    break;
                }
            }
            if (walkieTalkie == null)
            {
                _log.LogInfo("No walkie talkie found!");
            }
            else
            {
                _log.LogInfo("Found walkie talkie! Trying to set its mode...");
                walkieTalkie.UseItemOnClient(enabled); // Start the walkie talkie
            }
        }
    }   
}
