using Fantasista.Archive.LOD.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasista.Archive.LOD
{
    public class LODArchive
    {
        private BinaryReader reader;
        private Int32 header; // Should be 0x00444f4c
        private ArchiveType archiveType;
        private Int32 numberOfFiles;
        private LODEntry[] entries;
        private Dictionary<String, Int32> fileNameIndex;

        public LODArchive(Stream stream)
        {
            reader = new BinaryReader(stream);
            fileNameIndex = new Dictionary<String, Int32>();
            ReadHeader();
            ReadType();
            ReadNumberOfFiles();
            SkipUnknownData();
            ReadFileEntries();
        }

        private void ReadHeader()
        {
            header = reader.ReadInt32();
            if (header != 0x00444f4c)
                throw new IllegalHeaderException();
        }

        private void ReadType()
        {
            var type = reader.ReadInt32();
            if (type == 500)
                archiveType = ArchiveType.ExpansionArchive;
            else if (type == 200)
                archiveType = ArchiveType.BaseArchive;
            else
                archiveType = ArchiveType.Unknown;
        }

        private void ReadNumberOfFiles()
        {
            numberOfFiles = reader.ReadInt32();
            if (numberOfFiles <= 0)
                throw new IllegalHeaderException();
            entries = new LODEntry[numberOfFiles];
        }

        private void SkipUnknownData()
        {
            reader.ReadBytes(80);
        }

        private void ReadFileEntries()
        {
            for (Int32 i = 0; i < numberOfFiles; i++)
                CreateFileFromReader(i);
        }

        private void CreateFileFromReader(Int32 index)
        {
            var nameBytes = reader.ReadBytes(16);
            var name = ReadName(nameBytes);
            var offset = reader.ReadInt32();
            var originalSize = reader.ReadInt32();
            var type = reader.ReadInt32();
            var compressedSize = reader.ReadInt32();
            entries[index] = new LODEntry(name, offset, originalSize, type, compressedSize);
            fileNameIndex.Add(name, index);
        }

        private String ReadName(Byte[] nameBytes)
        {
            for (var i = 0; i < nameBytes.Length;i++ )
                if (nameBytes[i] == 0)
                {
                    return UTF8Encoding.UTF8.GetString(nameBytes.Take(i).ToArray());
                }
            throw new IllegalHeaderException();
        }

        public override string ToString()
        {
            return String.Format("Valid header {0}, type : {1}, files : {2}", header, archiveType, numberOfFiles);
        }

        public LODEntry[] Entries
        {
            get
            {
                return entries;
            }
        }
    }
}
