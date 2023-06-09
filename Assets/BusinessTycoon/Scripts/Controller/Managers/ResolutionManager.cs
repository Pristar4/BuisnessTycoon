#region Info

// -----------------------------------------------------------------------
// ResolutionManager.cs
// 
// Felix Jung 20.06.2023
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;

#endregion

namespace BT.Managers {
    public class ResolutionManager : MonoBehaviour {
        #region Serialized Fields

        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TextMeshProUGUI saveButtonText;
        [SerializeField] private Sprite resolutionSprite;

        #endregion

        private Resolution[] resolutions;

        #region Event Functions

        private void Start() {
            resolutions = Screen.resolutions;

            // Create a HashSet to store unique resolution options
            var uniqueOptions = new HashSet<string>();

            // Clear the dropdown options
            resolutionDropdown.ClearOptions();

            foreach (var resolution in resolutions) {
                string option = resolution.width + "x" + resolution.height;

                if (!uniqueOptions.Contains(option) && resolution is
                            { width: >= 800, height: >= 600 }) {
                    uniqueOptions.Add(option);
                    // Color : 93887A (RGB)
                    // Color : 153,142,127 (RGB) use it on the option added
                    resolutionDropdown.options.Add(
                            new TMP_Dropdown.OptionData(option, resolutionSprite));
                }
            }

            resolutionDropdown.value = GetCurrentResolutionIndex();
            resolutionDropdown.RefreshShownValue();
        }

        #endregion

        public void SaveResolution() {
            string selectedOption
                    = resolutionDropdown.options[resolutionDropdown.value].text;

            string[] resolutionValues = selectedOption.Split('x');
            int width = int.Parse(resolutionValues[0]);
            int height = int.Parse(resolutionValues[1]);

            Screen.SetResolution(width, height, true);

            saveButtonText.text = "Resolution Saved!";
        }

        private int GetCurrentResolutionIndex() {
            var currentResolution = Screen.currentResolution;
            for (int i = 0; i < resolutions.Length; i++)
                if (resolutions[i].width == currentResolution.width &&
                    resolutions[i].height == currentResolution.height)
                    return i;
            return 0;
        }
    }
}