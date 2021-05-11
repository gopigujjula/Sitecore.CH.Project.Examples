using Sitecore.CH.Project.WebClientSDK.Examples.Operations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sitecore.CH.Project.WebClientSDK.Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Test Connection
            //try
            //{
            //    await MClient.Client().TestConnectionAsync();
            //    Console.WriteLine("Connection is successfull!");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Exception Occurred: Unable to connect! {ex.Message}");
            //}
            #endregion

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Please input your choice: " +
                    $"{Environment.NewLine}" +
                    $"---------------------Read Operations---------------------- {Environment.NewLine}" +
                    $"1. Fetch Asset File Name by Entity ID {Environment.NewLine}" +
                    $"2. Fetch Entities By Entity Definition {Environment.NewLine}" +
                    $"3. Fetch Assets By Title (Property) {Environment.NewLine}" +
                    $"4. Fetch Assets By Asset Type (Relation) {Environment.NewLine}" +
                    $"5. Fetch Entities By Iterator {Environment.NewLine}" +
                    $"6. Fetch Entities By Scroller {Environment.NewLine}" +
                    $"7. Fetch Duplicate Assets {Environment.NewLine}" +
                    $"---------------------Delete Operations---------------------- {Environment.NewLine}" +
                    $"8. Delete Asset by entity id {Environment.NewLine}" +
                    $"9. Archive Asset by entity id {Environment.NewLine}" +
                    $"10. Soft delete Asset by entity id {Environment.NewLine}" +
                    $"---------------------Update Operations---------------------- {Environment.NewLine}" +
                    $"11. Update Asset by entity id {Environment.NewLine}" +
                    $"---------------------Create Operations---------------------- {Environment.NewLine}" +
                    $"12. Create Asset {Environment.NewLine}" +
                    $"------------------------------------------------------------ {Environment.NewLine}" +
                    $"0. Exit {Environment.NewLine}");

                var input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            {
                                return;
                            }
                        case 1:
                            {
                                Console.WriteLine("Fetch Asset File Name by Entity Id:");
                                Console.WriteLine("Please Input Entity Id.");
                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    var entity = await ReadEntity.GetEntityById(id);
                                    if (entity != null)
                                    {
                                        Console.WriteLine($"Entity Definition: {entity.DefinitionName}");
                                        var fileName = entity.GetPropertyValue<string>("FileName");
                                        Console.WriteLine($"File Name: {fileName}");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Entities By Entity Definition");
                                Console.WriteLine("Please Input Entity Definition name.");
                                var definitionName = Console.ReadLine();
                                var entityQueryresult = await ReadEntity.GetEntitiesByDefinition(definitionName);
                                if (entityQueryresult != null)
                                {
                                    Console.WriteLine($"Total number of entites for " +
                                        $"{definitionName}: {entityQueryresult.TotalNumberOfResults}");
                                    Console.WriteLine($"Number of entites fetched " +
                                        $"{definitionName}: {entityQueryresult.Items.Count}");
                                    Console.WriteLine($"Number of entites offset " +
                                        $"{definitionName}: {entityQueryresult.Offset}");

                                    Console.WriteLine("---------List of top 10 results-----------");
                                    foreach (var entity in entityQueryresult.Items.Take(10))
                                    {
                                        Console.WriteLine($"File Name: {entity.GetPropertyValue<string>("FileName")}");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Fetch Assets By Title (Property)");
                                Console.WriteLine("Please Input Title.");
                                var title = Console.ReadLine();
                                var entityQueryresult = await ReadEntity.GetEntitiesByTitle(title);
                                if (entityQueryresult != null)
                                {
                                    Console.WriteLine($"Total number of entites with Title " +
                                        $"{title}: {entityQueryresult.TotalNumberOfResults}");
                                    Console.WriteLine($"Number of entites fetched with Title " +
                                        $"{title}: {entityQueryresult.Items.Count}");

                                    Console.WriteLine("---------List of top 10 results-----------");
                                    foreach (var entity in entityQueryresult.Items.Take(10))
                                    {
                                        Console.WriteLine($"File Name: {entity.GetPropertyValue<string>("FileName")}");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Fetch Assets By Asset Type (Relation)");
                                Console.WriteLine("Please Input Asset Type Id.");
                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    var entityQueryresult = await ReadEntity.GetAssetsByAssetType(id);
                                    if (entityQueryresult != null)
                                    {
                                        Console.WriteLine($"Total number of entites with Asset Type " +
                                            $"{id}: {entityQueryresult.TotalNumberOfResults}");
                                        Console.WriteLine($"Number of entites fetched with Asset Type" +
                                            $"{id}: {entityQueryresult.Items.Count}");
                                        Console.WriteLine("---------List of top 10 results-----------");
                                        foreach (var entity in entityQueryresult.Items.Take(10))
                                        {
                                            Console.WriteLine($"File Name: {entity.GetPropertyValue<string>("FileName")}");
                                        }
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Assets By Entity Definition using Iterator");
                                Console.WriteLine("Please Input Entity Definition name.");
                                var definitionName = Console.ReadLine();
                                var entities = await ReadEntity.GetEntitiesByDefinitionByIterator(definitionName);
                                if (entities != null)
                                {
                                    Console.WriteLine($"Total number of entites for " +
                                        $"{definitionName}: {entities.Count}");

                                    Console.WriteLine("---------List of top 10 results By Iterator-----------");
                                    foreach (var entity in entities.Take(10))
                                    {
                                        Console.WriteLine($"File Name: {entity.GetPropertyValue<string>("FileName")}");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 6:
                            {
                                Console.WriteLine("Entities By Entity Definition using Scroller");
                                Console.WriteLine("Please Input Entity Definition name.");
                                var definitionName = Console.ReadLine();
                                var entities = await ReadEntity.GetEntitiesByDefinitionByScroller(definitionName);
                                if (entities != null)
                                {
                                    Console.WriteLine($"Total number of entites for " +
                                        $"{definitionName}: {entities.Count}");

                                    Console.WriteLine("---------List of top 10 results By Scroller-----------");
                                    foreach (var entity in entities.Take(10))
                                    {
                                        Console.WriteLine($"File Name: {entity.GetPropertyValue<string>("FileName")}");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 7:
                            {
                                Console.WriteLine("Fetch Duplicate Assets");
                                var duplicateAssets = await DuplicateAssetService.GetDuplicateAssetsAsync();

                                foreach (var asset in duplicateAssets?.OrderBy(f => f.DuplicateFileMapperId))
                                {
                                    Console.WriteLine($"Asset Id: {asset.EntityId}, " +
                                        $"DuplicateFileId: {asset.DuplicateFileMapperId}, " +
                                        $"File Name: {asset.FileName}");
                                }

                                Console.ReadKey();
                                break;
                            }
                        case 8:
                            {
                                Console.WriteLine("Delete Asset By ID");
                                Console.WriteLine("Please Input Entity Id.");

                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    DeleteEntity.DeleteEntityById(id);
                                    Console.WriteLine($"Entity with ID: {id} deleted successfully.");
                                }

                                Console.ReadKey();
                                break;
                            }
                        case 9:
                            {
                                Console.WriteLine("Archive Asset By ID");
                                Console.WriteLine("Please Input Entity Id.");

                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    ArchiveAsset.ArchiveAssetById(id);
                                    Console.WriteLine($"Entity with ID: {id} Archived successfully.");
                                }

                                Console.ReadKey();
                                break;
                            }
                        case 10:
                            {
                                Console.WriteLine("Please input entity id to soft delete.");
                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    var entity = await ReadEntity.GetEntityById(id);
                                    if (entity != null)
                                    {
                                        await UpdateEntity.TrashAssetAsync(entity);
                                        Console.WriteLine($"Entity with id {id} soft delete is successfull");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 11:
                            {
                                Console.WriteLine("Fetch Asset File Name by Entity Id:");
                                Console.WriteLine("Please Input Entity Id.");
                                if (long.TryParse(Console.ReadLine(), out long id))
                                {
                                    var entity = await ReadEntity.GetEntityById(id);
                                    if (entity != null)
                                    {
                                        await UpdateEntity.UpdateAssetAsync(entity);
                                        Console.WriteLine($"Update of the entity with id {id} is successfull");
                                    }
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 12:
                            {
                                Console.WriteLine("Creating Asset entity");
                                var entityId = await CreateEntity.CreateAssetEntity();
                                Console.WriteLine($"Asset is created successfully, Entity ID: {entityId} ");
                                Console.ReadKey();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }
    }
}
