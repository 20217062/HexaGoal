using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

public class Title : MonoBehaviour
{
    private void Start() {
        using (WebClient wc = new WebClient()) {
            try {
                wc.Credentials = new NetworkCredential(@"sch02270612.php.xdomain.jp", @"1234567890Th");
                wc.DownloadFile("ftp://sch02270612.php.xdomain.jp/Ranking.txt", Directory.GetCurrentDirectory() + "/Assets/Ranking.txt");
            } catch (WebException ex) {
                print(ex);
            }
            wc.Dispose();
        }
    }
    void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadSceneAsync("Battle");
        }
    }
}