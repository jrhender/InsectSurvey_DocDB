 using Microsoft.Azure.Documents; 
 using Microsoft.Azure.Documents.Client; 
 using Microsoft.Azure.Documents.Linq; 
 using System.Linq.Expressions;
 using System.Linq;
 using System.Threading.Tasks;
 using System;
 using System.Collections.Generic;

 //Note: Refactor to be object with DI (http://stackoverflow.com/questions/38833474/how-to-migrate-a-static-class-from-net-to-net-core)

 public static class DocumentDBRepository<T> where T : class
 {
     private static DocumentClient client;

     public static void Initialize(string endpoint, string authKey, string databaseId, string collectionId)
     {
         client = new DocumentClient(new Uri(endpoint), authKey);
         CreateDatabaseIfNotExistsAsync(databaseId).Wait();
         CreateCollectionIfNotExistsAsync(databaseId, collectionId).Wait();
     }

     private static async Task CreateDatabaseIfNotExistsAsync(string databaseId)
     {
         try
         {
             await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
         }
         catch (DocumentClientException e)
         {
             if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
             {
                 await client.CreateDatabaseAsync(new Database { Id = databaseId });
             }
             else
             {
                 throw;
             }
         }
     }

     private static async Task CreateCollectionIfNotExistsAsync(string databaseId, string collectionId)
     {
         try
         {
             await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
         }
         catch (DocumentClientException e)
         {
             if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
             {
                 await client.CreateDocumentCollectionAsync(
                     UriFactory.CreateDatabaseUri(databaseId),
                     new DocumentCollection { Id = collectionId },
                     new RequestOptions { OfferThroughput = 1000 });
             }
             else
             {
                 throw;
             }
         }
     }

    public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string databaseId, string collectionId)
    {
        IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
            UriFactory.CreateDocumentCollectionUri(databaseId, collectionId))
            .Where(predicate)
            .AsDocumentQuery();

        List<T> results = new List<T>();
        while (query.HasMoreResults)
        {
            results.AddRange(await query.ExecuteNextAsync<T>());
        }

        return results;
    }

    public static async Task<Document> CreateItemAsync(T item, string databaseId, string collectionId)
    {
        return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), item);
    }
 }