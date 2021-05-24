using com.mirle.ibg3k0.bcf.App;
using com.mirle.ibg3k0.bcf.Data.ValueDefMapAction;
using com.mirle.ibg3k0.bcf.Data.VO;

namespace com.mirle.ibg3k0.sc
{
    public partial class APORTSTATION : BaseEQObject
    {
        public string CST_ID { get; set; }
        public string EQPT_ID { get; set; }
        public override void doShareMemoryInit(BCFAppConstants.RUN_LEVEL runLevel)
        {
            foreach (IValueDefMapAction action in valueDefMapActionDic.Values)
            {
                action.doShareMemoryInit(runLevel);
            }
        }
        public override string ToString()
        {
            return $"{PORT_ID} ({ADR_ID})";
        }

        public bool IsBufferPort(BLL.EquipmentBLL equipmentBLL)
        {
            var eq = equipmentBLL.cache.getEQ(this.EQPT_ID);
            if (eq != null && eq.Type == App.SCAppConstants.EqptType.Buffer)
                return true;
            else
                return false;
        }
        public bool IsCVPort(BLL.EquipmentBLL equipmentBLL)
        {
            var eq = equipmentBLL.cache.getEQ(this.EQPT_ID);
            if (eq != null && eq.Type == App.SCAppConstants.EqptType.OHCV)
                return true;
            else
                return false;
        }
        public bool IsEQPort(BLL.EquipmentBLL equipmentBLL)
        {
            var eq = equipmentBLL.cache.getEQ(this.EQPT_ID);
            if (eq != null && eq.Type == App.SCAppConstants.EqptType.Equipment)
                return true;
            else
                return false;
        }

        public bool IsService(BLL.EquipmentBLL equipmentBLL)
        {
            var eq = equipmentBLL.cache.getEQ(this.EQPT_ID);
            if (eq != null && eq.Type == App.SCAppConstants.EqptType.Buffer)
            {
                return PORT_STATUS == E_PORT_STATUS.InService &&
                       PORT_SERVICE_STATUS == ProtocolFormat.OHTMessage.PortStationServiceStatus.InService;
            }
            else
            {
                return PORT_STATUS == E_PORT_STATUS.InService;
            }

        }
    }

}
