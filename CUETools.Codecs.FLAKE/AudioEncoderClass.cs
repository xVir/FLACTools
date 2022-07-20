﻿using System;

namespace CUETools.Codecs.FLAKE
{
    /// <summary>
    ///    This class provides an attribute for marking
    ///    classes that provide <see cref="IAudioDest" />.
    /// </summary>
    /// <remarks>
    ///    When plugins with classes that provide <see cref="IAudioDest" /> are
    ///    registered, their <see cref="AudioEncoderClassAttribute" /> attributes are read.
    /// </remarks>
    /// <example>
    ///    <code lang="C#">using CUETools.Codecs;
    ///
    ///[AudioEncoderClass("libFLAC", "flac", true, "0 1 2 3 4 5 6 7 8", "5", 1)]
    ///public class MyEncoder : IAudioDest {
    ///	...
    ///}</code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AudioEncoderClassAttribute : Attribute
    {
        private string _encoderName, _extension;
        private bool _lossless;
        private int _priority;
        private Type _settings;

        public string EncoderName
        {
            get { return _encoderName; }
        }

        public string Extension
        {
            get { return _extension; }
        }

        public bool Lossless
        {
            get { return _lossless; }
        }

        public int Priority
        {
            get { return _priority; }
        }

        public Type Settings
        {
            get { return _settings; }
        }

        public AudioEncoderClassAttribute(string encoderName, string extension, bool lossless, int priority, Type settings)
        {
            _encoderName = encoderName;
            _extension = extension;
            _lossless = lossless;
            _priority = priority;
            _settings = settings;
        }
    }
}
