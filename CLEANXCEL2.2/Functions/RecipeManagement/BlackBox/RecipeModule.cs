using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwinCAT.Ads;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CLEANXCEL2._2.Functions.RecipeManagement.BlackBox
{
    class RecipeModule
    {
        private static readonly string DSProcess = ".AR10DSStationSSERStore[1].DsStationSequenceRecipeMemory.";
        private static readonly string DSSubProcess = ".AR10DSStationSSURStore[1].DsSubRecipeMemory.";

        public static bool UploadSQLSubProcessId(string SubProcessName)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@sub_process_name", SubProcessName);
                mySqlCommand.Parameters.AddWithValue("@station", "1");
                mySqlCommand.Parameters.AddWithValue("@status", "1");
                Functions.SQL.Query.ExecuteNonQuery("insert into sub_process_id (name, station, status) values(@sub_process_name, @station, @status);", mySqlCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadSQLSubProcess(string EquipmentName, string EquipmentState, string Conditions)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@equipment_name", EquipmentName);
                mySqlCommand.Parameters.AddWithValue("@equipment_state", EquipmentState);
                mySqlCommand.Parameters.AddWithValue("@conditions", Conditions);
                Functions.SQL.Query.ExecuteNonQuery("insert into sub_process (sub_process_name, equipment_name, equipment_state, conditions) values(" +
                    "(select max(id) from sub_process_id), @equipment_name, @equipment_state, @conditions);", mySqlCommand);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadSQLProcessId(string ProcessName)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@process_name", ProcessName);
                mySqlCommand.Parameters.AddWithValue("@station", "1");
                mySqlCommand.Parameters.AddWithValue("@status", "1");
                Functions.SQL.Query.ExecuteNonQuery("insert into process_id (name, station, status) values(@process_name, @station, @status);", mySqlCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadSQLProcess(string SubProcessName, string Conditions)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@sub_process_name", SubProcessName);
                mySqlCommand.Parameters.AddWithValue("@conditions", Conditions);
                Functions.SQL.Query.ExecuteNonQuery("insert into process (process_name, sub_process_name, conditions) values(" +
                    "(select max(id) from process_id), (select id from sub_process_id where sub_process_id.name = @sub_process_name), @conditions);", mySqlCommand);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadSQLRecipeId(string RecipeName)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@recipe_name", RecipeName);
                mySqlCommand.Parameters.AddWithValue("@station", "1");
                mySqlCommand.Parameters.AddWithValue("@status", "1");
                Functions.SQL.Query.ExecuteNonQuery("insert into recipe_id (name, station, status) values(@recipe_name, @station, @status);", mySqlCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadSQLRecipe(string ProcessName, string ProcessTime, string Power, string Frequency)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@process_name", ProcessName);
                mySqlCommand.Parameters.AddWithValue("@process_time", ProcessTime);
                mySqlCommand.Parameters.AddWithValue("@parameters", "iOut3ProTankBtmUsAPwrPercent=" + Power + "~iOut8ProTankBtmUsAkHz=" + Frequency);
                mySqlCommand.Parameters.AddWithValue("@cycle", "1");
                mySqlCommand.Parameters.AddWithValue("@repeat_number", "1");
                Functions.SQL.Query.ExecuteNonQuery("insert into recipe (recipe_name, process_name, process_time, parameters, cycle, repeat_number) values(" +
                    "(select max(id) from recipe_id), (select id from process_id where process_id.name = @process_name), @process_time, @parameters, @cycle, @repeat_number);", mySqlCommand);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void UploadRecipe(TcAdsClient adsClient, string recipe_name, int totaltime)
        {
            List<ProcessFlow> list = UploadSSUR(adsClient, recipe_name);
            
            UploadSSER(adsClient, recipe_name, totaltime, list);
        }

        private static List<ProcessFlow> UploadSSUR(TcAdsClient adsClient, string recipe_name)
        {
            int k = 0;
            int count = 1;
            int temp_start;
            List<ProcessFlow> process_Run = new List<ProcessFlow>();
            List<Functions.RecipeManagement.BlackBox.ProcessExtraction.SubProcess> subProcessList = new List<Functions.RecipeManagement.BlackBox.ProcessExtraction.SubProcess>();

            List<List<string>> process = Functions.SQL.Query.ExecuteMultiQuery(
                "select recipe_id.name, recipe.process_name, recipe.parameters from recipe right join recipe_id on " +
                "recipe_id.id = recipe.recipe_name where recipe_id.id = '" + recipe_name + "' and recipe_id.status = '1'",
                new string[] { "name", "process_name", "parameters" });

            // Set of SSUR
            
            for (int a = 0; a < process[1].Count(); a++)
            {
                //Edit on 2020/06/05 - recipe not running in surface pro
                string[] parameters1 = process[2][a].Split(new char[] { '=', '~' });

                string[] parameters = parameters1.Where(val => val != "").ToArray();

                // Time Cutting
                subProcessList = new List<Functions.RecipeManagement.BlackBox.ProcessExtraction.SubProcess>();
                foreach (string subprocess in Functions.SQL.Query.ExecuteSingleQuery(
                    "select process.id from process right join process_id on process_id.id = process.process_name where process_id.id = '" + process[1][a] + "' and process_id.status = '1'", "id"))
                {
                    k = 0;
                    string[] conditions = Functions.SQL.Query.ExecuteSingleQuery("select process.conditions from process where process.id = '" + subprocess + "'", "conditions").First().Split('~');
                    List<List<string>> equipment = Functions.SQL.Query.ExecuteMultiQuery(
                        "select sub_process.id, sub_process.equipment_state, sub_process_id.name from sub_process " +
                        "right join sub_process_id on sub_process_id.id = sub_process.sub_process_name where sub_process_id.id = " +
                        "(select process.sub_process_name from process where process.id = '" + subprocess + "') and sub_process_id.status = '1'",
                        new string[] { "id", "equipment_state", "name" });

                    for (int i = 0; i < equipment[0].Count(); i++)
                    {
                        string[] time = conditions[k].Split(':');
                        if (i < equipment[0].Count() - 1)
                        {
                            if (equipment[1][i] != equipment[1][i + 1])
                                k++;
                        }
                        int StartTime = Convert.ToInt32(time[0]);
                        int StopTime = Convert.ToInt32(time[1]);
                        subProcessList.Add(Functions.RecipeManagement.BlackBox.ProcessExtraction.ExecuteProcessExtraction(equipment[0][i], StartTime, StopTime, equipment[2][i]));
                    }

                }
                List<List<string>> timeExtract = Functions.RecipeManagement.BlackBox.TimeManagement.ExecuteTimeCutter(subProcessList);

                // Writing SSUR
                temp_start = count;
                for (int i = 0; i < timeExtract[0].Count; i++)
                {
                    Functions.RecipeManagement.RecipeStructure.StandardCommand.SaveRecipe(adsClient, false);
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".AR10DSStationSSURStore[1].iSubRecipeNo", (count++).ToString(), "int");
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".AR10DSStationSSURStore[1].sSubRecipeDescription", process[0][a] + "\n" + timeExtract[1][i], "string");
                    
                    //Edit on 2020/05/29 - recipe not running in surface pro
                    string[] subprocess1 = timeExtract[0][i].Split('~', '=');

                    string[] subprocess = subprocess1.Where(val => val != "").ToArray();
                    for (int j = 0; j < subprocess.Count(); j += 2)
                    {
                        if(!string.IsNullOrEmpty(subprocess[j]))
                        {
                            if(!string.IsNullOrEmpty(subprocess[j]))
                            {
                                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSSubProcess + subprocess[j], subprocess[j + 1], datatype(subprocess[j]));
                            }                          
                        }               
                    }
                    for (int j = 0; j < parameters.Count(); j += 2)
                    {
                        if (!string.IsNullOrEmpty(parameters[j]))
                        {
                            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSSubProcess + parameters[j], parameters[j + 1], datatype(parameters[0]));
                        }
                    }
                    Functions.RecipeManagement.RecipeStructure.StandardCommand.SaveRecipe(adsClient, true);
                }
                //hashtable.Add(process[0][a], new ProcessFlow() { start = temp_start, stop = count - 1 }); //temporary unuse
                process_Run.Add(new ProcessFlow() { start = temp_start, stop = count - 1 });
            }

            return process_Run;
        }

        private static void UploadSSER(TcAdsClient adsClient, string recipe_name, int totaltime, List<ProcessFlow> process_Run)
        {
            int k = 0;
            // Set of SSER
            List<List<string>> recipe = Functions.SQL.Query.ExecuteMultiQuery(
                "select recipe.cycle, recipe.repeat_number from recipe " +
                "right join recipe_id on recipe_id.id = recipe.recipe_name where recipe_id.id = '" + recipe_name + "' and recipe_id.status = '1'",
                new string[] { "cycle", "repeat_number" });
            List<int> process_start = new List<int>();
            k = 1;

            // Writing SSER
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".AR10DSStationSSERStore[1].iStationSequenceRecipeNo", "1", "int");
            for (int i = 0; i < recipe[0].Count(); i++)
            {
                ProcessFlow processFlow = process_Run[i];
                //ProcessFlow processFlow = (ProcessFlow)hashtable[recipe[0][i]];
                process_start.Add(k);
                for (int j = processFlow.start; j <= processFlow.stop; j++)
                {
                    Functions.RecipeManagement.RecipeStructure.StandardCommand.SaveRecipe(adsClient, false);
                    if (j == processFlow.stop)
                    {
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "AR10iCycle[" + k + "]", recipe[0][i].ToString(), "int");
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "AR10iRepeatFromStepNo[" + k + "]"
                            , process_start[Convert.ToInt32(recipe[1][i]) - 1].ToString(), "int");
                    }
                    else
                    {
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "AR10iCycle[" + k + "]", "1", "int");
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "AR10iRepeatFromStepNo[" + k + "]", "1", "int");
                    }
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "AR10iStationSubProNo[" + (k++) + "]", j.ToString(), "int");
                    Functions.RecipeManagement.RecipeStructure.StandardCommand.SaveRecipe(adsClient, true);
                }
            }
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "iProcessMinTime", totaltime.ToString(), "int");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "iProcessMaxTime", totaltime.ToString(), "int");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, DSProcess + "iProcessCalculatedTime", totaltime.ToString(), "int");
        }

        private static string datatype(string variable_name)
        {
            switch (variable_name.Substring(0, 1))
            {
                case "b":
                    return "bool";
                case "i":
                    return "int";
                case "r":
                    return "real";
                case "s":
                    return "string";
                default:
                    return "";
            }
        }

        private struct ProcessFlow
        {
            public int start;
            public int stop;
        }
    }
}
