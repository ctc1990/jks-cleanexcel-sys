using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Functions.RecipeManagement.RecipeStructure
{
    class SettingsManagement
    {
        public static bool UploadSettings_Heater(TcAdsClient adsClient, int index, string Setpoint, string FirstAlarm, string SecondAlarm, string Hysteresis, string PreparationTemp, string PreparationTime)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnTempSV[" + index.ToString() + "]", Setpoint, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnTemp1stLimitSv[" + index.ToString() + "]", FirstAlarm, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnTempHALsv[" + index.ToString() + "]", SecondAlarm, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnTempHYSsv[" + index.ToString() + "]", Hysteresis, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnTempRDYsv[" + index.ToString() + "]", PreparationTemp, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARiStnPreparationTempSv[" + index.ToString() + "]", PreparationTime, "int");

                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + Setpoint + "' " +
                    "where variable_name = '.ARrStnTempSV[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + FirstAlarm + "' " +
                    "where variable_name = '.ARrStnTemp1stLimitSv[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + SecondAlarm + "' " +
                    "where variable_name = '.ARrStnTempHALsv[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + Hysteresis + "' " +
                    "where variable_name = '.ARrStnTempHYSsv[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + PreparationTemp + "' " +
                    "where variable_name = '.ARrStnTempRDYsv[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + PreparationTime + "' " +
                    "where variable_name = '.ARiStnPreparationTempSv[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> DownloadSettings_Heater(TcAdsClient adsClient, int index)
        {
            try
            {
                List<string> list = new List<string>();

                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnTempSV[" + index.ToString() + "]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnTemp1stLimitSv[" + index.ToString() + "]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnTempHALsv[" + index.ToString() + "]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnTempHYSsv[" + index.ToString() + "]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnTempRDYsv[" + index.ToString() + "]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARiStnPreparationTempSv[" + index.ToString() + "]"));

                return list;
            }
            catch
            {
                return new List<string>();
            }
        }

        public static bool UploadSettings_Pressure(TcAdsClient adsClient, int index, string LowerBoundary, string UpperBoundary)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnLowVacuumLevelSV[" + index.ToString() + "]", LowerBoundary, "real");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".ARrStnHighVacuumLevelSV[" + index.ToString() + "]", UpperBoundary, "real");

                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + LowerBoundary + "' " +
                    "where variable_name = '.ARrStnLowVacuumLevelSV[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                Functions.SQL.Query.ExecuteNonQuery("update memory_machine set variable_value = '" + UpperBoundary + "' " +
                    "where variable_name = '.ARrStnHighVacuumLevelSV[" + index.ToString() + "]'", new MySql.Data.MySqlClient.MySqlCommand());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> DownloadSettings_Pressure(TcAdsClient adsClient, int index)
        {
            try
            {
                List<string> list = new List<string>();

                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnLowVacuumLevelSV[1]"));
                list.Add(Functions.ADS.ADS_ReadWrite.ADS_ReadValue(adsClient, ".ARrStnHighVacuumLevelSV[1]"));

                return list;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
