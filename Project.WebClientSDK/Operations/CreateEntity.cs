using Sitecore.CH.Project.WebClientSDK.Examples.Client;
using Stylelabs.M.Framework.Essentials.LoadOptions;
using Stylelabs.M.Sdk;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Models.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Operations
{
    internal static class CreateEntity
    {
        public static async Task<long> CreateAssetEntity()
        {
            // Create the entity resource
            IEntity asset = await MConnector.Client().EntityFactory
                .CreateAsync(Constants.Asset.DefinitionName, CultureLoadOption.Default).ConfigureAwait(false);

            asset.Identifier = "unique_identifier_2";
            asset.SetPropertyValue("Title", "Sample Asset from SDK");

            //var standardContentRepository = await MConnector.Client().Entities.GetAsync("M.Content.Repository.Standard").ConfigureAwait(false);
            //var contentRepositoryRelation = asset.GetRelation<IChildToManyParentsRelation>("ContentRepositoryToAsset");
            //contentRepositoryRelation.Parents.Add(standardContentRepository.Id.Value);

            var standardContentRepository = ReadEntity.GetEntityIdByIdentifier("M.Content.Repository.Standard");
            var contentRepositoryRelation = asset.GetRelation<IChildToManyParentsRelation>("ContentRepositoryToAsset");
            contentRepositoryRelation.Parents.Add(standardContentRepository.Value);

            //var finalLifeCycleCreated = await MConnector.Client().Entities.GetAsync("M.Final.LifeCycle.Status.Approved").ConfigureAwait(false);
            //var finalLifeCycleRelation = asset.GetRelation<IChildToOneParentRelation>("FinalLifeCycleStatusToAsset");
            //finalLifeCycleRelation.Parent = finalLifeCycleCreated.Id.Value;

            var finalLifeCycleCreated = ReadEntity.GetEntityIdByIdentifier("M.Final.LifeCycle.Status.Approved");
            var finalLifeCycleRelation = asset.GetRelation<IChildToOneParentRelation>("FinalLifeCycleStatusToAsset");
            finalLifeCycleRelation.Parent = finalLifeCycleCreated.Value;

            //var assetType = await MConnector.Client().Entities
            //        .GetAsync("M.AssetType.BrandingAsset").ConfigureAwait(false);
            //var assetTypeRelation = asset.GetRelation<IChildToOneParentRelation>("AssetTypeToAsset");
            //assetTypeRelation.Parent = assetType.Id.Value;

            var assetType = ReadEntity.GetEntityIdByIdentifier("M.AssetType.BrandingAsset");
            var assetTypeRelation = asset.GetRelation<IChildToOneParentRelation>("AssetTypeToAsset");
            assetTypeRelation.Parent = assetType.Value;

            // Create the asset
            var assetId = await MConnector.Client().Entities.SaveAsync(asset).ConfigureAwait(false);

            //Create a fetch job to associate a file
            Uri file = new Uri("https://images.unsplash.com/photo-1504674900247-0877df9cc836?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1050&q=80");
            if (file != null)
                await CreateFetchJob(assetId, file);

            return assetId;
        }

        private static async Task CreateFetchJob(long? assetId, Uri resource)
        {
            var fetchJobRequest = new WebFetchJobRequest("File", assetId.Value);
            fetchJobRequest.Urls.Add(resource);
            
            await MConnector.Client().Jobs.CreateFetchJobAsync(fetchJobRequest).ConfigureAwait(false);
        }
    }
}
