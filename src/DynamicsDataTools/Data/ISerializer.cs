﻿namespace DynamicsDataTools.Data
{
    public interface ISerializer
    {
        string Extension { get; }
        void Serialize(DataTable data, string fileName, bool addRecordNumber = false);
        DataTable Deserialize(string fileName);
    }
}
