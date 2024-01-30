namespace BuddyApiClient.Test.Pipelines.Models
{
    using BuddyApiClient.Pipelines.Models;

    public sealed class TriggerConditionTypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TriggerConditionTypeJsonConverter.AlwaysAsJson, TriggerConditionType.Always)]
            [InlineData(TriggerConditionTypeJsonConverter.OnChangeAsJson, TriggerConditionType.OnChange)]
            [InlineData(TriggerConditionTypeJsonConverter.OnChangeAtPathAsJson, TriggerConditionType.OnChangeAtPath)]
            [InlineData(TriggerConditionTypeJsonConverter.VariableIsAsJson, TriggerConditionType.VariableIs)]
            [InlineData(TriggerConditionTypeJsonConverter.VariableIsNotAsJson, TriggerConditionType.VariableIsNot)]
            [InlineData(TriggerConditionTypeJsonConverter.VariableContainsAsJson, TriggerConditionType.VariableContains)]
            [InlineData(TriggerConditionTypeJsonConverter.VariableContainsNotAsJson, TriggerConditionType.VariableContainsNot)]
            [InlineData(TriggerConditionTypeJsonConverter.DateTimeAsJson, TriggerConditionType.DateTime)]
            [InlineData(TriggerConditionTypeJsonConverter.SuccessPipelineAsJson, TriggerConditionType.SuccessPipeline)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, TriggerConditionType expected)
            {
                var enumValue = TriggerConditionTypeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerConditionTypeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(TriggerConditionType.Always, TriggerConditionTypeJsonConverter.AlwaysAsJson)]
            [InlineData(TriggerConditionType.OnChange, TriggerConditionTypeJsonConverter.OnChangeAsJson)]
            [InlineData(TriggerConditionType.OnChangeAtPath, TriggerConditionTypeJsonConverter.OnChangeAtPathAsJson)]
            [InlineData(TriggerConditionType.VariableIs, TriggerConditionTypeJsonConverter.VariableIsAsJson)]
            [InlineData(TriggerConditionType.VariableIsNot, TriggerConditionTypeJsonConverter.VariableIsNotAsJson)]
            [InlineData(TriggerConditionType.VariableContains, TriggerConditionTypeJsonConverter.VariableContainsAsJson)]
            [InlineData(TriggerConditionType.VariableContainsNot, TriggerConditionTypeJsonConverter.VariableContainsNotAsJson)]
            [InlineData(TriggerConditionType.DateTime, TriggerConditionTypeJsonConverter.DateTimeAsJson)]
            [InlineData(TriggerConditionType.SuccessPipeline, TriggerConditionTypeJsonConverter.SuccessPipelineAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(TriggerConditionType enumValue, string expected)
            {
                var json = TriggerConditionTypeJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerConditionTypeJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
