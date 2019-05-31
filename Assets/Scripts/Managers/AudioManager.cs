using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private Sound[] _sounds;

    protected override void Awake()
    {
        base.Awake();

        gameObject.SetActive(PlayerPrefs.GetInt("EnableSound") == 1);

        foreach (var sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    public void Play(string sound)
    {
        var s = _sounds.FirstOrDefault(x => x.name == sound);

        if (s == null)
        {
            Debug.LogError($"Sound: {name} not found.");
            return;
        }

        s.source.volume = s.volume;
        s.source.Play();
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public bool loop = false;

    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;

    [Range(0f, 1f)]
    public float volume = .75f;

    #region Sounds
    public const string ANIMAL_HIT = "animalHit";
    public const string CHAINGUN = "chainGun";
    public const string GUNSHOT = "gunShot";
    #endregion
}
