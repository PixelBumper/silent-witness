using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SongSaver : MonoBehaviour
{
    [MenuItem("Tools/Save current song %i")]
    public static void SaveCurrentSong()
    {
        var allCharacterMusicControllers = FindObjectsOfType<CharacterMusicController>();

        var enabledCharacterMusicControllers = new List<CharacterMusicController>();
        for (var i = 0; i < allCharacterMusicControllers.Length; i++)
        {
            var characterMusicController = allCharacterMusicControllers[i];
            if (characterMusicController.enabled)
            {
                enabledCharacterMusicControllers.Add(characterMusicController);
            }
        }

        if (enabledCharacterMusicControllers.Count > 0)
        {
            var parentClass = new GameObject("PlayingSong", typeof(Song));
            var parent = Instantiate(parentClass, Vector3.zero, Quaternion.identity);
            var playingInstrument = new GameObject("Instrument", typeof(PersistentCharacterMusicController));

            for (var i = 0; i < enabledCharacterMusicControllers.Count; i++)
            {
                var enabledCharacterMusicController = enabledCharacterMusicControllers[i];
                var child = Instantiate(playingInstrument, enabledCharacterMusicController.transform.position, enabledCharacterMusicController.transform.localRotation, parent.transform);
                child.transform.localScale = enabledCharacterMusicController.transform.localScale;
                var persistentCharacterMusicController = child.GetComponent<PersistentCharacterMusicController>();
                
                persistentCharacterMusicController.CopyFromController(enabledCharacterMusicController);
                persistentCharacterMusicController.enabled = false;
            }
            DestroyImmediate(parentClass);
            DestroyImmediate(playingInstrument);
        }
        else
        {
            Debug.Log("No enabled music controllers were found");
        }
    }
}