using System;
using DNTFrameworkCore.Common;
using DNTFrameworkCore.Domain;

namespace DNTFrameworkCore.TestAPI.Domain.Tasks
{
    public class Task : Entity, IHasRowVersion, IHasRowIntegrity, ICreationTracking, IModificationTracking,
        INumberedEntity
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 1024;

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; } = TaskState.Todo;
        public long BranchId { get; set; }

        public DateTime? NullableDateTime { get; set; }
        [SkipNormalization] public DateTime LocalDateTime { get; set; }
        [SkipNormalization] public decimal Value { get; set; }
        public decimal NormalizedValue { get; set; }

        public byte[] Version { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}