using System;
using System.Drawing;
using System.IO;
using System.Net;
using AngleSharp;

namespace VocabularyApi.Services
{
    public class ImageWebLoaderService
    {
        private const string BaseUri = "https://yandex.ru/images/search?text=";
        private const string ImageQuerySelector = "a.serp-item__link img";

        private int _imageWidth = 220;
        private int _imageHeight = 220;

        private int _thumbnailWidth = 60;
        private int _thumbnailHeight = 60;

        private string GetSearchImageUri(string word)
        {
            return BaseUri + word;
        }

        public Image GetImage(string word)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(GetSearchImageUri(word)).Result;

            var element = document.QuerySelector(ImageQuerySelector);
            var href = element.GetAttribute("src");
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead("http:" + href);
                return Image.FromStream(stream);
            }
        }

        public byte[] GetImage(Image image)
        {
            return GetImage(image, _imageWidth, _imageHeight);
        }

        public byte[] GetThumbnail(Image image)
        {
            return GetImage(image, _thumbnailWidth, _thumbnailHeight);
        }

        private byte[] GetImage(Image image, int width, int height)
        {
            if (image.Height > image.Width)
            {
                width = width * image.Width / image.Height;
            }
            else
            {
                height = height * image.Height / image.Width;
            }

            var thumbnail = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

            using (var memoryStream = new MemoryStream())
            {
                thumbnail.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}
