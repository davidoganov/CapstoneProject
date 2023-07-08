using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
		public Vector2 initialValue;
		public Vector2 spawnLoc;

		public void OnAfterDeserialize() { initialValue = spawnLoc; }
		public void OnBeforeSerialize() {}
}
