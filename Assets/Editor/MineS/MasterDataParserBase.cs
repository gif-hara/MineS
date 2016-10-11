using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class MasterDataParserBase<T> where T : ScriptableObject
	{
		protected static void Parse(System.Func<List<string>, T> parser, string csvDataPath, string assetPathFormat)
		{
			var csvData = AssetDatabase.LoadAssetAtPath(csvDataPath, typeof(TextAsset)) as TextAsset;
			var list = CsvParser<T>.Parse(csvData, parser);
			foreach(var item in list.Select((v, i) => new {v, i}))
			{
				EditorUtility.DisplayProgressBar(string.Format("Create {0}", typeof(T).Name), string.Format("Creating... {0}/{1}", item.i, list.Count), (float)item.i / list.Count);
				AssetDatabase.CreateAsset(item.v, string.Format(assetPathFormat, item.i));
			}

			EditorUtility.ClearProgressBar();
		}
	}
}