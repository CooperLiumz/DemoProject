using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CommonUtil
{
    public static class Util : System.Object
    {

        static public bool NetReachable
        {
            get
            {
                return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }

        /// <summary>
        /// 获取指定目录下的文件列表
        /// </summary>
        /// <param name="_dir"></param>
        /// <returns></returns>
        static public string[] GetFilesByDirectory ( string _dir )
        {
            if ( Directory.Exists ( _dir ) )
            {
                return Directory.GetFiles ( _dir );
            }
            else
            {
                return null;
            }
        }

        //将文本信息保存到指定路径，替换原有文件
        static public void SaveTextToPath ( string fileName , string text )
        {
            using ( FileStream stream = new FileStream ( fileName , FileMode.OpenOrCreate ) )
            {
                stream.Seek ( 0 , SeekOrigin.Begin );
                //清空文本里数据
                stream.SetLength ( 0 );
                using ( StreamWriter writer = new StreamWriter ( stream , System.Text.Encoding.UTF8 ) )
                {
                    writer.Write ( text );
                    writer.Flush ( );
                    writer.Close ( );
                }
                stream.Close ( );
            }
        }

        //读取指定txt文件的所有信息
        static public string ReadFileContentByPath ( string path )
        {
            if ( File.Exists ( path ) )
            {
                StreamReader stream = new StreamReader ( path );
                if ( stream != null )
                    return stream.ReadToEnd ( );
                stream.Close ( );
            }
            return null;
        }

        //读取指定文件的所有信息
        static public byte[] ReadFileBytes ( string path )
        {
            if ( File.Exists ( path ) )
            {
                //创建文件读取流
                FileStream fileStream = new FileStream ( path , FileMode.Open , FileAccess.Read );
                fileStream.Seek ( 0 , SeekOrigin.Begin );
                //创建文件长度缓冲区
                byte[] bytes = new byte[ fileStream.Length ];
                //读取文件
                fileStream.Read ( bytes , 0 , ( int ) fileStream.Length );
                //释放文件读取流
                fileStream.Close ( );
                fileStream.Dispose ( );
                fileStream = null;

                return bytes;
            }
            return null;
        }

        //读取图片
        static public Texture2D ReadTexture2D ( string path )
        {
            byte[] _bytes = ReadFileBytes ( path );

            Texture2D mTex2D = null;


            if ( _bytes == null || _bytes.Length < 1 )
            {
            }
            else
            {
                mTex2D = new Texture2D ( 2 , 2 , TextureFormat.PVRTC_RGBA4 , false );
                mTex2D.LoadImage ( _bytes );
            }
            return mTex2D;
        }

        static public Sprite ReadSprite ( string path )
        {
            Texture2D _tex = ReadTexture2D ( path );
            if ( _tex == null )
            {
                return null;
            }
            else
            {
                Sprite _sprite = Sprite.Create ( _tex , new Rect ( 0 , 0 , _tex.width , _tex.height ) , new Vector2 ( 0.5f , 0.5f ) );
                _sprite.name = _tex.name;
                return _sprite;
            }
        }

        //保存bytes文件到指定路径，替换原有文件
        static public void SaveBytesToPath ( string rootpath , string fileName , byte[] bytes )
        {
            if ( string.IsNullOrEmpty ( rootpath ) || string.IsNullOrEmpty ( fileName ) )
            {
                Debug.LogWarning ( "路径错误 ==> rootpath " + rootpath + " fileName ==> " + fileName );
                return;
            }

            string filePath = Path.Combine ( rootpath , fileName );
            if ( !Directory.Exists ( rootpath ) )
            {
                Directory.CreateDirectory ( rootpath );
            }

            if ( File.Exists ( filePath ) )
            {
                File.Delete ( filePath );
            }

            FileStream wfs = new FileStream ( filePath , FileMode.Create , FileAccess.ReadWrite );
            wfs.Write ( bytes , 0 , bytes.Length );
            wfs.Close ( );
        }  

        //删除指定文件
        static public void DeleteFileByPath ( string filePath )
        {
            if ( File.Exists ( filePath ) )
            {
                File.Delete ( filePath );
            }
        }

        //删除指定目录
        static public void DeleteFileByDir ( string dir )
        {
            if ( Directory.Exists ( dir ) )
            {
                Directory.Delete ( dir , true );
            }
        }

        public static Transform FindChild ( Transform parent , string name )
        {
            if ( parent.name == name )
            {
                return parent;
            }

            foreach ( Transform child in parent )
            {
                Transform found = FindChild ( child , name );
                if ( found != null )
                {
                    return found;
                }
            }

            return null;
        }
    }
}

