using System.Collections;
using System.Collections.Generic;
using CSCore.SoundIn;
using CSCore.DSP;
using CSCore;
using System;
using CSCore.SoundOut;

public class CoreSpectrum {
    private static readonly int CAPTURE_DELAY = 50;
    private static readonly int STEREO = 2;

    private static float[] hamming;

    private WasapiLoopbackCapture capture;
    private FftProvider fftProvider; // Still use fft providers? This may be the bug for the fft "pausing"
    private SilenceGenerator silence;
    private float[] spectrumData;
    private bool newDataRead;

    public CoreSpectrum(int size=1024)
    {
        newDataRead = true;

        hamming = new float[size];
        for(int n = 0; n < hamming.Length; n++)
        {
            hamming[n] = FastFourierTransformation.HammingWindowF(n, size);
        }

        capture = new WasapiLoopbackCapture(CAPTURE_DELAY);
        capture.DataAvailable += (s, a) =>
        {
            if (newDataRead)
            {
                UpdateSpectrumData(a.Data);
            }
        };
        capture.Stopped += (c, e) =>
        {
            for(int i = 0; i < spectrumData.Length; i++)
            {
                spectrumData[i] = 0f;
            }
        };
        capture.Initialize();
        capture.Start();

        fftProvider = new FftProvider(STEREO, FftSize.Fft1024);

        spectrumData = new float[size];
    }

    public float[] GetSpectrumData()
    {
        return spectrumData;
    }

    public void DataRead()
    {
        newDataRead = true;
    }

    private void UpdateSpectrumData(byte[] data)
    {
        for (int i = 0; i < (int)fftProvider.FftSize; i++)
        {
            float l = BitConverter.ToSingle(data, i * 8) * hamming[i];
            float r = BitConverter.ToSingle(data, i * 8 + 4) * hamming[i];
            fftProvider.Add(l, r);
        }

        lock (spectrumData)
        {
            fftProvider.GetFftData(spectrumData);
        }

        newDataRead = false;
    }

}

public class SilenceGenerator : IWaveSource
{
    private readonly WaveFormat _waveFormat = new WaveFormat(44100, 16, 2);

    public int Read(byte[] buffer, int offset, int count)
    {
        Array.Clear(buffer, offset, count);
        return count;
    }

    public WaveFormat WaveFormat
    {
        get { return _waveFormat; }
    }

    public long Position
    {
        get { return -1; }
        set
        {
            throw new InvalidOperationException();
        }
    }

    public long Length
    {
        get { return -1; }
    }

    public bool CanSeek
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void Dispose()
    {
        //do nothing
    }
}