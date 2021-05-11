using Stylelabs.M.Base.Querying;
using Stylelabs.M.Sdk.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using System.Threading.Tasks;
using Stylelabs.M.Base.Querying.Linq;
using Stylelabs.M.Framework.Essentials.LoadOptions;
using Sitecore.CH.Project.WebClientSDK.Examples.Client;
using Sitecore.CH.Project.WebClientSDK.Examples.Model;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Operations
{
    internal static class DuplicateAssetService
    {
        internal static async Task<List<Asset>> GetDuplicateAssetsAsync()
        {
            Dictionary<long, long> masterFileToMapperIds = new Dictionary<long, long>();

            EntityLoadConfiguration loadConfiguration = new EntityLoadConfiguration()
            {
                CultureLoadOption = CultureLoadOption.Default,
                PropertyLoadOption = new PropertyLoadOption("FileName"),
                RelationLoadOption = new RelationLoadOption("MasterFile")
            };

            List<Asset> duplicateAssets = new List<Asset>();

            List<IEntity> entities = new List<IEntity>();
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.Asset"
                                                      && e.Parent("ContentRepositoryToAsset") == 734 //Standard (DAM)
                                                      && e.Parent("FinalLifeCycleStatusToAsset") == 544 // Approved
                                                      && e.Property("HasDuplicate") == true
                                                      select e);

            var queryResult = await MConnector.Client().Querying.QueryAsync(query, loadConfiguration);

            foreach (var entity in queryResult.Items)
            {
                var entityId = entity.Id;

                var fileName = entity.GetPropertyValue<string>("Filename");
                var masterFile = entity.GetRelation("MasterFile")?.GetIds();
                duplicateAssets.Add(
                    new Asset()
                    {
                        EntityId = entityId.Value,
                        FileName = fileName,
                        MasterFile = masterFile.First()
                    });
            }
            entities.AddRange(queryResult.Items);

            List<long> masterFileIds = duplicateAssets.Select(f => f.MasterFile).ToList();
            masterFileToMapperIds = Task.Run(async () => await FetchFileDuplicateToFileByMasterFile(masterFileIds)).Result;

            if (masterFileToMapperIds != null)
            {
                foreach (var asset in duplicateAssets)
                {
                    if (masterFileToMapperIds.TryGetValue(asset.MasterFile, out long mapperId))
                    {
                        asset.DuplicateFileMapperId = mapperId.ToString();
                    }
                }
            }

            return duplicateAssets;
        }

        public static async Task<Dictionary<long, long>> FetchFileDuplicateToFileByMasterFile(
            List<long> masterFileIds)
        {
            Dictionary<long, long> masterFileToMapperIds = new Dictionary<long, long>();

            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.File"
                                                         && e.Id.In(masterFileIds)
                                                      select e);

            var entityLoadConfig = new EntityLoadConfiguration
            {
                PropertyLoadOption = PropertyLoadOption.None,
                CultureLoadOption = CultureLoadOption.Default,
                RelationLoadOption = new RelationLoadOption("FileDuplicateToFile")
            };

            var queryResult = await MConnector.Client().Querying.QueryAsync(query, entityLoadConfig);

            var entities = queryResult.Items;
            foreach (var entity in entities)
            {
                var relation = entity?.GetRelation("FileDuplicateToFile")?.GetIds();
                if (relation != null && relation.Count > 0)
                {
                    masterFileToMapperIds.Add(entity.Id.Value, relation.First());
                }
            }

            return masterFileToMapperIds;
        }
    }
}
