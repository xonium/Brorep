using Brorep.Application.Interfaces.Mapping;
using System;

namespace Brorep.Application.JudgingType.Models
{
    public class JudgingTypeDto : IMapFrom<Domain.Entities.JudgingType>
    {
        public Guid JudgingTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
