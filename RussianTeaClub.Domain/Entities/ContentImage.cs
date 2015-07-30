using System;

namespace RussianTeaClub.Domain.Entities
{
    public class ContentImage
    {
        public ContentImage()
        {
            ContentImageId = Guid.NewGuid();
        }

        public ContentImage(string id)
        {
            ContentImageId = Guid.Parse(id);
        }

        public Guid ContentImageId { get; set; }

        public virtual Article Article { get; set; }

        public Guid? ArticleId { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}