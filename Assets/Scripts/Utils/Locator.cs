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
                if (DoesServiceExist(typeof(ITinyMessengerHub)))
                {
                    var hub = Find<ITinyMessengerHub>();

                    return hub;
                }
                else
                {
                    var instance = Add<ITinyMessengerHub>(new TinyMessengerHub());

                    return instance;
                }
            }
        }
        
        public static IPongApp App
        {
            get
            {
                if (DoesServiceExist(typeof(IPongApp)))
                {
                    var hub = Find<IPongApp>();

                    return hub;
                }
                else
                {
                    var instance = Add<IPongApp>(new PongApp());

                    return instance;
                }
            }
        }
    }
}