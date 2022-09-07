namespace BuddyApiClient.Actions.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record AddSleepAction : AddAction
    {
        private int _sleepInSeconds;

        public AddSleepAction(string name) : base(name, Type.Sleep)
        {
        }

        [JsonPropertyName("sleep_in_sec")]
        public int SleepInSeconds
        {
            get => _sleepInSeconds;
            set => _sleepInSeconds = Ensure.Comparable.IsInRange(value, 0, 1200, nameof(value));
        }
    }
}