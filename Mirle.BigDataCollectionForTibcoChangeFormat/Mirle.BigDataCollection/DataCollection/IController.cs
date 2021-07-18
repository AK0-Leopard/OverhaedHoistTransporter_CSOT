using Mirle.BigDataCollection.Define;
using Mirle.MPLC;

namespace Mirle.BigDataCollection.DataCollection
{
    public abstract class IController
    {
        protected readonly LoggerService _loggerService;
        public readonly DataCollectionINI _dataCollectionINI;
        public readonly IMPLCProvider _SMReadWriter;

        public IController(IMPLCProvider smReadWriter, LoggerService loggerService, DataCollectionINI dataCollectionINI)
        {
            _SMReadWriter = smReadWriter;
            _loggerService = loggerService;
            _dataCollectionINI = dataCollectionINI;
        }
    }
}