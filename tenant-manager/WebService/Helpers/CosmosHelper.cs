using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace MMM.Azure.IoTSolutions.TenantManager.WebService.Helpers
{
    public class CosmosHelper
    {
        private DocumentClient client;

        public CosmosHelper(String cosmosDb, String cosmosDbToken)
        {
            try
            {
                this.client = new DocumentClient(new Uri(cosmosDb), cosmosDbToken);
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }
        public async Task DeleteCosmosDbCollection(string database, string collectionPrefix)
        {
            await client.DeleteDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(database, collectionPrefix));
        }

        private void LogException(Exception e)
        {

            if (e is DocumentClientException)
            {
                DocumentClientException de = (DocumentClientException)e;
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            else
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
        }
    }
}