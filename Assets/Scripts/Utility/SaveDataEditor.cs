#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEditor;
using System.IO;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SaveDataEditor
	{
		[MenuItem("MineS/SaveData/Remove")]
		private static void Remove()
		{
			var saveData = new FileInfo(SaveData.Savedatabase.Path + SaveData.Savedatabase.FileName);
			saveData.Delete();
		}
	}
}
#endif