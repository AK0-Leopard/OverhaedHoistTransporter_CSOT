using Mirle.BigDataCollection.DataCollection.Collection;
using Mirle.LCS.FFUC;
using System;

namespace Mirle.BigDataCollection.DataCollection.Cache
{
    public class STKFFUDataCache : ICashe
    {
        public string CommadSpeed { get; }
        public string Rotote_Speed { get; }
        public string Ratio_Speed { get; }

        public STKFFUDataCache(FFUDataCollection collection)
        {
            CommadSpeed = "0";
            Rotote_Speed = collection.Rotote_Speed.GetValue().ToString();

            if (CommadSpeed != "0")
                Ratio_Speed = (Convert.ToInt32(Rotote_Speed) / Convert.ToInt32(CommadSpeed)).ToString();
            else
                Ratio_Speed = "0";
        }

        public STKFFUDataCache()
        {
            CommadSpeed = "0";
            Rotote_Speed = "0";
            Ratio_Speed = "0";
        }

        public STKFFUDataCache(string ffuId, FFUController ffuc)
        {
            var ffuAddr = ffuId.Remove(0, ffuId.Length - 5).Split('-');
            var groupNo = int.Parse(ffuAddr[0]);
            var ffuNo = int.Parse(ffuAddr[1]);

            var ffuGroup = ffuc.GetFFUGroupByNumber(groupNo);
            var ffu = ffuGroup?.GetFFUByNumber(ffuNo);

            Rotote_Speed = ffu.Speed.ToString("F0");
            CommadSpeed = "0";
            Ratio_Speed = "0";
        }
    }
}