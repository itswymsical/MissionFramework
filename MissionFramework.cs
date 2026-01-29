using MissionFramework.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MissionFramework;

/// <summary>
/// Main mod class for MissionFramework.
/// </summary>
public class MissionFramework : Mod
{/// <summary>
 ///     The name of this mod.
 /// </summary>
    public const string NAME = nameof(MissionFramework);

    /// <summary>
    ///     Directory for UI assets.
    /// </summary>
    public const string UI_ASSET_DIRECTORY = nameof(MissionFramework) + "/Assets/Textures/UI/";
    public const string TEXTURE_DIRECTORY = nameof(MissionFramework) + "/Assets/Textures/";

    /// <summary>
    ///     Directory for VFX textures.
    /// </summary>
    public const string VFX_DIRECTORY = nameof(MissionFramework) + "/Assets/Textures/VFX/";

    /// <summary>
    ///     Directory for sound effects.
    /// </summary>
    public const string SFX_DIRECTORY = nameof(MissionFramework) + "/Assets/Sounds/";

    /// <summary>
    ///     The prefix to use for the name of this mod.
    /// </summary>
    public const string NAME_PREFIX = NAME + ": ";

    public static MissionFramework Instance { get; set; }

    private List<IOrderedLoadable> loadCache;
    public MissionFramework() => Instance = this;

    public override void Load()
    {

        loadCache = [];

        foreach (Type type in Code.GetTypes())
        {
            if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(IOrderedLoadable)))
            {
                object instance = Activator.CreateInstance(type);
                loadCache.Add((IOrderedLoadable)instance);
            }

            loadCache.Sort((n, t) => n.Priority.CompareTo(t.Priority));
        }

        for (int k = 0; k < loadCache.Count; k++)
        {
            loadCache[k].Load();

        }

    }

    public override void Unload()
    {

        if (loadCache != null)
        {
            foreach (IOrderedLoadable loadable in loadCache)
            {
                loadable.Unload();
            }

            loadCache = null;
        }
        else
        {
            Logger.Warn("load cache was null, IOrderedLoadable's may not have been unloaded...");
        }

        if (!Main.dedServ)
        {
            Instance ??= null;
        }

    }
}