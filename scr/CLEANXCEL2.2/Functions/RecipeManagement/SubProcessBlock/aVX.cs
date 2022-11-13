using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CLEANXCEL2._2.Functions.RecipeManagement.SubProcessBlock
{
    class aVX
    {   //21102019 - Adding vacuum time
        public static void Encode_aVX(string ProcessName, int start_time, int vacuum_hold_time, int pulses, int vacuum_level, int vacuum_time)
        {
            //int vacuum_time = 0;
            //string value = System.Configuration.ConfigurationManager.AppSettings["vacumn_time"];
            //if (!string.IsNullOrEmpty(value))
            //{
            //    vacuum_time = Convert.ToInt16(value);
            //}
            
            int vacuum_release_time = 5;
            int start, stop = start_time;
            int vacuum_count = Convert.ToInt32(Functions.SQL.Query.ExecuteSingleQuery("select count(sub_process.equipment_name) as vacuum_count from sub_process inner join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.name = 'TEMPLATE VACUUM'", "vacuum_count")[0]);
            int vacuum_hold_count = Convert.ToInt32(Functions.SQL.Query.ExecuteSingleQuery("select count(sub_process.equipment_name) as vacuum_hold_count from sub_process inner join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.name = 'VACUUM HOLD WITH ULTRASONIC'", "vacuum_hold_count")[0]);
            int vacuum_release_count = Convert.ToInt32(Functions.SQL.Query.ExecuteSingleQuery("select count(sub_process.equipment_name) as vacuum_release_count from sub_process inner join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.name = 'VACUUM RELEASE WITH ULTRASONIC'", "vacuum_release_count")[0]);

            string vacuum_conditions = "", vacuum_hold_conditions = "", vacuum_release_conditions = "";

            aVXVacuum_WithoutUltrasonic(vacuum_level);

            for (int i = 0; i < pulses; i++)
            {
                vacuum_conditions = "";
                vacuum_hold_conditions = "";
                vacuum_release_conditions = "";

                start = stop;
                stop = start + vacuum_time;
                for (int j = 0; j < vacuum_count; j++)
                {
                    vacuum_conditions += start + ":" + stop + (j == (vacuum_count - 1) ? "" : "~");
                }
                Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLProcess("VACUUM WITHOUT ULTRASONIC=" + vacuum_level.ToString(), vacuum_conditions);

                start = stop;
                stop = start + vacuum_hold_time;
                for (int j = 0; j < vacuum_hold_count; j++)
                {
                    vacuum_hold_conditions += start + ":" + stop + (j == (vacuum_hold_count - 1) ? "" : "~");
                }
                Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLProcess("VACUUM HOLD WITHOUT ULTRASONIC", vacuum_hold_conditions);

                start = stop;
                stop = start + vacuum_release_time;
                for (int j = 0; j < vacuum_release_count; j++)
                {
                    vacuum_release_conditions += start + ":" + stop + (j == (vacuum_release_count - 1) ? "" : "~");
                }
                Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLProcess("VACUUM RELEASE WITHOUT ULTRASONIC", vacuum_release_conditions);
            }
        }

        #region Avx Block - Version 1
        private static void aVXVacuum(int vacuum_level)
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            if (!Functions.SQL.Query.ExecuteCheckQuery("select * from sub_process_id where sub_process_id.name = 'VACUUM =" + vacuum_level.ToString() + "'", mySqlCommand))
            {
                List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery(
                    "select * from sub_process right join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.name= 'TEMPLATE VACUUM'", new string[] { "equipment_name", "equipment_state", "conditions" });

                Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcessId("VACUUM WITH ULTRASONIC=" + vacuum_level.ToString());

                for (int i = 0; i < list[0].Count(); i++)
                {
                    if (list[2][i] != "")
                        Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(list[0][i], list[1][i], list[2][i] + vacuum_level);
                    else
                        Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(list[0][i], list[1][i], "");
                }
            }
        }
        #endregion

        #region Avx Block - Without Ultrasonic       
        private static void aVXVacuum_WithoutUltrasonic(int vacuum_level)
        {
            MySqlCommand mySqlCommand = new MySqlCommand();
            if (!Functions.SQL.Query.ExecuteCheckQuery("select * from sub_process_id where sub_process_id.name = 'VACUUM WITHOUT ULTRASONIC=" + vacuum_level.ToString() + "'", mySqlCommand))
            {
                List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery(
                    "select * from sub_process right join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.name= 'TEMPLATE VACUUM WITHOUT ULTRASONIC'", new string[] { "equipment_name", "equipment_state", "conditions" });

                Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcessId("VACUUM WITHOUT ULTRASONIC=" + vacuum_level.ToString());

                for (int i = 0; i < list[0].Count(); i++)
                {
                    if (list[2][i] != "")
                        Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(list[0][i], list[1][i], list[2][i] + vacuum_level);
                    else
                        Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(list[0][i], list[1][i], "");
                }
            }
        }
        #endregion
    }
}
