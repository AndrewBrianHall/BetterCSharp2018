using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QuestionApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using QuestionApp.Services;

namespace CoreImageGallery.Services
{
    public class AzStorageService : IStorageService
    {
        private const string QuestionPrefix = "question_";

        private readonly CloudStorageAccount _account;
        private readonly CloudBlobClient _client;
        private readonly string _connectionString;
        private CloudBlobContainer _questionContainer;

        public AzStorageService(IConfiguration config)
        {
            _connectionString = config["AzureStorageConnectionString"];
            _account = CloudStorageAccount.Parse(_connectionString);
            _client = _account.CreateCloudBlobClient();
            _questionContainer = _client.GetContainerReference("questions");
        }

        private async Task InitializeContainerAsync()
        {
            await _questionContainer.CreateIfNotExistsAsync();
        }

        public async Task AddQuestionAsync(UserQuestion question)
        {
            await InitializeContainerAsync();

            var questionText = JsonConvert.SerializeObject(question);
            //var byteArray = Encoding.ASCII.GetBytes(questionText);
            var fileName = $"{QuestionPrefix}{questionText.GetHashCode().ToString()}.json";
            var imageBlob = _questionContainer.GetBlockBlobReference(fileName);

            await imageBlob.UploadTextAsync(questionText);
            //using (var ms = new MemoryStream())
            //{
            //    await ms.WriteAsync(byteArray, 0, byteArray.Length);
            //    await imageBlob.UploadFromStreamAsync(ms);
            //}

            await GetQuestionsAsync();
        }

        public async Task<IEnumerable<UserQuestion>> GetQuestionsAsync()
        {
            //await InitializeContainerAsync();

            var questionList = new List<UserQuestion>();
            var token = new BlobContinuationToken();
            var blobList = await _questionContainer.ListBlobsSegmentedAsync(QuestionPrefix, true, BlobListingDetails.All, 1000, token, null, null);

            foreach (CloudBlockBlob blob in blobList.Results)
            {
                CloudBlockBlob blockBlob2 = _questionContainer.GetBlockBlobReference(blob.Name);
                string text;
                using (var memoryStream = new MemoryStream())
                {
                    await blockBlob2.DownloadToStreamAsync(memoryStream);
                    text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                var image = JsonConvert.DeserializeObject<UserQuestion>(text);

                questionList.Add(image);
            }

            return questionList;
        }
    }
}
