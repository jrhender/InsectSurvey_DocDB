 using Microsoft.Azure.Documents; 
 using Microsoft.Azure.Documents.Client; 
 using Microsoft.Azure.Documents.Linq; 
 using System.Configuration;
 using System.Linq.Expressions;
 using System.Threading.Tasks;
 using System;

 public static class DocumentDBRepository<T> where T : class
 {
     //private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
     //private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
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
 }