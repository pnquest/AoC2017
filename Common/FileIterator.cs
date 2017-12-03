using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public class FileIterator : IEnumerable<string>
    {
        private readonly string _path;

        private FileIterator(string path)
        {
            _path = path;
        }

        public IEnumerator<string> GetEnumerator()
        {
            using (StreamReader sr =
                new StreamReader(new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static FileIterator Create(string path)
        {
            return new FileIterator(path);
        }
    }
}
