using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Assets")]
public class AudioAssets : ScriptableObject
{
    public AudioAsset[] asset;
}

[System.Serializable]
public struct AudioAsset
{
    public string name;
    public AudioClip[] audioClips;
}
