using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using GameNetcodeStuff;


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
            Logger.LogError($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            _harmony.PatchAll(typeof(TermSpeak));
        }

        [HarmonyPatch(nameof(Terminal.BeginUsingTerminal))]
        [HarmonyPrefix]
        public static void OpenTerminal()
        {
            _log.LogInfo("Terminal opened!");
        }   
    }   
}
