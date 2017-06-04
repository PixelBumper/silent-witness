using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private Song[] _playList;
    [SerializeField] private int _songCurrentlyPlaying;

    public void StartPlayingSong()
    {
        if (_playList == null || _playList.Length == 0)
        {
            Debug.LogWarning("No song is saved in the radio", gameObject);
            return;
        }

        var currentListOfObjectsToPlay = _playList[_songCurrentlyPlaying % _playList.Length];
        for (var i = 0; i < currentListOfObjectsToPlay.transform.childCount; i++)
        {
            var child = currentListOfObjectsToPlay.transform.GetChild(i);
            var persistentCharacterMusicController = child.GetComponent<PersistentCharacterMusicController>();
            if (persistentCharacterMusicController)
            {
                // Enable! 
            }
        }
        
    }
}