namespace BuddyApiClient.Test.Testing
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Xunit.Sdk;

    public sealed class FileDataAttribute : DataAttribute
    {
        private readonly string _path;

        public FileDataAttribute(string path)
        {
            _path = path;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (File.Exists(_path))
            {
                yield return new object[] { File.ReadAllText(_path) };
            }
            else if (Directory.Exists(_path))
            {
                foreach (var path in Directory.EnumerateFiles(_path, "*.*", SearchOption.AllDirectories))
                {
                    yield return new object[] { File.ReadAllText(path) };
                }
            }
        }
    }
}