using Core;
using DC.MessageService;

namespace Utils
{
    /// <summary>
    /// Locator class that contains implementations for service classes to maintain cleaner code in other classes by
    /// keeping find checks and instantiation in one location.
    /// </summary>
    internal abstract class Locator : BaseLocator
    {
        public static ITinyMessengerHub EBus
        {
            get
            {
                var hub = Find<ITinyMessengerHub>();

                if (hub != null) return hub;

                var instance = Add<ITinyMessengerHub>(new TinyMessengerHub());

                return instance;
            }
        }
        
        public static IPongApp App
        {
            get
            {
                var app = Find<IPongApp>();

                if (app != null) return app;

                var instance = Add<IPongApp>(new PongApp());

                return instance;
            }
        }
    }
}