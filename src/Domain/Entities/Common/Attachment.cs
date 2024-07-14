namespace Domain.Entities.Common
{
    public class Attachment
    {
        public Guid AttachmentId { get; private set; } = Guid.NewGuid();
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public string FileContent { get; private set; } // URL to the storage location
        public DateTime UploadedDate { get; private set; } = DateTime.UtcNow;

        // Private constructor for EF Core
        private Attachment() { }

        public Attachment(string fileName, string fileType, string fileContent)
        {
            FileName = fileName;
            FileType = fileType;
            FileContent = fileContent;
        }
    }
}
