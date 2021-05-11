using Sitecore.CH.Project.WebClientSDK.Examples.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Operations
{
    internal static class DeleteEntity
    {
        public static async void DeleteEntityById(long entityId)
        {
            try
            {
                await MConnector.Client().Entities.DeleteAsync(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
