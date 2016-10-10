using UnityEngine;
using System.Collections.Generic;

public class EnumLabelExample : MonoBehaviour
{

	public enum Example
	{
		[EnumLabel("テスト")]
		HIGH,
		[EnumLabel("その２")]
		LOW
	}

	[EnumLabel("例", typeof(Example))]
	public List<Example> test;
}
