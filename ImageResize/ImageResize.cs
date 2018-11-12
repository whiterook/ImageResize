using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage.Blob;
//using System.Windows.Media.Imaging;

namespace ImageResize
{
    public static class ImageResize
    {
        private static readonly string BlobStorageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        private static readonly string ThumbnailsContainerName = Environment.GetEnvironmentVariable("ThumbnailsContainerName");

        [FunctionName("ImageResize")]
        public static async System.Threading.Tasks.Task RunAsync([BlobTrigger("images/{name}", Connection = "")]Stream blobStream, string name, ILogger log)
        {
            if (blobStream != null)
            {

                log.LogInformation(
                    $"C# Blob trigger function Processed blob\n Name:{name} \n Size: {blobStream.Length} Bytes");

                try
                {
                    var storageAccount = CloudStorageAccount.Parse(BlobStorageConnectionString);
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    var blobContainer = blobClient.GetContainerReference(ThumbnailsContainerName);
                    
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(name);

                    await blob.UploadFromStreamAsync(blobStream);
                }
                catch (Exception ex)
                {
                    log.LogInformation(ex.Message);
                    throw;
                }
            }
        }

        //private static IImageEncoder GetEncoder(string extension)
        //{
        //    IImageEncoder encoder = null;

        //    extension = extension.Replace(".", "");

        //    var isSupported = Regex.IsMatch(extension, "gif|png|jpe?g", RegexOptions.IgnoreCase);

        //    if (isSupported)
        //    {
        //        switch (extension)
        //        {
        //            case "png":
        //                encoder = new PngEncoder();
        //                break;
        //            case "jpg":
        //                encoder = new JpegEncoder();
        //                break;
        //            case "jpeg":
        //                encoder = new JpegEncoder();
        //                break;
        //            case "gif":
        //                encoder = new GifEncoder();
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return encoder;
        //}

    }
}
