namespace BuddyApiClient.Variables.Models.Response
{
    public interface IVariable
    {
        public VariableId Id { get; set; }

        public string? Key { get; set; }

        public string? Value { get; set; }

        public Type Type { get; set; }

        public string? Description { get; set; }

        public bool Settable { get; set; }

        public bool Encrypted { get; set; }

        public FilePlace? FilePlace { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public FilePermission? FilePermission { get; set; }

        public string? PublicValue { get; set; }

        public string? KeyFingerprint { get; set; }

        public string? Checksum { get; set; }
    }
}