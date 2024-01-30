namespace BuddyApiClient.Actions.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record UpdateSleepAction : UpdateAction
    {
        private int? _sleepInSeconds;

        [JsonPropertyName("sleep_in_sec")]
        public int? SleepInSeconds
        {
            get => _sleepInSeconds;
            set
            {
                if (value is null)
                {
                    _sleepInSeconds = null;

                    return;
                }

                _sleepInSeconds = Ensure.Comparable.IsInRange(value.Value, SleepAction.MinSleepInSeconds, SleepAction.MaxSleepInSeconds, nameof(value));
            }
        }
    }
}
