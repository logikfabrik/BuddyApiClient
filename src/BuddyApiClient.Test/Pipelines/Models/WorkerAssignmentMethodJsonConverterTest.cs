namespace BuddyApiClient.Test.Pipelines.Models
{
    using BuddyApiClient.Pipelines.Models;
    using FluentAssertions.Execution;


    public sealed class WorkerAssignmentMethodJsonConverterTest
    {
        public sealed class TryConvertTo
        {
            [Theory]
            [InlineData(WorkerAssignmentMethod.Fixed, WorkerAssignmentMethodJsonConverter.FixedAsJson)]
            [InlineData(WorkerAssignmentMethod.Tags, WorkerAssignmentMethodJsonConverter.TagsAsJson)]
            public void Should_ReturnTrue_When_EnumIsValid(WorkerAssignmentMethod enumValue, string expected)
            {
                using (new AssertionScope())
                {
                    WorkerAssignmentMethodJsonConverter.TryConvertTo(enumValue, out var json).Should().BeTrue();

                    json.Should().Be(expected);
                }
            }

            [Fact]
            public void Should_ReturnFalse_When_EnumIsInvalid()
            {
                using (new AssertionScope())
                {
                    WorkerAssignmentMethodJsonConverter.TryConvertTo(null, out var json).Should().BeFalse();

                    json.Should().BeNull();
                }
            }
        }
    }
}
