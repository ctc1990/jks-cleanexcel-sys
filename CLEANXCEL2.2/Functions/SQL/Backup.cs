using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLEANXCEL2._2.Functions.SQL
{
    class Backup
    {
        private static string server = "localhost";
        private static string database = "fe01fs";
        private static string uid = "root";
        private static string password = "abcd1234";
        private static string connString = "server=" + server + ";database=" + database + ";uid=" + uid + ";password=" + password + ";SSL Mode=none;" +
                                           "charset=utf8;convertzerodatetime=true;";

        public static bool Backup_SQL(string filepath)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                MySqlConnection mySqlConnection = new MySqlConnection(connString);
                MySqlBackup mySqlBackup = new MySqlBackup();

                mySqlCommand.Connection = mySqlConnection;
                mySqlConnection.Open();
                mySqlBackup.ExportToFile(filepath);
                mySqlConnection.Close();

                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public static bool Modify_StatusBLOBStructure()
        {
            try
            {
                string nonquery =
                    "update history_status set parameters = replace(parameters, 'Process Chamber Actual Temperature', 'Process Chamber Temperature (°C) Actual') where parameters like '%Process Chamber Actual Temperature%';" +
                    "update history_status set parameters = replace(parameters, 'Process Chamber Set Temperature', 'Process Chamber Temperature (°C) Set') where parameters like '%Process Chamber Set Temperature%';" +

                        "update history_status set parameters = replace(parameters, 'Sub-Tank 1 Actual Temperature', 'Sub-Tank 1 Temperature (°C) Actual') where parameters like '%Sub-Tank 1 Actual Temperature%';" +
                        "update history_status set parameters = replace(parameters, 'Sub-Tank 1 Set Temperature', 'Sub-Tank 1 Temperature (°C) Set') where parameters like '%Sub-Tank 1 Set Temperature%';" +

                        "update history_status set parameters = replace(parameters, 'Sub-Tank 2 Actual Temperature', 'Sub-Tank 2 Temperature (°C) Actual') where parameters like '%Sub-Tank 2 Actual Temperature%';" +
                        "update history_status set parameters = replace(parameters, 'Sub-Tank 2 Set Temperature', 'Sub-Tank 2 Temperature (°C) Set') where parameters like '%Sub-Tank 2 Set Temperature%';" +

                        "update history_status set parameters = replace(parameters, 'Distillation Tank (Top) Actual Temperature', 'Distillation Tank (Top) Temperature (°C) Actual') where parameters like '%Distillation Tank (Top) Actual Temperature%';" +
                        "update history_status set parameters = replace(parameters, 'Distillation Tank (Top) Set Temperature', 'Distillation Tank (Top) Temperature (°C) Set') where parameters like '%Distillation Tank (Top) Set Temperature%';" +

                        "update history_status set parameters = replace(parameters, 'Distillation Tank (Bottom) Actual Temperature', 'Distillation Tank (Bottom) Temperature (°C) Actual') where parameters like '%Distillation Tank (Bottom) Actual Temperature%';" +
                        "update history_status set parameters = replace(parameters, 'Distillation Tank (Bottom) Set Temperature', 'Distillation Tank (Bottom) Temperature (°C) Set') where parameters like '%Distillation Tank (Bottom) Set Temperature%';" +

                        "update history_status set parameters = replace(parameters, 'Vacuum Pump Actual Pressure', 'Vacuum Pump Pressure (kPa) Actual') where parameters like '%Vacuum Pump Actual Pressure%';" +

                        "update history_status set parameters = replace(parameters, 'Ultrasonic Actual Power Percentage', 'Ultrasonic Power Percentage (%) Actual') where parameters like '%Ultrasonic Actual Power Percentage%';" +
                        "update history_status set parameters = replace(parameters, 'Ultrasonic Set Power Percentage', 'Ultrasonic Power Percentage (%) Set') where parameters like '%Ultrasonic Set Power Percentage%';" +

                        "update history_status set parameters = replace(parameters, 'Ultrasonic Actual Frequency', 'Ultrasonic Frequency (kHz) Actual') where parameters like '%Ultrasonic Actual Frequency%';" +
                        "update history_status set parameters = replace(parameters, 'Ultrasonic Set Frequency', 'Ultrasonic Frequency (kHz) Set') where parameters like '%Ultrasonic Set Frequency%'";

                Functions.SQL.Query.ExecuteNonQuery(nonquery, new MySqlCommand());

                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }

    //update history_status set parameters = replace(parameters, 'Process Chamber Temperature (°C) Actual', '187 Actual') where parameters like '%Process Chamber Temperature (°C) Actual%';
    //update history_status set parameters = replace(parameters, 'Process Chamber Temperature (°C) Set', '187 Set') where parameters like '%Process Chamber Temperature (°C) Set%';

    //update history_status set parameters = replace(parameters, 'Sub-Tank 1 Temperature (°C) Actual', '189 Actual') where parameters like ' %Sub-Tank 1 Temperature (°C) Actual% ';
    //update history_status set parameters = replace(parameters, 'Sub-Tank 1 Temperature (°C) Set', '189 Set') where parameters like '%Sub-Tank 1 Temperature (°C) Set%';

    //update history_status set parameters = replace(parameters, 'Sub-Tank 2 Temperature (°C) Actual', '190 Actual') where parameters like '%Sub-Tank 2 Temperature (°C) Actual%';
    //update history_status set parameters = replace(parameters, 'Sub-Tank 2 Temperature (°C) Set', '190 Temperature (°C) Set') where parameters like '%Sub-Tank 2 Temperature (°C) Set%';

    //update history_status set parameters = replace(parameters, 'Distillation Tank (Top) Temperature (°C) Actual', '191 Actual') where parameters like '%Distillation Tank (Top) Temperature (°C) Actual%';
    //update history_status set parameters = replace(parameters, 'Distillation Tank (Top) Temperature (°C) Set', '191 Set') where parameters like '%Distillation Tank (Top) Temperature (°C) Set%';

    //update history_status set parameters = replace(parameters, 'Distillation Tank (Bottom) Temperature (°C) Actual', '192 Actual') where parameters like '%Distillation Tank (Bottom) Temperature (°C) Actual%';
    //update history_status set parameters = replace(parameters, 'Distillation Tank (Bottom) Temperature (°C) Set', '192 Set') where parameters like '%Distillation Tank (Bottom) Temperature (°C) Set%';

    //update history_status set parameters = replace(parameters, 'Vacuum Pump Pressure (kPa) Actual', '193 Actual') where parameters like '%Vacuum Pump Pressure (kPa) Actual%';

    //update history_status set parameters = replace(parameters, 'Ultrasonic Power Percentage (%) Actual', '194 Actual') where parameters like '%Ultrasonic Power Percentage (%) Actual%';
    //update history_status set parameters = replace(parameters, 'Ultrasonic Power Percentage (%) Set', '194 Set') where parameters like '%Ultrasonic Power Percentage (%) Set%';

    //update history_status set parameters = replace(parameters, 'Ultrasonic Frequency (kHz) Actual', '195 Actual') where parameters like '%Ultrasonic Frequency (kHz) Actual%';
    //update history_status set parameters = replace(parameters, 'Ultrasonic Frequency (kHz) Set', '195 Set') where parameters like '%Ultrasonic Frequency (kHz) Set%';
}
