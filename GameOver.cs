using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
public class GameOver : MonoBehaviour
{
    private string _host = null;
    private string _user = null;
    private string _pass = null;
    private FtpWebRequest _ftpRequest = null;
    private Stream _ftpStream = null;
    private const int BUFFERSIZE = 2048;
    [SerializeField]private Text _score;
    [SerializeField] private TextAsset _readText;

    public GameOver(string hostIP, string userName, string password) {
        _host = hostIP;
        _user = userName;
        _pass = password;
    }
    private string[] _rankText;
    private void Start() {
        _score.text ="Score:" + PlayerStatus._score.ToString();
        _rankText = new string[10];
        _rankText = _readText.text.Split('\n');
        for (int i = 9; i >= 0; i--) {
            if (int.Parse(_rankText[i]) < PlayerStatus._score) {
                if (i == 9) {
                    _rankText[i] = PlayerStatus._score.ToString();
                } else {
                    _rankText[i + 1] = _rankText[i];
                    _rankText[i] = PlayerStatus._score.ToString();
                }
            } else {
                break;
            }
        }
        string path = Directory.GetCurrentDirectory() + @"/Ranking.txt";
        for (int i = 0; i <= 9; i++) {
                if (i == 0) {
                    File.WriteAllText(path, _rankText[i].ToString() + "\n");
                } else {
                    File.AppendAllText(path, _rankText[i].ToString() + "\n");
                }

            }
        print(File.ReadAllText(path));

        TestUP();
    }
    void Update()
    {
        if (Input.anyKeyDown) {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
           Application.Quit();
        #endif
        }
    }
    public void Upload(string remote_file, string local_file) {
        try {
            _ftpRequest = (FtpWebRequest)FtpWebRequest.Create(_host + "/" + remote_file);
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);

            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            _ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            _ftpStream = _ftpRequest.GetRequestStream();
            FileStream localFileStream = new FileStream(local_file, FileMode.Open);
            byte[] byteBuffer = new byte[BUFFERSIZE];
            int bytesSent = localFileStream.Read(byteBuffer, 0, BUFFERSIZE);
            try {
                while (bytesSent != 0) {
                    _ftpStream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, BUFFERSIZE);
                }
            } catch (Exception ex) {
                Debug.Log(ex.ToString() + "UP1");
            }
            localFileStream.Close();
            _ftpStream.Close();
            _ftpRequest = null;
        } catch (Exception ex) {
            Debug.Log(ex.ToString() + "Up2");
        }
        return;
    }
    string _stCurrentDir = System.IO.Directory.GetCurrentDirectory();
    public void TestUP() {
        GameOver ftp = new GameOver(@"ftp://sch02270612.php.xdomain.jp", @"sch02270612.php.xdomain.jp", @"1234567890Th");
        string filename = "Ranking.txt";
        ftp.Upload(filename, Directory.GetCurrentDirectory() + @"/Ranking.txt");
        ftp = null;
    }
}