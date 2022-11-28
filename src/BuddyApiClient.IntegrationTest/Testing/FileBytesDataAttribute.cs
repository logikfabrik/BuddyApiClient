namespace BuddyApiClient.IntegrationTest.Testing
{
    using System.Reflection;
    using Xunit.Sdk;

    public sealed class FileBytesDataAttribute : DataAttribute
    {
        private readonly string _path;

        public FileBytesDataAttribute(string path)
        {
            _path = path;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (File.Exists(_path))
            {
                yield return new object[] { File.ReadAllBytes(_path) };
            }
            else if (Directory.Exists(_path))
            {
                foreach (var path in Directory.EnumerateFiles(_path, "*.*", SearchOption.AllDirectories))
                {
                    yield return new object[] { File.ReadAllBytes(path) };
                }
            }
        }
    }
}
