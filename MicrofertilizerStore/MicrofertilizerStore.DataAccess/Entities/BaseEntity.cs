namespace MicrofertilizerStore.DataAccess.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; } // key in db
        public Guid ExternalId { get; set; } // unique index - unique optional
        public DateTime ModificationTime { get; set; } 
        public DateTime CreationTime { get; set; }

        public bool IsNew()
        {
            return ExternalId == Guid.Empty;
        }

        public void Init()
        {
            ExternalId = Guid.NewGuid();
            ModificationTime = DateTime.Now;
            CreationTime = DateTime.Now;
        }
    }
}