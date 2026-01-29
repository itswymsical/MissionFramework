namespace MissionFramework.Core.Interfaces
{
    interface IOrderedLoadable
    {
        void Load();
        void Unload();
        float Priority { get; }
    }
}
