using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    // obican singleton koji se resetuje nakon zatvaranja aplikacije, resetovanja scene itd.
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) // ukoliko je audio menadzer vec postavljen obrisi novu instancu
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
    }
}

public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject); // sprecava unistavanje ako se reloaduje scena, promeni scena, etc, da ostane muzika iako smo promenili nivo
    }
}