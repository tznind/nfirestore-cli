using CsvHelper.Configuration;
using CsvHelper;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using Terminal.Gui;

namespace nfirestore_cli
{
    internal class TableFromCollection : ITableSource
    {
        public CollectionReference Collection { get; }


        private List<KeyValuePair<DocumentReference, Dictionary<string, object>>> _snaps;
        private string[] _columns;

        public TableFromCollection(CollectionReference cr, IEnumerable<DocumentReference> docs)
        {
            Collection = cr;
            _snaps = docs
                .ToDictionary(k => k, d => d.GetSnapshotAsync().Result?.ToDictionary())
                .ToList();

            var cols = _snaps
                .Where(k => k.Value != null)               // Skip null dictionaries
                .SelectMany(k => k.Value.Keys)             // Safely access Keys
                .Distinct()
                .Order()
                .ToList();

            cols.Insert(0, "Id");
            _columns = cols.ToArray();
        }

        public int Rows => _snaps.Count;

        public int Columns => _columns.Length;

        public string[] ColumnNames => _columns;

        public object this[int row, int col] {
                get {
                    var colName = _columns[col];

                    if(col == 0)
                    {
                        return _snaps[row].Key.Id;
                    }

                    var val = _snaps[row].Value.ContainsKey(colName) ? _snaps[row].Value[colName] : null;

                    if (val is IDictionary)
                    {
                        val = JsonConvert.SerializeObject(val);
                    }

                    return val;
                }
            }

        internal void WriteTo(Stream stream)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            using (var csv = new CsvWriter(new StreamWriter(stream), config))
            {
                foreach (var header in _columns)
                {
                    csv.WriteField(header);
                }
                csv.NextRecord();


                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        csv.WriteField(this[r,c]);
                    }

                    csv.NextRecord();
                }
            }
        }
    }
}