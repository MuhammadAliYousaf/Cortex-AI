using CortexAI.Domain.Common;
using CortexAI.Domain.Specifications;

namespace CortexAI.Domain.Tests;

public class SpecificationTests
{
    [Fact]
    public void Specification_ExposesConfiguredQueryMetadata()
    {
        var specification = new ActiveRecordSpecification();

        Assert.NotNull(specification.Criteria);
        Assert.NotNull(specification.OrderBy);
        Assert.True(specification.AsNoTracking);
    }

    private sealed class TestRecord : Entity
    {
        public bool IsActive { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    private sealed class ActiveRecordSpecification : Specification<TestRecord>
    {
        public ActiveRecordSpecification()
        {
            Where(record => record.IsActive);
            OrderByAscending(record => record.Name);
            UseNoTracking();
        }
    }
}
