using Android.App;
using Android.Content;
using Android.Support.CustomTabs;
using Plugin.CurrentActivity;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.Share
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class ShareImplementation : IShare
    {
        /// <summary>
        /// For linker
        /// </summary>
        /// <returns></returns>
        public static async Task Init()
        {
            var test = DateTime.UtcNow;
        }

        /// <summary>
        /// Open a browser to a specific url
        /// </summary>
        /// <param name="url">Url to open</param>
        /// <param name="options">Platform specific options</param>
        /// <returns>awaitable Task</returns>
        public async Task OpenBrowser(string url, BrowserOptions options = null)
        {
            try
            {
                if (options == null)
                    options = new BrowserOptions();

                if (CrossCurrentActivity.Current.Activity == null)
                {
                    var intent = new Intent(Intent.ActionView);
                    intent.SetData(Android.Net.Uri.Parse(url));

                    intent.SetFlags(ActivityFlags.ClearTop);
                    intent.SetFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                }
                else
                {
                    var tabsBuilder = new CustomTabsIntent.Builder();
                    tabsBuilder.SetShowTitle(options?.ChromeShowTitle ?? false);
                    var toolbarColor = options?.ChromeToolbarColor;
                    if (toolbarColor != null)
                    {
                        tabsBuilder.SetToolbarColor(Android.Graphics.Color.Argb(toolbarColor.A,
                            toolbarColor.R,
                            toolbarColor.G,
                            toolbarColor.B));
                    }

                    var intent = tabsBuilder.Build();
                    intent.LaunchUrl(CrossCurrentActivity.Current.Activity, Android.Net.Uri.Parse(url));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to open browser: " + ex.Message);
            }
        }

        /// <summary>
        /// Simply share text with compatible services
        /// </summary>
        /// <param name="text">Text to share</param>
        /// <param name="title">Title of the share popup on Android and Windows, email subject if sharing with mail apps</param>
        /// <returns>awaitable Task</returns>
        public async Task Share(string text, string title = null)
        {
            ShareInternal(title, text, null);
        }

        /// <summary>
        /// Share a link url with compatible services
        /// </summary>
        /// <param name="url">Link to share</param>
        /// <param name="message">Message to include with the link</param>
        /// <param name="title">Title of the share popup on Android and Windows, email subject if sharing with mail apps</param>
        /// <returns>awaitable Task</returns>
        public async Task ShareLink(string url, string message = null, string title = null)
        {
            ShareInternal(title, message, url);
        }

        /// <summary>
        /// Share data with compatible services
        /// </summary>
        /// <param name="title">Title to share</param>
        /// <param name="message">Message to share</param>
        /// <param name="url">Link to share</param>
        void ShareInternal(string title, string message, string url)
        {
            var items = new List<string>();
            if (message != null)
                items.Add(message);
            if (url != null)
                items.Add(url);

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, string.Join(Environment.NewLine, items));
            if (title != null)
                intent.PutExtra(Intent.ExtraSubject, title);

            var chooserIntent = Intent.CreateChooser(intent, title);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        }

        /// <summary>
        /// Sets text on the clipboard
        /// </summary>
        /// <param name="text">Text to set</param>
        /// <param name="label">Label to display (not required, Android only)</param>
        /// <returns></returns>
        public Task<bool> SetClipboardText(string text, string label = null)
        {
            try
            {
                var sdk = (int)Android.OS.Build.VERSION.SdkInt;
                if (sdk < (int)Android.OS.BuildVersionCodes.Honeycomb)
                {
                    var clipboard = (Android.Text.ClipboardManager)Application.Context.GetSystemService(Context.ClipboardService);
                    clipboard.Text = text;
                }
                else
                {
                    var clipboard = (ClipboardManager)Application.Context.GetSystemService(Context.ClipboardService);
                    clipboard.PrimaryClip = ClipData.NewPlainText(label ?? string.Empty, text);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to copy to clipboard: " + ex);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// Gets if cliboard is supported
        /// </summary>
        public bool SupportsClipboard => true;

    }
}