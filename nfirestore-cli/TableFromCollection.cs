using Google.Cloud.Firestore;
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
            _snaps = docs.ToDictionary(k => k, d => d.GetSnapshotAsync().Result.ToDictionary()).ToList();
            _columns = _snaps.SelectMany(k=>k.Value.Keys).ToArray();
        }

        public int Rows => _snaps.Count;

        public int Columns => _columns.Length;

        public string[] ColumnNames => _columns;

        public object this[int row, int col] {
                get {
                var colName = _columns[col];

                return _snaps[row].Value.ContainsKey(colName) ? _snaps[row].Value[colName] : null;
            }
            }

    }
}