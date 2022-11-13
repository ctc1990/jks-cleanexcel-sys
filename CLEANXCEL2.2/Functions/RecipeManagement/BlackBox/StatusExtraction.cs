using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLEANXCEL2._2.Functions.ADS;
using CLEANXCEL2._2.UserControls;
using MySql.Data.MySqlClient;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Functions.RecipeManagement.BlackBox
{
    class StatusExtraction
    {
        public static string ExecuteStatusExtraction(TcAdsClient adsClient)
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            string parameters = "";
            try
            {
                List<Schematics.IO> list = (new Schematics()).InitiateMapping();

                foreach (Schematics.IO io in list)
                {
                    try
                    {
                        parameters += io.tag + ":" + ADS_ReadWrite.ADS_ReadValue(adsClient, io.input) + "~";
                    }
                    catch
                    {
                        parameters += io.tag + ":null~";
                    }
                }

                foreach (Sensor sensor in sensors)
                {
                    try
                    {
                        parameters += sensor.tag + ":" + ADS_ReadWrite.ADS_ReadValue(adsClient, sensor.input) + "~";
                    }
                    catch
                    {
                        parameters += sensor.tag + ":null~";
                    }
                }

                return parameters.Remove(parameters.Length-1);
            }
            catch (Exception ex) { return ex.Message.ToString(); }
        }

        private static readonly List<Sensor> sensors = new List<Sensor>() {
            new Sensor(){ tag = "187 Actual", input = ".ARrStnTempPV[1]" },
            new Sensor(){ tag = "187 Set", input = ".ARrStnTempSV[1]" },
            new Sensor(){ tag = "188 Actual", input = ".ARrStnTempPV[6]" },
            new Sensor(){ tag = "188 Set", input = ".ARrStnTempSV[6]" },
            new Sensor(){ tag = "189 Actual", input = ".ARrStnTempPV[2]" },
            new Sensor(){ tag = "189 Set", input = ".ARrStnTempSV[2]" },
            new Sensor(){ tag = "190 Actual", input = ".ARrStnTempPV[3]" },
            new Sensor(){ tag = "190 Set", input = ".ARrStnTempSV[3]" },
            new Sensor(){ tag = "191 Actual", input = ".ARrStnTempPV[5]" },
            new Sensor(){ tag = "191 Set", input = ".ARrStnTempSV[5]" },
            new Sensor(){ tag = "192 Actual", input = ".ARrStnTempPV[4]" },
            new Sensor(){ tag = "192 Set", input = ".ARrStnTempSV[4]" },
            new Sensor(){ tag = "193 Actual", input = ".ARrStnVacuumPV[1]" },
            new Sensor(){ tag = "194 Actual", input = ".ARiStnUSSideAPv[1]" },
            new Sensor(){ tag = "194 Set", input = ".ARDsStnSeqProcessCtrl[1].Out_DSStnSeqProOutput.i3ProTankBtmUsAPwrPercent" },
            new Sensor(){ tag = "195 Actual", input = ".DSWeberEnclosure[1].DSWeberGenerator[1].IN_iFreqSetPoint" },
            new Sensor(){ tag = "195 Set", input = ".DSWeberEnclosure[1].DSWeberGenerator[1].DSWeberMulFreqSwitching.iFreqOutput_TS" }
        };

        //private static readonly List<Sensor> sensors = new List<Sensor>() {
        //    new Sensor(){ tag = "Process Chamber Temperature (°C) Actual", input = ".ARrStnTempPV[1]" },
        //    new Sensor(){ tag = "Process Chamber Temperature (°C) Set", input = ".ARrStnTempSV[1]" },
        //    new Sensor(){ tag = "Chiller Inlet Temperature (°C) Actual", input = ".ARrStnTempPV[6]" },
        //    new Sensor(){ tag = "Chiller Inlet Temperature (°C) Set", input = ".ARrStnTempSV[6]" },
        //    new Sensor(){ tag = "Sub-Tank 1 Temperature (°C) Actual", input = ".ARrStnTempPV[2]" },
        //    new Sensor(){ tag = "Sub-Tank 1 Temperature (°C) Set", input = ".ARrStnTempSV[2]" },
        //    new Sensor(){ tag = "Sub-Tank 2 Temperature (°C) Actual", input = ".ARrStnTempPV[3]" },
        //    new Sensor(){ tag = "Sub-Tank 2 Temperature (°C) Set", input = ".ARrStnTempSV[3]" },
        //    new Sensor(){ tag = "Distillation Tank (Top) Temperature (°C) Actual", input = ".ARrStnTempPV[5]" },
        //    new Sensor(){ tag = "Distillation Tank (Top) Temperature (°C) Set", input = ".ARrStnTempSV[5]" },
        //    new Sensor(){ tag = "Distillation Tank (Bottom) Temperature (°C) Actual", input = ".ARrStnTempPV[4]" },
        //    new Sensor(){ tag = "Distillation Tank (Bottom) Temperature (°C) Set", input = ".ARrStnTempSV[4]" },
        //    new Sensor(){ tag = "Vacuum Pump Pressure (kPa) Actual", input = ".ARrStnVacuumPV[1]" },
        //    new Sensor(){ tag = "Ultrasonic Power Percentage (%) Actual", input = ".ARiStnUSSideAPv[1]" },
        //    new Sensor(){ tag = "Ultrasonic Power Percentage (%) Set", input = ".ARDsStnSeqProcessCtrl[1].Out_DSStnSeqProOutput.i3ProTankBtmUsAPwrPercent" },
        //    new Sensor(){ tag = "Ultrasonic Frequency (kHz) Actual", input = ".DSWeberEnclosure[1].DSWeberGenerator[1].IN_iFreqSetPoint" },    //,".ARiStnManualUSBtmAsv[1]",
        //    new Sensor(){ tag = "Ultrasonic Frequency (kHz) Set", input = ".DSWeberEnclosure[1].DSWeberGenerator[1].DSWeberMulFreqSwitching.iFreqOutput_TS" }
        //};

        private struct Sensor
        {
            public string tag;
            public string input;
        }
    }
}
