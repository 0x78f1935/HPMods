using Il2CppSystem.Collections.Generic;
using System.Linq;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using EekCharacterEngine.Interaction;


namespace ItemRandomizer
{
    public class ItemRandomizer : MelonMod
    {
        private Scene scene = SceneManager.GetActiveScene();
        public static bool sceneInIntro = false;
        public static bool sceneInDisclaimer = false;
        public static bool sceneInMainMenu = false;
        public static bool sceneInLoading = false;
        public static bool sceneInGameMain = false;

        private System.Collections.Generic.List<System.String> blacklist = new System.Collections.Generic.List<System.String> {
            "bone", "door", "cabernet", "chair", "couch", "drawer", "phone", "particle", "terrarium", "fire", "spot", "seat", "button", "sofa", "dryer", "washer", "pouf", "bed", "toilet", "compubrah", "climbtoroof", "audio"
        };

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        { // Starts when scene loads
            InitializeGameObjects();
        }

        public void InitializeGameObjects()
        { // __init__ objects when scene loads
            scene = SceneManager.GetActiveScene();
            // MelonLogger.Msg(string.Concat(Enumerable.Repeat("=", scene.name.Length)));
            // MelonLogger.Msg("Scene loaded: " + scene.name);
            // MelonLogger.Msg(string.Concat(Enumerable.Repeat("=", scene.name.Length)));

            sceneInIntro = scene.name == "EekGamesIntro";
            sceneInDisclaimer = scene.name == "Disclaimer";
            sceneInMainMenu = scene.name == "MainMenu";
            sceneInLoading = scene.name == "LoadingScreen";
            sceneInGameMain = scene.name == "GameMain";

            if (sceneInIntro)
            {
                MelonLogger.Msg("Initialized");
            }

            if (sceneInGameMain)
            {
                RandomizeLogic();
            }
        }

        private static void PrintHierarchy()
        {
            MelonLogger.Msg("Print object hierarchy");
            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                MelonLogger.Msg("Root object");
                PrintChildren(obj.transform, "");
            }
        }

        private static void PrintChildren(Transform t, string indent)
        {
            int child_count = t.childCount;
            MelonLogger.Msg($"{indent}'<{t.gameObject.GetType().ToString().Replace("UnityEngine.", "")}>{t.gameObject.name}' ({child_count} children) -> Layer [{t.gameObject.layer}] {LayerMask.LayerToName(t.gameObject.layer)}");

            string more_indent;
            if (indent.Length == 0)
            {
                more_indent = indent + "L___";
            }
            else
            {
                more_indent = indent + "____";
            }
            for (int i = 0; i < child_count; ++i)
            {
                var child = t.GetChild(i);
                PrintChildren(child, more_indent);
            }
        }

        public override void OnUpdate()
        { // Append onUpdate method

            if (sceneInIntro)
            {
                InIntro();
            }
            else if (sceneInDisclaimer)
            {
                InDisclaimer();
            }
            else if (sceneInMainMenu)
            {
                InMainMenu();
            }
            else if (sceneInLoading)
            {
                InLoading();
            }
            else if (sceneInGameMain)
            {
                InGame();            
            }
        }

        private void InIntro()
        {
            //
        }

        private void InDisclaimer()
        {
            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                Text target = obj.gameObject.GetComponentInChildren<Text>();
                if (target)
                {
                    target.text = Shuffle(target.text);
                }
            }
        }
        private string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            System.Random rng = new System.Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        private void InMainMenu()
        {
            //
        }

        private void InLoading()
        {
            //
        }

        private void InGame()
        {
            if (Keyboard.current[Key.Numpad9].wasPressedThisFrame && Keyboard.current[Key.LeftAlt].isPressed && Keyboard.current[Key.LeftCtrl].isPressed)
            {
                RandomizeLogic();
            }
        }

        private List<GameObject> ExtractChildren(Transform obj)
        { // Method which extract children from parents
            List<GameObject> interactiveItems = new List<GameObject>();

            for (int i = 0; i < obj.childCount; ++i)
            {
                var child = obj.GetChild(i);
                if (child.gameObject.layer == 11 && LayerMask.LayerToName(child.gameObject.layer).ToLower() == "interactiveitems")
                {
                    interactiveItems.Add(child.gameObject);
                }
                if (child.transform.childCount > 0)
                {
                    foreach (var item in ExtractChildren(child.transform))
                    {
                        if (item.gameObject.layer == 11 && LayerMask.LayerToName(item.gameObject.layer).ToLower() == "interactiveitems")
                        {
                            interactiveItems.Add(item);
                        }
                    }
                }
            }

            return interactiveItems;
        }

        private List<GameObject> InteractiveItems()
        { // Fetches each interactive object and checks if it has children, if so we extract those children
            List<GameObject> interactiveItems = new List<GameObject>();

            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (obj.gameObject.layer == 11 && LayerMask.LayerToName(obj.gameObject.layer).ToLower() == "interactiveitems")
                {
                    interactiveItems.Add(obj.gameObject);
                }
                if (obj.transform.childCount > 0)
                {
                    foreach (var moreObj in ExtractChildren(obj.transform))
                    {
                        interactiveItems.Add(moreObj);
                    }
                }
            }
            return interactiveItems;
        }

        private List<GameObject> ValidInteractiveItems(List<GameObject> interactiveItems)
        { // Removes items which are defined in the blacklist variable
            List<GameObject> validInteractiveItems = new List<GameObject>();

            foreach (var obj in interactiveItems)
            {
                bool isValid = true;
                foreach (string sensor in blacklist)
                {
                    if (obj.gameObject.name.ToString().ToLower().Contains(sensor.ToLower()))
                    {
                        isValid = false;
                    }
                }

                if (!obj.gameObject.GetComponent<InteractiveItem>())
                {
                    isValid = false;
                }

                if (isValid)
                {
                    validInteractiveItems.Add(obj);
                }

            }

            return validInteractiveItems;
        }

        private void RandomizeLogic()
        {
            List<GameObject> interactiveItems = ValidInteractiveItems(InteractiveItems());
            List<Transform> transformLocations = new List<Transform>();

            foreach (var obj in interactiveItems)
            {
                transformLocations.Add(obj.transform);
            }
            var random = new System.Random();
            int patchCount = 0;
            MelonLogger.Msg($"Interactive Items: {interactiveItems.Count} | Locations stripped: {transformLocations.Count}");
            foreach (GameObject obj in interactiveItems)
            {
                int index = random.Next(transformLocations.Count);
                MelonLogger.Msg(
                    $"\n** PATCHING ==> {obj.name}\n" +
                    $"[POS] \n\t ({obj.transform.position.x}, {obj.transform.position.y}, {obj.transform.position.z}), : ({transformLocations[index].position.x}, {transformLocations[index].position.y}, {transformLocations[index].position.z})\n" +
                    $"[ROT] \n\t ({obj.transform.rotation.x}, {obj.transform.rotation.y}, {obj.transform.rotation.z}) : ({transformLocations[index].rotation.x}, {transformLocations[index].rotation.y}, {transformLocations[index].rotation.z})\n"
                );
                obj.transform.position = transformLocations[index].position;
                obj.transform.rotation = transformLocations[index].rotation;
                transformLocations.RemoveAt(index);
                patchCount += 1;
            }
            MelonLogger.Msg($"** PATCH COMPLETE -> Affected {patchCount} items");
        }
    }
}
