﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using NAudio.Wave;

// ReSharper disable once CheckNamespace
namespace Colladeo.FlakeNAudioAdapter
{
    public class FlakeFileReader : WaveStream
    {
        public FlakeFileReader(string path)
        {
            _flakeFileReader = new FlakeReader(path, null);
            _streamInfo = _flakeFileReader.PCM;
            _waveFormat =  new WaveFormat(_streamInfo.SampleRate, _streamInfo.BitsPerSample, _streamInfo.ChannelCount);

            var len = 65546 * _streamInfo.BitsPerSample / 8 * _streamInfo.ChannelCount;
            _audioBuffer = new AudioBuffer(_streamInfo, len);
            _decompressBuffer = new byte[len];
        }

        private readonly FlakeReader _flakeFileReader;
        private readonly AudioBuffer _audioBuffer;
        private readonly WaveFormat _waveFormat;
        private readonly AudioPCMConfig _streamInfo;
        private readonly object _repositionLock = new object();

        private readonly byte[] _decompressBuffer;
        private int _decompressLeftovers;
        private long _decompressBufferOffset;

        #region WaveStream members

        public override long Length => _flakeFileReader.Length * _waveFormat.BlockAlign;

        public override long Position
        {
            get { return _flakeFileReader.Position * _waveFormat.BlockAlign; }

            set
            {
                lock (_repositionLock)
                {
                    _flakeFileReader.Position = value /_waveFormat.BlockAlign;
                    _decompressLeftovers = 0;
                }
            }
        }

        public override WaveFormat WaveFormat => _waveFormat;

        /// <summary>
        /// Read from this FLAC stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null" />.</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            int bytesRead = 0;
            lock (_repositionLock)
            {
                while (bytesRead < count)
                {
                    if (_decompressLeftovers > 0)
                    {
                        int toCopy = Math.Min(_decompressLeftovers, count - bytesRead);
                        Array.Copy(_decompressBuffer, _decompressBufferOffset, buffer, offset, toCopy);
                        _decompressLeftovers -= toCopy;
                        if (_decompressLeftovers == 0)
                        {
                            _decompressBufferOffset = 0;
                        }
                        else
                        {
                            _decompressBufferOffset += toCopy;
                        }
                        bytesRead += toCopy;
                        offset += toCopy;
                    }
                    if (bytesRead == count)
                    {
                        break;
                    }
                    _decompressBufferOffset = 0;

                    // at this point our buffer will be empty
                    int br = _flakeFileReader.Read(_audioBuffer, count);
                    if (br > 0)
                    {
                        _decompressLeftovers = _audioBuffer.ByteLength;
                        Array.Copy(_audioBuffer.Bytes, 0, _decompressBuffer, 0, _decompressLeftovers);
                    }
                }
            }
            
            return bytesRead;
        }
    }
#endregion

}