using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nfirestore_cli
{
    public class FirestoreTreePresenter
    {
        public static string AspectGetter(object toRender)
        {
            if (toRender is CollectionReference cr)
            {
                return cr.Id;
            }
            if (toRender is DocumentReference dr)
            {
                return LastPart(dr.Id);
            }

            if (toRender is Document d)
            {
                return LastPart(d.Name);
            }

            return "Unknown Object Type " + toRender.GetType().Name;
        }



        private static string LastPart(string name)
        {
            return name.Substring(name.LastIndexOf('/') + 1);
        }
    }
}
