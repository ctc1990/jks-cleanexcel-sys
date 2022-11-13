using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Functions.RecipeManagement.SubProcessBlock
{
    static class Calibration
    {
        private static void ResetEquipmentStatus(TcAdsClient adsClient)
        {
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bManualModeOn", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bManualModeOnPB", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bManualModeOnLatch", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bManualModeOnPB", "true", "bool");
        }

        private static void GenerateVacuum(TcAdsClient adsClient)
        {
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10009_15", "true", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10112_15", "true", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10113_15", "true", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10200_15", "false", "bool");
        }

        private static void SaveGenerateVacuum(TcAdsClient adsClient, double HighestValue)
        {
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARlrStnVacuumMaxReadSV[1]", HighestValue.ToString(), "lreal");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARbStnHighVacuumLvlSave[1]", "true", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARbStnHighVacuumLvlSave[1]", "false", "bool");
        }

        private static void ReleaseVacuum(TcAdsClient adsClient)
        {
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10009_15", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10112_15", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10113_15", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".WY10200_15", "true", "bool");
        }

        private static void SaveReleaseVacuum(TcAdsClient adsClient, double LowestValue)
        {
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARbStnLowVacuumLvlSave[1]", "true", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARbStnLowVacuumLvlSave[1]", "false", "bool");
        }

        public static void CalibrateVacuumLevel(TcAdsClient adsClient, double LowestValue, double HighestValue, string analogueVariable)
        {
            // Reset all Equipments Status
            ResetEquipmentStatus(adsClient);
            System.Threading.Thread.Sleep(100);

            // Generate Air Pressure
            GenerateVacuum(adsClient);

            // Wait for steady state
            WaitForSteadyState(adsClient, analogueVariable);

            // Save Generated Air Pressure State
            SaveGenerateVacuum(adsClient, HighestValue);

            // Release Air Pressure
            ReleaseVacuum(adsClient);

            // Wait for steady state
            WaitForSteadyState(adsClient, analogueVariable);

            // Save Released Air Pressure State
            SaveReleaseVacuum(adsClient, LowestValue);

            // Reset all Equipments Status
            ResetEquipmentStatus(adsClient);
        }

        private static float WaitForSteadyState(TcAdsClient adsClient, string analogueVariable)
        {
            float threshold = 1;
            float present = 0;
            float past = 0;
            int sampling_time = 2000;

            // Waiting for low fluctuations (based on threshold value)
            do
            {
                past = present;

                System.Threading.Thread.Sleep(sampling_time);
                present = Convert.ToSingle(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, analogueVariable));

            } while (Math.Abs(present - past) > threshold);

            return present;
        }
    }
}
