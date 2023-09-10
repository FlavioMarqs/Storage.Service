using Storage.Domain.Interfaces;

namespace Storage.Domain
{
    public class StringDTO : IDTO
    {
        public int Identifier { get; set; }

        public string StringValue { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}