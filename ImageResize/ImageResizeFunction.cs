using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ImageResize
{
    public static class ImageResizeFunction
    {
        private static readonly string BLOB_STORAGE_CONNECTION_STRING = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        [FunctionName("ImageResizeFunction")]
        public static void Run(
            [BlobTrigger("images/{name}", Connection = "")]Stream myBlob, 
            string name, 
            TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
