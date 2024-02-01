using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLEANXCEL2._2.Functions.RecipeManagement.RecipeStructure
{
    class DataManagement
    {
        public static bool RegisterPartName(string PartName, string Description, string RecipeID, string BatchNo)
        {
            try
            {
                Globals.currentPage = null;
                Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Parameters.AddWithValue("@name", PartName);

                if (!Functions.SQL.Query.ExecuteCheckQuery("select * from part_id where part_id.name=@name", mySqlCommand))
                {
                    mySqlCommand.Parameters.AddWithValue("@description", Description);
                    mySqlCommand.Parameters.AddWithValue("@recipe_name", RecipeID);
                    mySqlCommand.Parameters.AddWithValue("@batch_no", BatchNo);
                    Functions.SQL.Query.ExecuteNonQuery("insert into part_id (name, status) values (@name, '1')", mySqlCommand);
                    Functions.SQL.Query.ExecuteNonQuery("insert into part (part_name, description, recipe_name, batch_no) " +
                                                        "values ((select max(part_id.id) from part_id), @description, @recipe_name, @batch_no)", mySqlCommand);
                }
                else // Stay on the page
                {
                    Globals.POPUP_REQUEST("51", "41", CLEANXCEL2._2.Pages.Window.WindowsMessageBox.State.Ok);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<List<string>> LoadPartName()
        {

            List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery(
                @"select 
                	part_info.name as part_name, 
                	part_info.description, 
                	part_info.recipe_id as recipe_id, 
                	recipe_id.name as recipe_name, 
                	part_info.batch_no 
                from 
                	(select part_id.name, 
                	 part.description, 
                	 part.recipe_id, 
                	 part.batch_no, 
                	 part_id.status 
                		from part 
                		right join part_id on part.fk_part_id = part_id.id) as part_info 
                left join recipe_id on part_info.recipe_id = recipe_id.id 
                where part_info.status = '1'",
                new string[] { "part_name", "description", "recipe_id", "recipe_name", "batch_no" });

            return list;
        }

        public static List<List<string>> LoadRecipeName()
        {
            List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery("select * from recipe_id",
                                    new string[] { "id", "name" });

            return list;
        }
    }
}
