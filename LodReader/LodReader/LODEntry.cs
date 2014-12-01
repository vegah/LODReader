using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasista.Archive.LOD
{
    public class LODEntry
    {
        private String _name;
        private Int32 _offset;
        private Int32 _originalSize;
        private Int32 _type;
        private Int32 _sizeCompressed;

        public LODEntry(String name, Int32 offset, Int32 originalSize, Int32 type, Int32 sizeCompressed)
        {
            _name = name;
            _offset = offset;
            _originalSize = originalSize;
            _type = type;
            _sizeCompressed = sizeCompressed;
        }

        public override string ToString()
        {
            return String.Format("{0}, uncompressed: {1}, compressed: {2}, type: {3}, offset: {4}", _name, _originalSize, _sizeCompressed, _type, _offset);
        }
    }
}
