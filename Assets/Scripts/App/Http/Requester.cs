using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Requester {

    public IEnumerator PostRequest(string url, string json)
    {
        var uwr             = new UnityWebRequest(url, "POST");
        byte[] jsonToSend   = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler   = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    public IEnumerator GetRequest(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            //handle error
        }
        else {
            var json = www.downloadHandler.text;
            json = json.ToLower(); // TODO: delete
            var bases = BasesDto.FromJson(json);
            var path = Application.persistentDataPath + "/" + "remote";

            if (System.IO.Directory.Exists(path)) {
                System.IO.Directory.Delete(path, true);
            }

            System.IO.Directory.CreateDirectory(path);

            foreach (var q_base in bases.json_dbs)
            {
                var name = 0;
                var base_path = path + "/" + q_base.name;


                System.IO.Directory.CreateDirectory(base_path);

                foreach (var question in q_base.json_db)
                {
                    System.IO.File.WriteAllText(base_path + "/" + name + ".txt", question.IntoJson());
                    name++;
                }
            }
        }
    }
}
