using Mirle.Logger;
using System;
using System.Diagnostics;

namespace Mirle.BigDataCollection
{
    public class LoggerService : IDisposable
    {
        private readonly Log _log = new Log();
        private readonly object _logLock = new object();

        public void WriteException(string strFunSubName, string strMsg)
        {
            try
            {
                lock (_logLock)
                {
                    _log.WriteLogFile($"{_deviceID}_DataCollectionService_Exception.log", strFunSubName.PadRight(30, ' ') + ":" + strMsg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void WriteLog(string fileName, string msg)
        {
            try
            {
                lock (_logLock)
                {
                    _log.WriteLogFile($"{_deviceID}_DataCollectionService_{fileName}", msg);
                }
            }
            catch (Exception ex)
            {
                WriteException(nameof(WriteLog), $" : {msg} | {ex.Message}");
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // 偵測多餘的呼叫
        private string _deviceID;

        public LoggerService(string deviceID)
        {
            _deviceID = deviceID;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置受控狀態 (受控物件)。
                }
                _log?.Dispose();

                // TODO: 釋放非受控資源 (非受控物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放非受控資源的程式碼時，才覆寫完成項。
        ~LoggerService()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}