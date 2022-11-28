namespace BuddyApiClient.Test.Actions.Models.Response
{
    using System.Text.Json;
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.Test.Testing;

    public sealed class ActionDetailsJsonDeserializerTest
    {
        public sealed class Read
        {
            [Theory]
            [FileBytesData(@"Actions/Models/Response/.testdata/Read_Should_ReturnTheSleepAction.json")]
            public void Should_ReturnTheSleepAction(byte[] responseJson)
            {
                var sut = new ActionDetailsJsonDeserializer();

                var reader = new Utf8JsonReader(responseJson);

                var action = sut.Read(ref reader, typeof(ActionDetails), new JsonSerializerOptions());

                action.Should().BeOfType<SleepActionDetails>();
            }
        }

        public sealed class Write
        {
            [Fact]
            public void Should_Throw()
            {
                var sut = new ActionDetailsJsonDeserializer();

                var act = FluentActions.Invoking(() => sut.Write(new Utf8JsonWriter(Stream.Null), new SleepActionDetails(), new JsonSerializerOptions()));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
