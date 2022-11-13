using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Functions.RecipeManagement.RecipeStructure
{
    class StandardCommand
    {
        public static bool SaveRecipe(TcAdsClient adsClient, bool condition)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bRecipeSavePb", condition.ToString(), "bool");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EmptyRecipe(TcAdsClient adsClient)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bRecipeEmptyPB", "true", "bool");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bRecipeEmptyPB", "false", "bool");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool StartRecipeNumber(TcAdsClient adsClient, int i)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bBasketCfmEn", "true", "bool");
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".DSStnBasketInfo[1].iStationSeqenceRecipeNo", i.ToString(), "int");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool StopProcess(TcAdsClient adsClient)
        {
            try
            {
                Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bBasketCfmEn", "false", "bool");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
