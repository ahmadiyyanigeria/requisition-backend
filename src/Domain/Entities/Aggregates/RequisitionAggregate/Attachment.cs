namespace Domain.Entities.Aggregates.RequisitionAggregate
{
    public class Attachment(string fileName, string fileType, string fileContent)
    {
        public Guid AttachmentId { get; private set; } = Guid.NewGuid();
        public string FileName { get; private set; } = fileName;
        public string FileType { get; private set; } = fileType;
        public string FileContent { get; private set; } = fileContent;// URL to the storage location
        public DateTime UploadedDate { get; private set; } = DateTime.UtcNow;
    }
}
