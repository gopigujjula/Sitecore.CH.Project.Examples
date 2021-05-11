using Sitecore.CH.Project.WebClientSDK.Examples.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Operations
{
    internal static class ArchiveAsset
    {
        public static async void ArchiveAssetById(long entityId)
        {
            try
            {
                await MConnector.Client().Assets.FinalLifeCycleManager.ArchiveAsync(entityId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
