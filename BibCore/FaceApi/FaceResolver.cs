using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using Logger;
using BibCore.FaceApi.Models;
using System.Text;

namespace BibCore.FaceApi
{
    public class FaceResolver
    {
        //&display_name=bib_cursac&outer_id=bibs

        const string FACESET_OUTER_ID = "bibs";
        const int FACESET_FACES_STEP = 5;

        public static List<FaceSearchModel> Search(string faceId)
        {
            var result = FaceManager.getJsonReturn("search", new NameValueCollection()
            {
                { "face_token", faceId },
                { "outer_id", FACESET_OUTER_ID }
            });

            if (result == null)
                return new List<FaceSearchModel>();
            if (result.ContainsKey("error_message"))
                return new List<FaceSearchModel>();
            List<FaceSearchModel> data = new List<FaceSearchModel>();
            foreach (var item in result["results"])
            {
                FaceSearchModel element = new FaceSearchModel();
                element.Read(item);
                data.Add(element);
            }
            return data;
        }

        public static bool SetFaceUser(string faceId, string tag)
        {
            var result = FaceManager.getJsonReturn("face/setuserid", new NameValueCollection()
            {
                { "face_token", faceId },
                { "user_id", tag }
            });

            bool resp = false;
            if (result != null)
                resp = !result.ContainsKey("error_message");

            if (resp)
                LogManager.Info("Face tag \"" + tag + "\" was applied to \"" + faceId + "\"");
            else LogManager.Error("Unable to apply tag \"" + tag + "\" to \"" + faceId + "\"");

            return resp;
        }

        public static bool AddFaceToFaceset(string faceId)
        {
            return AddFacesToFaceset(new string[] { faceId }, 0);
        }
        public static bool AddFacesToFaceset(string[] faces, int delay)
        {
            bool result = true;
            System.Threading.Thread.Sleep(delay);
            for (int i = 0; i * FACESET_FACES_STEP < faces.Length; i++)
                result = result && addFacesToFaceset(faces.Skip(i * FACESET_FACES_STEP).Take(FACESET_FACES_STEP).ToArray());
            return result;
        }

        private static bool addFacesToFaceset(string[] faces)
        {
            if (faces.Length > FACESET_FACES_STEP)
            {
                faces = faces.Take(FACESET_FACES_STEP).ToArray();
                LogManager.Warning("Force trimming faces. Prohibited to add " + faces.Length + " items");
            }
            string ids = string.Join(",", faces);
            var result = FaceManager.getJsonReturn("faceset/addface", new NameValueCollection()
            {
                { "outer_id", FACESET_OUTER_ID },
                { "face_tokens", ids }
            });

            bool resp = false;
            if (result != null)
                resp = !result.ContainsKey("error_message");

            if (resp)
                LogManager.Info("Faceset updated with \"" + ids + "\"");
            else LogManager.Error("Unable to update faceset with \"" + ids + "\"");

            return resp;
        }
    }
}
