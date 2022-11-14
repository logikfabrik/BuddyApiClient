namespace BuddyApiClient.Test.Actions.Models.Response
{
    using System.Text.Json;
    using BuddyApiClient.Actions.Models.Response;

    public sealed class ActionDetailsJsonDeserializerTest
    {
        public sealed class Write
        {
            [Fact]
            public void Should_Throw()
            {
                var sut = new ActionDetailsJsonDeserializer();

                var act = FluentActions.Invoking(() => sut.Write(new Utf8JsonWriter(Stream.Null), new ActionDetails(), new JsonSerializerOptions()));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
