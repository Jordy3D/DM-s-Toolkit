using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

using System.IO;

public class ExternalLoader : MonoBehaviour
{
  public string path;
  public FileType type = FileType.PNG;

  Texture2D texture;
  Sprite spr;

  [Range(0, 100)]
  public float completion;
  public UnityEvent onCompletion;

  public List<Sprite> loadedSprites;

  public void Load()
  {
    StartCoroutine(nameof(LoadExternal));
  }

  IEnumerator LoadExternal()
  {
    completion = 0;

    DirectoryInfo dir = new DirectoryInfo(path);
    FileInfo[] info = dir.GetFiles("*." + type.ToString(), SearchOption.AllDirectories);

    for (int i = 0; i < info.Length; i++)
    {
      completion = ((float)i / info.Length) * 100;

      FileInfo file = info[i];

      yield return new WaitForSeconds(0);
      var www = UnityWebRequestTexture.GetTexture("file://" + file);
      yield return www.SendWebRequest();

      switch (type)
      {
        case FileType.PNG:
          LoadImage(www, file);
          break;
        case FileType.JPG:
          LoadImage(www, file);
          break;
        case FileType.MP3:
          throw new NotImplementedException();
      }
    }

    completion = 100;
    onCompletion.Invoke();
  }

  public void LoadImage(UnityWebRequest www, FileInfo file)
  {
    texture = DownloadHandlerTexture.GetContent(www);
    spr = Sprite.Create(texture, FullRect(texture), Mid());

    spr.name = FileNameWithoutExtension(file.Name);
    loadedSprites.Add(spr);
  }

  public string FileNameWithoutExtension(string _string)
  {
    var rgx = new System.Text.RegularExpressions.Regex(".[^.]*$");
    return rgx.Replace(_string, "");
  }

  public Rect FullRect(Texture2D _texture) { return new Rect(0, 0, _texture.width, _texture.height); }
  public Vector2 Mid() { return new Vector2(.5f, .5f); }

  public enum FileType
  {
    PNG = 0,
    JPG = 1,
    MP3 = 2
  }
}
