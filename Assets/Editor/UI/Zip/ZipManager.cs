using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using UnityEditor;

using ICSharpCode.SharpZipLib.Zip;

public class ZipManager {
	
	private static string[] mLanguages = new string[] {
		"translate_zh_CN",
        "translate_zh_US"
    };
	[MenuItem ("Common/Zip Resources/Localization")]
	public static void ZipLocalization () {
		string root = string.Concat(Application.dataPath, "/StreamingAssets/Localization");
		string zipTemp = string.Concat(Application.dataPath, "/StreamingAssets/Zip/Localization.tmp");
		string zip = string.Concat(Application.dataPath, "/StreamingAssets/Zip/Localization.zip");
		if (File.Exists(zipTemp)) {
			File.Delete(zipTemp);
		}
		if (File.Exists(zip)) {
			File.Delete(zip);
		}
		if (!Directory.Exists(Path.GetDirectoryName(zipTemp))) {
			Directory.CreateDirectory(Path.GetDirectoryName(zipTemp));
		}
		
		AssetDatabase.Refresh();

		List<string> localizationFilter = new List<string>();

		for (int i = 0; i < mLanguages.Length; i++) {
			localizationFilter.Add(mLanguages[i]);
		}

		using (ZipOutputStream zos = new ZipOutputStream(File.Open(zipTemp, FileMode.OpenOrCreate))) {
			zos.SetLevel(9);
			byte[] data = ZipFloder(zos, root, root, ".txt", localizationFilter);
			PlayerPrefs.SetString("Localization", CalculateMD5(data));
		}
		AssetDatabase.Refresh();
		// zipTemp to zip
		EncryptZip(zipTemp, zip);
	}
	
	private static byte[] ZipFloder (ZipOutputStream zos, string _rootPath, string _subPath, string _filter, List<string> _filterList) {
		List<byte> bytes = new List<byte>();

		//get all file and decide which to zip
		foreach (FileSystemInfo info in new DirectoryInfo(_subPath).GetFileSystemInfos()) {
			if (Directory.Exists(info.FullName)) {
				ZipFloder(zos, _rootPath, info.FullName, _filter, _filterList);
			} else if (File.Exists(info.FullName) && info.FullName.ToLower().EndsWith(_filter)) {
				bool toZip = false;
				if (_filterList != null) {
					foreach (string file in _filterList) {
						if (Path.GetFileNameWithoutExtension(info.FullName).Equals(file)) {
							toZip = true;
						}
					}
				} else {
					toZip = true;
				}
				if (toZip) {
					//get zip file path
					DirectoryInfo rootDir = new DirectoryInfo(_rootPath);
					string fullName = new FileInfo(info.FullName).FullName;
					string path = fullName.Substring(rootDir.FullName.Length + 1, fullName.Length - rootDir.FullName.Length - 1);
					// read the file to data by stream
					FileStream fs = File.OpenRead(fullName);
					byte[] data = new byte[fs.Length];
					fs.Read(data, 0, data.Length);
					//add the array to list
					bytes.AddRange(data);

					ZipEntry ze = new ZipEntry(path);
					zos.PutNextEntry(ze);
					zos.Write(data, 0, data.Length);
				}
			}
		}
		return bytes.ToArray();
	}
	
	private static void EncryptZip (string _input, string _output) {
		FileStream rfs = new FileStream(_input, FileMode.Open, FileAccess.ReadWrite);
		byte[] buff = new byte[rfs.Length];
		rfs.Read(buff, 0, buff.Length);
		rfs.Close();
		
		byte[] edata = Encrypt(buff);
		
		FileStream wfs = new FileStream(_output, FileMode.Create);
		wfs.Write(edata, 0, edata.Length);
		wfs.Close();
		
		File.Delete(_input);
		
		AssetDatabase.Refresh();
	}
	
	private static string CalculateMD5 (byte[] _data) {
		HashAlgorithm ha = MD5.Create();
		byte[] hashdata = ha.ComputeHash(_data);
		
		StringBuilder md5 = new StringBuilder();
		for (int i = 0; i < hashdata.Length; i++) {
			md5.Append(hashdata[i].ToString("X2"));
		}
		
		return md5.ToString().ToLower();
	}
	
	private static byte[] Encrypt (byte[] _src) {
		DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //8¸ö×Ö·û
		ICryptoTransform ict = des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes("lenovoce"), ASCIIEncoding.ASCII.GetBytes("tangogog"));
		return ict.TransformFinalBlock(_src, 0, _src.Length);
	}
}
