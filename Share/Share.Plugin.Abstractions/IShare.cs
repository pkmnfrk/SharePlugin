﻿using System;
using System.Threading.Tasks;

namespace Plugin.Share.Abstractions
{
    
    /// <summary>
    /// Interface for Share
    /// </summary>
    public interface IShare
    {
        /// <summary>
        /// Simply share text with compatible services
        /// </summary>
        /// <param name="text">Text to share</param>
        /// <param name="title">Title of the share popup on Android and Windows, email subject if sharing with mail apps</param>
        /// <returns>awaitable Task</returns>
        Task Share(string text, string title = null);

        /// <summary>
        /// Open a browser to a specific url
        /// </summary>
        /// <param name="url">Url to open</param>
        /// <param name="options">Platform specific options</param>
        /// <returns>awaitable Task</returns>
        Task OpenBrowser(string url, BrowserOptions options = null);

        /// <summary>
        /// Share a link url with compatible services
        /// </summary>
        /// <param name="url">Link to share</param>
        /// <param name="message">Message to include with the link</param>
        /// <param name="title">Title of the share popup on Android and Windows, email subject if sharing with mail apps</param>
        /// <returns>awaitable Task</returns>
        Task ShareLink(string url, string message = null, string title = null);

        /// <summary>
        /// Share a link url with compatible services
        /// </summary>
        /// <param name="url">Link to share</param>
        /// <param name="message">Message to include with the link</param>
        /// <param name="title">Title of the share popup on Android and Windows, email subject if sharing with mail apps</param>
        /// <returns>awaitable Task</returns>
        Task ShareLink(byte[] data, string type, string url = null, string message = null, string title = null);

        /// <summary>
        /// Sets text on the clipboard
        /// </summary>
        /// <param name="text">Text to set</param>
        /// <param name="label">Label to dislay (no required, Android only)</param>
        /// <returns></returns>
        Task<bool> SetClipboardText(string text, string label = null);

        /// <summary>
        /// Gets if clipboard is supported
        /// </summary>
        bool SupportsClipboard { get; }
    }
}
