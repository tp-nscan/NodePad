using System;
using TT;

namespace RepoLocalDB
{
    public enum VectorFormat
    {
        Dense,
        Sparse
    }

    public enum MatrixFormat
    {
        Dense,
        Sparse
    }

    public class VectorRecord
    {
        public int ID { get; set; }
        public VectorFormat VectorFormat { get; set; }
        public int Length { get; set; }
        public string Coords { get; set; }
        public string Values { get; set; }
        public string Description { get; set; }
    }

    public class MatrixRecord
    {
        public int ID { get; set; }
        public MatrixFormat MatrixFormat { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string RowCoords { get; set; }
        public string ColCoords { get; set; }
        public string Values { get; set; }
        public string Description { get; set; }
    }

}