namespace MarcelJoachimKloubert.WpfAuctionDesigner.eBay
{
    internal static class eBayHelper
    {
#if DEBUG
        internal const string APP_ID = "MarcelJo-3f77-4f43-9cab-1b7660fdd408";
        internal const string DEV_ID = "c73ab931-2e3d-46c4-8849-f30807c16276";
        internal const string CERT_ID = "7bb564ea-7fb9-495a-a401-ba97d98af188";
#else
        internal const string APP_ID = "MarcelJo-39ae-4e21-9727-0dc192dc0527";
        internal const string DEV_ID = "c73ab931-2e3d-46c4-8849-f30807c16276";
        internal const string CERT_ID = "4fa7555e-4923-4da8-81dd-15175de3877b";
#endif
    }
}
