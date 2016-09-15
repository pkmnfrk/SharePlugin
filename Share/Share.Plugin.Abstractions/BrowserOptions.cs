namespace Plugin.Share.Abstractions
{
    /// <summary>
    /// Platform specific Browser Options
    /// </summary>
    public class BrowserOptions
    {
        /// <summary>
        /// iOS: Gets or Set to use the SFSafariWebViewController on iOS 9+ (recommended)
        /// Default is true
        /// </summary>
        public bool UseSafariWebViewController { get; set; } = true;
        /// <summary>
        /// iOS: When the user taps the done button, this callback is invoked. If it returns true, the webview is dismissed. If it returns false, it does not
        /// Default is null
        /// </summary>
        public System.Action OnSafariWebViewControllerDone { get; set; } = null;
        /// <summary>
        /// iOS: Gets or sets to use reader mode (good for markdown files)
        /// Default is false
        /// </summary>
        [System.Obsolete("Use UseSafariReaderMode instead", true)]
        public bool UseSafairReaderMode { get { return UseSafariReaderMode; } set { UseSafariReaderMode = value; } }
        /// <summary>
        /// iOS: Gets or sets to use reader mode (good for markdown files)
        /// Default is false
        /// </summary>
        public bool UseSafariReaderMode { get; set; } = false;
        /// <summary>
        /// Android: Gets or sets to display title as well as url in chrome custom tabs
        /// Default is true
        /// </summary>
        public bool ChromeShowTitle { get; set; } = true;
        /// <summary>
        /// Android: Gets or sets the toolbar color of the chrome custom tabs
        /// If null (default) will be default chrome color
        /// </summary>
        public ShareColor ChromeToolbarColor { get; set; } = null;
    }
}
