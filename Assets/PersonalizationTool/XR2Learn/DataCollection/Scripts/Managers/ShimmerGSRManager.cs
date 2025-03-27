/*********** Copyright � 2024 University of Applied Sciences of Southern Switzerland (SUPSI) ***********\
 
 Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 associated documentation files (the "Software"), to deal in the Software without restriction,
 including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
 subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all copies or substantial
 portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
 LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

\*******************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using XR2Learn.Common.UserConfig;
using XR2Learn.DataCollection.Shimmer;

namespace XR2Learn.DataCollection.Managers
{ 
    public partial class ShimmerGSRManager : MonoBehaviour
    {
        public enum ConnectionState 
        {
            CONNECTED, CONNECTING, DISCONNECTED, STREAMING, INACTIVE
        }

        #region Serialized fields

        [Serializable]
        private struct ShimmerDevice
        {
            [Tooltip("Enable/disable device")]
            [SerializeField] public bool Enabled;
            [Tooltip("User defined device name")]
            [SerializeField] public string DeviceName;
        }
        [SerializeField] private ShimmerDevice _device = new ShimmerDevice();

        [Serializable]
        private struct ShimmerDeviceConfig 
        {
            [Tooltip("Number of Heart Beats required to calculate an average")]
            [SerializeField] public int HeartBeatsToAverage;
            [Tooltip("PPG algorithm training period in [s]")]
            [SerializeField] public int TrainingPeriodPPG;
            [Tooltip("PPG low-pass filter cutoff in [Hz]")]
            [SerializeField] public double LowPassFilterCutoff;
            [Tooltip("PPG high-pass filter cutoff in [Hz]")]
            [SerializeField] public double HighPassFilterCutoff;
            [Range(0.01f, 1000.0f)]
            [Tooltip("Shimmer device internal sampling rate in [Hz]")]
            [SerializeField] public double SamplingRate;
            [Range(1, 100)]
            [Tooltip("Number of samples to buffer before writing to file")]
            [SerializeField] public int SamplesBufferSize;
        }
        [SerializeField] private ShimmerDeviceConfig _config = new ShimmerDeviceConfig();

        [Serializable]
        private struct ShimmerDeviceSensors
        {
            [Tooltip("Flag to enable/disable the Accelerator sensor")]
            [SerializeField] public bool EnableAccelerator;
            [Tooltip("Flag to enable/disable the GSR sensor")]
            [SerializeField] public bool EnableGSR;
            [Tooltip("Flag to enable/disable the PPG sensor")]
            [SerializeField] public bool EnablePPG;
        }
        [SerializeField] private ShimmerDeviceSensors _sensors = new ShimmerDeviceSensors();

        #endregion

        private ShimmerGSR Shimmer;
        private System.Diagnostics.Stopwatch _stopwatch;
        private List<ShimmerGSRData> _dataBuffer;
        public ConnectionState State { get; private set; }

        private static ShimmerGSRManager _instance;
        public static ShimmerGSRManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<ShimmerGSRManager>();
                return _instance;
            }
        }


        private ShimmerGSRManager() { }

        private void Reset()
        {
            _device.Enabled = true;
            _device.DeviceName = "Shimmer3";

            _config.HeartBeatsToAverage = ShimmerGSR.DefaultNumberOfHeartBeatsToAverage;
            _config.TrainingPeriodPPG = ShimmerGSR.DefaultTrainingPeriodPPG;
            _config.LowPassFilterCutoff = ShimmerGSR.DefaultLowPassFilterCutoff;
            _config.HighPassFilterCutoff = ShimmerGSR.DefaultHighPassFilterCutoff;
            _config.SamplingRate = ShimmerGSR.DefaultSamplingRate;
            _config.SamplesBufferSize = 50;

            _sensors.EnableAccelerator = ShimmerGSR.DefaultEnableAccelerator;
            _sensors.EnableGSR = ShimmerGSR.DefaultEnableGSR;
            _sensors.EnablePPG = ShimmerGSR.DefaultEnablePPG;
        }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_Enabled, out _device.Enabled, true);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_DeviceName, out _device.DeviceName, "Shimmer3");

            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_HeartbeatsToAverage, out _config.HeartBeatsToAverage, ShimmerGSR.DefaultNumberOfHeartBeatsToAverage);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_TrainingPeriodPPG, out _config.TrainingPeriodPPG, ShimmerGSR.DefaultTrainingPeriodPPG);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_SamplingRate, out _config.SamplingRate, ShimmerGSR.DefaultSamplingRate);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_SamplingBufferSize, out _config.SamplesBufferSize, 50);

            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_EnableAccelerator, out _sensors.EnableAccelerator, ShimmerGSR.DefaultEnableAccelerator);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_EnableGSR, out _sensors.EnableGSR, ShimmerGSR.DefaultEnableGSR);
            UserSettingsLoader.Load(UserSettingsLoader.userSettings.Shimmer_EnablePPG, out _sensors.EnablePPG, ShimmerGSR.DefaultEnablePPG);

            Shimmer = new ShimmerGSR();
            State = _device.Enabled ? ConnectionState.DISCONNECTED : ConnectionState.INACTIVE;

            _dataBuffer = new List<ShimmerGSRData>();
            _stopwatch = new System.Diagnostics.Stopwatch();

            if (!_device.Enabled)
            {
                gameObject.SetActive(false);
                return;
            }

            new System.Threading.Thread(AttemptConnection).Start();
        }

        private void FixedUpdate()
        {
            if (!_device.Enabled) return;

            if (!Shimmer.IsConnected()) return;

            if (State == ConnectionState.STREAMING && _stopwatch.ElapsedMilliseconds > (1000 / Shimmer.SamplingRate))
            { 
                HandleData();
                _stopwatch.Restart();
            }
        }

        public void Configure(string serialPort)
        {
            Shimmer.Configure(_device.DeviceName, serialPort);

            Shimmer.NumberOfHeartBeatsToAverage = _config.HeartBeatsToAverage;
            Shimmer.TrainingPeriodPPG = _config.TrainingPeriodPPG;
            Shimmer.LowPassFilterCutoff = _config.LowPassFilterCutoff;
            Shimmer.HighPassFilterCutoff = _config.HighPassFilterCutoff;
            Shimmer.SamplingRate = _config.SamplingRate;
            Shimmer.EnableAccelerator = _sensors.EnableAccelerator;
            Shimmer.EnableGSR = _sensors.EnableGSR;
            Shimmer.EnablePPG = _sensors.EnablePPG;
        }

        private void HandleData()
        {
            if (Shimmer.LatestData != null) _dataBuffer.Add(Shimmer.LatestData);

            if (_dataBuffer.Count >= _config.SamplesBufferSize) 
            {
                IOManager.Append(IOManager.Sensor.SHIMMER, _dataBuffer);
                _dataBuffer.Clear();
            }
        }

        public void OnApplicationQuit()
        {
            if (!_device.Enabled) return;

            StopStreaming();
            Disconnect();
        }

        public void AttemptConnection()
        {
            if (State != ConnectionState.INACTIVE)
            {
                State = ConnectionState.CONNECTING;

                string[] serialPorts = SerialPortsManager.GetAvailableSerialPortsNames();
                LogUtils.LogShimmer("Avaialbe serial ports: [ " + string.Join(", ", serialPorts) + " ]");
                foreach (string serialPort in serialPorts)
                {
                    Configure(serialPort);

                    LogUtils.LogShimmer("Attempting connection on serial port " + serialPort + " ...");
                    Shimmer.Connect();
                    if (Shimmer.IsConnected())
                    {
                        LogUtils.LogShimmer("Shimmer connected on port " + serialPort);
                        State = ConnectionState.CONNECTED;
                        return;
                    }
                }
                LogUtils.LogShimmer("Unable to connect to Shimmer device");
                State = ConnectionState.DISCONNECTED;
            }
            else LogUtils.LogShimmer("Shimmer AttemptConnection called but device is INACTIVE");
        }

        private void Disconnect()
        {
            if (State == ConnectionState.CONNECTED)
            {
                LogUtils.LogShimmer("Disconnecting from Shimmer device");
                Shimmer.Disconnect();
                State = ConnectionState.DISCONNECTED;
            }
            else LogUtils.LogShimmer("Shimmer Disconnect called but device is not CONNECTED (current state is " + State.ToString() + ")");
        }

        public void StartStreaming()
        {
            if (State == ConnectionState.CONNECTED)
            { 
                LogUtils.LogShimmer("Shimmer device Start streaming");
                Shimmer.StartStreaming();
                State = ConnectionState.STREAMING;

                _stopwatch.Start();
            }
            else LogUtils.LogShimmer("Shimmer StartStreaming called but device is not CONNECTED (current state is " + State.ToString() + ")");
        }

        public void StopStreaming()
        {
            if (State == ConnectionState.STREAMING)
            { 
                LogUtils.LogShimmer("Shimmer device Stop streaming");
                Task.Run(() => Shimmer.StopStreaming());
                State = ConnectionState.CONNECTED;

                _stopwatch.Reset();
            }
            else LogUtils.LogShimmer("Shimmer StopStreaming called but device is not STREAMING (current state is " + State.ToString() + ")");
        }

        public bool IsEnabled()
        {
            return _device.Enabled;
        }

        public ShimmerGSRData GetLatestData()
        {
            return Shimmer?.LatestData;
        }
    }
}
