﻿using UnityEngine;
using UnityEngine.UI;

namespace DynamicCSharp.Demo
{
    /// <summary>
    /// Responsible for the tank demo gameplay.
    /// </summary>
    public sealed class TankManager : MonoBehaviour
    {
        // Private
        private ScriptDomain domain = null;
        private Vector2 startPosition;
        private Quaternion startRotation;
        
        private const string newTemplate = "BlankTemplate";
        private const string exampleTemplate = "ExampleTemplate";

        // Public
        /// <summary>
        /// The shell prefab that tanks are able to shoot.
        /// </summary>
        public string nextStage;
        public GameObject bulletObject;
        /// <summary>
        /// The tank object that can be controlled via code.
        /// </summary>
        public GameObject tankObject;

        public Sprite playBuSprite;
        public Image buttonPlayimg;


       


        // Methods
        /// <summary>
        /// Called by Unity.
        /// </summary>
        public void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
            //if(ScriptDomain.Active== null){

        //    Debug.Log("333333333333");
        //}

        // Create our script domain
        domain = ScriptDomain.CreateDomain("ScriptDomain", true);

            // Find start positions
            startPosition = tankObject.transform.position;
            startRotation = tankObject.transform.rotation;

            // Add listener for new button
            CodeUI.onNewClicked += (CodeUI ui) =>
            {
                Debug.Log("ddddddddd");


                // Load new file
                ui.codeEditor.text = Resources.Load<TextAsset>(newTemplate).text;
            };

            // Add listener for example button
            CodeUI.onLoadClicked += (CodeUI ui) =>
            {
                // Load example file
                ui.codeEditor.text = Resources.Load<TextAsset>(exampleTemplate).text;
            };

            CodeUI.onCompileClicked += (CodeUI ui) =>
            {
                // Try to run the script
                RunTankScript(ui.codeEditor.text);
            };

        }

        /// <summary>
        /// Resets the demo game and runs the tank with the specified C# code controlling it.
        /// </summary>
        /// <param name="source">The C# sorce code to run</param>
        public void RunTankScript(string source)
        {
            if (tankObject != null)
            {

                // Strip the old controller script
                TankController old = tankObject.GetComponent<TankController>();

                if (old != null)
                    Destroy(old);

            // Reposition the tank at its start position
                RespawnTank();

            }
            // Compile the script
            ScriptType type = domain.CompileAndLoadScriptSource(source);

            if (type == null)
            {
                Debug.Log("1111"+domain.GetErrorLineValue());
                Debug.LogError("Compile failed");
                return;
            }

            // Make sure the type inherits from 'TankController'
            if (type.IsSubtypeOf<TankController>() == true && tankObject != null)
            {

                // Attach the component to the object
                ScriptProxy proxy = type.CreateInstance(tankObject);

                // Check for error
                if(proxy == null)
                {
                    // Display error
                    Debug.LogError(string.Format("Failed to create an instance of '{0}'", type.RawType));
                    return;
                }
                // Assign the bullet prefab to the 'TankController' instance
                proxy.Fields["playBuSprite"] = playBuSprite;
                proxy.Fields["buttonPlayimg"] = buttonPlayimg;
                proxy.Fields["bulletObject"] = bulletObject;
                proxy.Fields["nextStage"] = nextStage;
                // Call the run tank method
                proxy.Call("RunTank");
            }
            else
            {
                //if(tankObject != null){
                //    Debug.LogError("The script must inherit from 'TankController'");
                //}

            }
        }

        /// <summary>
        /// Resets the tank at its starting location.
        /// </summary>
        public void RespawnTank()
        {
            tankObject.transform.position = startPosition;
            tankObject.transform.rotation = startRotation;

        }
    }
}
