namespace AtomicOrientedDesign.Shooter
{
    public sealed class PluginsStartable : IAppStartable
    {
        void IAppStartable.Start()
        {
            AppsFlyer.startSDK();
            FB.Init(null, null);
        }
    }
}