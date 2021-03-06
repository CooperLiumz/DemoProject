//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// MemoryStream.ReadLine has an interesting oddity: it doesn't always advance the stream's position by the correct amount:
/// http://social.msdn.microsoft.com/Forums/en-AU/Vsexpressvcs/thread/b8f7837b-e396-494e-88e1-30547fcf385f
/// Solution? Custom line reader with the added benefit of not having to use streams at all.
/// </summary>

public class ByteReader : System.Object
{
	byte[] mBuffer;
	int mOffset = 0;

	public ByteReader (byte[] bytes) { mBuffer = bytes; }
	public ByteReader (TextAsset asset) { mBuffer = asset.bytes; }

	/// <summary>
	/// Whether the buffer is readable.
	/// </summary>

	public bool canRead { get { return (mBuffer != null && mOffset < mBuffer.Length); } }

	/// <summary>
	/// Read a single line from the buffer.
	/// </summary>

	static string ReadLine (byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	/// <summary>
	/// Read a single line from the buffer.
	/// </summary>

	public string ReadLine ()
	{
		int max = mBuffer.Length;

		// Skip empty characters
		while (mOffset < max && mBuffer[mOffset] < 32) ++mOffset;

		int end = mOffset;

		if (end < max)
		{
			for (; ; )
			{
				if (end < max)
				{
					int ch = mBuffer[end++];
					if (ch != '\n' && ch != '\r') continue;
				}
				else ++end;

				string line = ReadLine(mBuffer, mOffset, end - mOffset - 1);
				mOffset = end;
				return line;
			}
		}
		mOffset = max;
		return null;
	}

	/// <summary>
	/// Assume that the entire file is a collection of key/value pairs.
	/// </summary>

	public Dictionary<string, string> ReadDictionary ()
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();
		char[] separator = new char[] { '=' };

		while (canRead)
		{
			string line = ReadLine();
			if (line == null) break;
			if (line.StartsWith("//")) continue;
			string[] split = line.Split(separator, 2, System.StringSplitOptions.RemoveEmptyEntries);
			if (split.Length == 2)
			{
				string key = split[0].Trim();
				string val = split[1].Trim().Replace("\\n", "\n");
				dict[key] = val;
			}
		}
		return dict;
	}

	static BetterList<string> mTemp = new BetterList<string>();

	/// <summary>
	/// Read a single line of Comma-Separated Values from the file.
	/// </summary>

	public BetterList<string> ReadCSV ()
	{
		mTemp.Clear();

		if (canRead)
		{
			string line = ReadLine();
			if (line == null) return null;
			line = line.Replace("\\n", "\n");

			int wordStart = 0;
			bool insideQuotes = false;

			for (int i = 0, imax = line.Length; i < imax; ++i)
			{
				char ch = line[i];

				if (ch == ',')
				{
					if (!insideQuotes)
					{
						mTemp.Add(line.Substring(wordStart, i - wordStart));
						wordStart = i + 1;
					}
				}
				else if (ch == '"')
				{
					if (insideQuotes)
					{
						if (i + 1 >= imax)
						{
							mTemp.Add(line.Substring(wordStart, i - wordStart).Replace("\"\"", "\""));
							return mTemp;
						}

						if (line[i + 1] != '"')
						{
							mTemp.Add(line.Substring(wordStart, i - wordStart));
							insideQuotes = false;

							if (line[i + 1] == ',')
							{
								++i;
								wordStart = i + 1;
							}
						}
						else ++i;
					}
					else
					{
						wordStart = i + 1;
						insideQuotes = true;
					}
				}
			}

			if (wordStart < line.Length)
			{
				mTemp.Add(line.Substring(wordStart, line.Length - wordStart));
			}
			return mTemp;
		}
		return null;
	}
}
